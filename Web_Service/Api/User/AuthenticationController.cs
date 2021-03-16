using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Transactions;
using Web_Service.Common.Config;
using InsureApi.WebApi.Model.Common;
using InsureApi.WebApi.Model.User;
using InsureApi.BLL;
using InsureApi.Common.Common;
using InsureApi.WebApi.Model;

namespace Web_Service.Api.User
{

    public class AuthenticationController : BasicController
    {
        /// <summary>
        /// 用户登录
        /// Type PC,WeChat
        /// PC 用户名、密码
        /// APP 用户名、密码
        /// WeChat WeChatID
        /// APPUserID App UserID 登录
        /// </summary>
        [HttpPost]
        public ResultAPIModel<UserAPIModel> Login44(ResultAPIModel<UserAPIModel> m)
        {
            string apiUrl = "Post api/user/authentication/login";
            var result = new ResultAPIModel<UserAPIModel>();
            result.resultMessage.errorMessage = "";
            try
            {
                string msg = "";
                if (!m.data.IsValidInfo(out msg))
                {
                    result.resultMessage.errorMessage = msg;
                    return result;
                }

                result = PCLogin(m);
            }
            catch (Exception ex)
            {
                result.resultMessage = new ResultMessageAPIModel()
                {
                    code = (int)ResultMessageAPIModel.Codes.fail,
                    errorMessage = ex.Message
                };
            }

            return result;
        }

        /// <summary>
        /// PC 登录
        /// </summary>
        private ResultAPIModel<UserAPIModel> PCLogin(ResultAPIModel<UserAPIModel> m)
        {
            var result = new ResultAPIModel<UserAPIModel> { data = new UserAPIModel() };
          
            var TUser_BC = new TUserBC();
            var TUser_Info = TUser_BC.GetUserInfoByAccount(m.data.UserAccount);
            if (TUser_Info == null)
            {
                result.resultMessage.errorMessage = "帐号不存在！";
                result.resultMessage.errorCode = 101;
                return result;
            }
            if (TUser_Info.IsDelete != 0)
            {
                result.resultMessage.errorMessage = "帐号已禁用！";
                return result;
            }

            //登录密码MD5加密验证
            if (!PasswordMD5Class.CheckLoginPassWord(m.data.UserPassword, TUser_Info.UserID, TUser_Info.UserPassword))
            {
                
                result.resultMessage.errorMessage = "登录密码不正确！";
                return result;
            }

            if (TUser_Info.State != 1)
            {
                result.resultMessage.errorMessage = "帐号未授权";
                return result;
            }

            //if (!new TChannelBC().checkChannelTreeStatus(TUser_Info.ChannelNumber))
            //{
            //    result.resultMessage.errorMessage = "账号渠道已禁用";
            //    return result;
            //}

            var userID = TUser_Info.UserID;

            result.data = new UserAPIModel
            {
                UserAccount = TUser_Info.UserAccount,
                UserName = TUser_Info.UserName,
                UserIDCard = TUser_Info.UserIDCard,
                UserIDCardType = TUser_Info.UserIDCardType,
                UserPhone = TUser_Info.UserPhone,
                UserSharedID = TUser_Info.UserSharedID,
                UserType = TUser_Info.UserType ?? 0,
                UserIDRSA = Secret.getIns().encryptRSA(userID),
                UserIDMD5 = Secret.getIns().encryptMD5(userID),
                UserID = userID,
                IsShowBP = TUser_Info.ShowAmount != null && TUser_Info.ShowAmount.Value ? 1 : 0,
                IsShowBPSys = TUser_Info.ShowAmountSys != null && TUser_Info.ShowAmountSys.Value ? 1 : 0,
                ChannelNumber = TUser_Info.ChannelNumber,
                DevelopmentCode = TUser_Info.DevelopmentCode
            };
            //result.data.IsShowPCHeadAndFoot = new TChannelConfigBC().PcHeadAndFootShowState(TUser_Info.ChannelNumber);
            //RSA加密用户ID
            if (result.data.IsShowBPSys == 1)
            {
                //result.data.IsShowBPSys = new TUserBC().GetShowAmountSysStatus(TUser_Info.ChannelNumber,
                //    TUser_Info.UserType);
                if (result.data.IsShowBPSys != 1)
                {
                    result.data.IsShowBP = 0;
                }
            }

            //微信信息
            result.data.OpenID = TUser_Info.WeiXinUserId;
            result.data.NickName = TUser_Info.UserName;
            result.data.HeadimgUrl = TUser_Info.HeadImgUrl;

            //证件信息
            //result.data.UserCardInfos = UserCertificateAPIModel.ConvertListBy(new UserCertificateBC().getInfo(userID));
            return result;
        }
    }
}