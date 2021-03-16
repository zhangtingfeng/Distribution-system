using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eggsoft_Public_CL
{
    public class OperationCenter
    {
        /// <summary>
        /// 检测是否存在运营中心模式(有运营商品 并且有默认的运营中心)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="stringHttp"></param>
        /// <returns></returns>
        public static bool ExsitMode_OperationCenter(int ShopClientID)
        {
            bool ExsitMode_OperationCenter = false;


            string CacheKey = "ExsitMode_OperationCenter" + ShopClientID.toString();
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    #region
                    bool ExsitStep = true;



                    if (ExsitStep)
                    {
                        ExsitStep = new EggsoftWX.BLL.b004_OperationGoods().Exists("ShopClient_ID=" + ShopClientID + " and IsDeleted=0");
                    }
                    if (ExsitStep)
                    {
                        ///读取默认的运营中心
                        int pOperationCenterID = Eggsoft_Public_CL.Pub.stringShowPower(ShopClientID.toString(), "ConsumptionCapital_OperationCenterID").toInt32();
                        if (!(pOperationCenterID > 0)) ExsitStep = false;
                    }
                    ExsitMode_OperationCenter = ExsitStep;

                    #endregion
                    Eggsoft.Common.DataCache.SetCache(CacheKey, ExsitMode_OperationCenter);// 写入缓存
                    objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
                }
                catch { }
            }
            ExsitMode_OperationCenter = (bool)objType;
            return ExsitMode_OperationCenter;
        }
        #region 仅仅更新一个  粉丝  是扫描 也可能是 转发形成的

        public static Object statictasksupdate_Only_One_UserID_Operation_ID = new Object();
        public static Dictionary<int, DateTime?> Dictionarystatictasksupdate_Only_One_UserID_Operation_ID = new Dictionary<int, DateTime?>();
        /// <summary>
        /// 仅仅更新一个  粉丝
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ShopClientID"></param>
        /// <param name="UserParentID"></param>      
        public static void update_Only_One_UserID_Operation_ID(int UserID, int ShopClientID, int UserParentID, int argOperation_ID = 0)
        {
            if (!ExsitMode_OperationCenter(ShopClientID)) return;
            if (UserParentID == 0) return;////上级不能是空 要根据上级的所在运营中心 查找运营中心
            if (UserID > 0 && ShopClientID > 0)
            {
                try
                {
                    #region 防止多个线程执行一个strLicenseNo+strCompanyCode任务   

                    DateTime? DateTimeRunning = null;
                    lock (statictasksupdate_Only_One_UserID_Operation_ID)
                    {
                        Dictionarystatictasksupdate_Only_One_UserID_Operation_ID.TryGetValue(UserID, out DateTimeRunning);
                        if (DateTimeRunning == null) Dictionarystatictasksupdate_Only_One_UserID_Operation_ID[UserID] = DateTime.Now;
                    }
                    while (DateTimeRunning != null)
                    {
                        if ((DateTime.Now - DateTimeRunning.Value).TotalSeconds < 200)
                        {
                            System.Threading.Thread.Sleep(1000);
                            lock (statictasksupdate_Only_One_UserID_Operation_ID)
                            {
                                Dictionarystatictasksupdate_Only_One_UserID_Operation_ID.TryGetValue(UserID, out DateTimeRunning);
                                if (DateTimeRunning == null)
                                {
                                    Dictionarystatictasksupdate_Only_One_UserID_Operation_ID[UserID] = DateTime.Now;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            #region 超时报警日志
                            Eggsoft.Common.debug_Log.Call_WriteLog("taskNum" + UserID.ToString() + " 超时报警日志random" + UserID.ToString() + "  " + Dictionarystatictasksupdate_Only_One_UserID_Operation_ID.toJsonString(), "更新一个运营中心粉丝", "超时报警日志");
                            Console.WriteLine();
                            return;
                            #endregion
                        }
                    }

                    #endregion


                    EggsoftWX.BLL.b005_UserID_Operation_ID BLL_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                    EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", UserID, ShopClientID);
                    if (Model_b005_UserID_Operation_ID == null)
                    {
                        int intWillWriteOperationCenterID = 0;
                        int intWillWriteOperationCenterID_UserID = 0;

                        if (argOperation_ID > 0)///argOperation_ID指定了运营 强制更新 可能来自订单录入
                        {
                            EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                            EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("ID=@argOperation_ID and ShopClient_ID=@ShopClient_ID", argOperation_ID, ShopClientID);
                            intWillWriteOperationCenterID = argOperation_ID;
                            intWillWriteOperationCenterID_UserID = Model_b002_OperationCenter.UserID.toInt32();
                        }
                        else
                        {
                            EggsoftWX.Model.b005_UserID_Operation_ID ParentModel_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", UserParentID, ShopClientID);
                            if (ParentModel_b005_UserID_Operation_ID != null)
                            {
                                intWillWriteOperationCenterID = ParentModel_b005_UserID_Operation_ID.OperationCenterID.toInt32();
                                intWillWriteOperationCenterID_UserID = ParentModel_b005_UserID_Operation_ID.OperationCenterID_UserID.toInt32();
                            }

                        }
                        if (intWillWriteOperationCenterID > 0)
                        {
                            Model_b005_UserID_Operation_ID = new EggsoftWX.Model.b005_UserID_Operation_ID();
                            Model_b005_UserID_Operation_ID.OperationCenterID = intWillWriteOperationCenterID;
                            Model_b005_UserID_Operation_ID.UserID = UserID;
                            Model_b005_UserID_Operation_ID.ShopClientID = ShopClientID;
                            Model_b005_UserID_Operation_ID.UserParentID = UserParentID;
                            Model_b005_UserID_Operation_ID.OperationCenterID_UserID = intWillWriteOperationCenterID_UserID;
                            int intTypeTableID = BLL_b005_UserID_Operation_ID.Add(Model_b005_UserID_Operation_ID);

                            #region 增加运营会员未处理信息
                            string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "增加运营会员";
                            Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UserID = intWillWriteOperationCenterID_UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = ShopClientID;
                            Model_b011_InfoAlertMessage.Type = "Info_myYunYingAllMember";///运营中心所有会员   
                            Model_b011_InfoAlertMessage.TypeTableID = intTypeTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加运营会员未处理信息  


                        }
                    }
                    else if (Model_b005_UserID_Operation_ID != null && argOperation_ID > 0)////argOperation_ID指定了运营 强制更新 可能来自订单录入
                    {
                        EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                        EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("ID=@argOperation_ID and ShopClient_ID=@ShopClient_ID", argOperation_ID, ShopClientID);


                        //Model_b005_UserID_Operation_ID = new EggsoftWX.Model.b005_UserID_Operation_ID();
                        Model_b005_UserID_Operation_ID.OperationCenterID = argOperation_ID;
                        //Model_b005_UserID_Operation_ID.UserID = UserID;
                        //Model_b005_UserID_Operation_ID.ShopClientID = ShopClientID;
                        Model_b005_UserID_Operation_ID.UserParentID = UserParentID;
                        Model_b005_UserID_Operation_ID.OperationCenterID_UserID = Model_b002_OperationCenter.UserID;
                        BLL_b005_UserID_Operation_ID.Update(Model_b005_UserID_Operation_ID);
                    }
                }
                catch (Exception e)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(e, "更新一个运营中心粉丝", "程序出错");
                }

                finally
                {
                    DateTime? runTime = null;
                    lock (statictasksupdate_Only_One_UserID_Operation_ID)
                    {
                        Dictionarystatictasksupdate_Only_One_UserID_Operation_ID.TryGetValue(UserID, out runTime);
                        if (runTime != null)
                        {
                            Dictionarystatictasksupdate_Only_One_UserID_Operation_ID.Remove(UserID);
                        }
                    }
                }
            }
        }
        #endregion

        #region 更新运营中心的表

        public static Object statictasksupdate_b005_UserID_Operation_ID = new Object();
        public static Dictionary<int, DateTime?> Dictionarystatictasksupdate_b005_UserID_Operation_ID = new Dictionary<int, DateTime?>();
        /// <summary>
        /// 更新运营中心的表
        ///  <para>
        ///  OperationCenterID 如果 没有 。就从b002_OperationCenter中 tab_ShopClient_Agent_ 关联查找
        ///  </para>
        /// <para>UserID ShopClientID 2个参数必传。其他的没有的 可以从 代理表中取。 然后再从tab-User中取
        /// </para>
        /// </summary>      
        /// <param name="UserID"></param>
        /// <param name="ShopClientID"></param>
        /// <returns></returns>
        public static void update_b005_UserID_Operation_ID(int UserID, int ShopClientID, int UserParentID = 0, int OperationCenterID = 0, int OperationCenterID_UserID = 0)
        {
            if (!ExsitMode_OperationCenter(ShopClientID)) return;
            if (UserID == UserParentID) UserParentID = 0;////上级不要是自己
            if (UserID > 0 && ShopClientID > 0)
            {
                try
                {
                    #region 防止多个线程执行一个strLicenseNo+strCompanyCode任务   

                    DateTime? DateTimeRunning = null;
                    lock (statictasksupdate_b005_UserID_Operation_ID)
                    {
                        Dictionarystatictasksupdate_b005_UserID_Operation_ID.TryGetValue(UserID, out DateTimeRunning);
                        if (DateTimeRunning == null) Dictionarystatictasksupdate_b005_UserID_Operation_ID[UserID] = DateTime.Now;
                    }
                    while (DateTimeRunning != null)
                    {
                        if ((DateTime.Now - DateTimeRunning.Value).TotalSeconds < 20)
                        {
                            System.Threading.Thread.Sleep(1000);
                            lock (statictasksupdate_b005_UserID_Operation_ID)
                            {
                                Dictionarystatictasksupdate_b005_UserID_Operation_ID.TryGetValue(UserID, out DateTimeRunning);
                                if (DateTimeRunning == null)
                                {
                                    Dictionarystatictasksupdate_b005_UserID_Operation_ID[UserID] = DateTime.Now;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            #region 超时报警日志
                            Eggsoft.Common.debug_Log.Call_WriteLog("taskNum" + UserID.ToString() + " 超时报警日志UserID" + UserID.ToString() + "  " + Dictionarystatictasksupdate_b005_UserID_Operation_ID.toJsonString(), "更新运营中心系列粉丝", "程序出错 执行超时");

                            Console.WriteLine();
                            return;
                            #endregion
                        }
                    }

                    #endregion





                    EggsoftWX.BLL.b005_UserID_Operation_ID BLL_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                    EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", UserID, ShopClientID);
                    if (Model_b005_UserID_Operation_ID == null)
                    {
                        #region 数据库还没有数据
                        Model_b005_UserID_Operation_ID = new EggsoftWX.Model.b005_UserID_Operation_ID();
                        Model_b005_UserID_Operation_ID.UserID = UserID;
                        Model_b005_UserID_Operation_ID.ShopClientID = ShopClientID;



                        #region 写上级
                        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=@UserID and IsDeleted=0  and ShopClientID=@ShopClientID", UserID, ShopClientID);
                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("ID=@ID and ShopClientID=@ShopClientID", UserID, ShopClientID);
                        EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                        EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel("UserID=@UserID and ShopClient_ID=@ShopClientID and IsDeleted=0", UserID, ShopClientID);


                        if (UserParentID <= 0)
                        {
                            #region 查找上级
                            if (Model_tab_ShopClient_Agent_ != null && Model_tab_ShopClient_Agent_.ParentID > 0)
                            {
                                Model_b005_UserID_Operation_ID.UserParentID = Model_tab_ShopClient_Agent_.ParentID;
                            }
                            else
                            {
                                if (Model_tab_User != null && Model_tab_User.ParentID > 0)
                                {
                                    Model_b005_UserID_Operation_ID.UserParentID = Model_tab_User.ParentID;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            Model_b005_UserID_Operation_ID.UserParentID = UserParentID;
                        }
                        #endregion 写上级

                        #region 写所属运营中心
                        EggsoftWX.Model.b002_OperationCenter boolExsitModel = BLL_b002_OperationCenter.GetModel("ID=" + OperationCenterID + " and ShopClient_ID=" + ShopClientID);
                        if (OperationCenterID <= 0 || boolExsitModel == null)
                        {
                            if (Model_b002_OperationCenter != null)
                            {
                                Model_b005_UserID_Operation_ID.OperationCenterID = Model_b002_OperationCenter.ID;
                                Model_b005_UserID_Operation_ID.OperationCenterID_UserID = Model_b002_OperationCenter.UserID;
                                Model_b005_UserID_Operation_ID.CreateBy = "指定运营中心添加";
                            }
                            else
                            {
                                ////查找父亲的运营中心     都父亲的运营中心
                                if (Model_b005_UserID_Operation_ID.UserParentID > 0)
                                {
                                    EggsoftWX.Model.b005_UserID_Operation_ID ParentModel_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID and ShopClientID=@ShopClientID", Model_b005_UserID_Operation_ID.UserParentID, ShopClientID);
                                    if (ParentModel_b005_UserID_Operation_ID != null)
                                    {
                                        Model_b005_UserID_Operation_ID.CreateBy = "父亲的运营中心添加";
                                        Model_b005_UserID_Operation_ID.OperationCenterID = ParentModel_b005_UserID_Operation_ID.OperationCenterID;
                                        Model_b005_UserID_Operation_ID.OperationCenterID_UserID = ParentModel_b005_UserID_Operation_ID.OperationCenterID_UserID;
                                    }
                                    else
                                    {
                                        return;///
                                    }
                                }
                                else
                                {
                                    return;///
                                }
                            }
                        }
                        else
                        {
                            Model_b005_UserID_Operation_ID.CreateBy = "访问添加";
                            Model_b005_UserID_Operation_ID.OperationCenterID = OperationCenterID;
                            Model_b005_UserID_Operation_ID.OperationCenterID_UserID = BLL_b002_OperationCenter.GetModel(OperationCenterID).UserID;
                        }
                        #endregion 写所属运营中心
                        int intTypeTableID = BLL_b005_UserID_Operation_ID.Add(Model_b005_UserID_Operation_ID);


                        #region 增加运营会员未处理信息
                        if (Model_b005_UserID_Operation_ID != null)
                        {
                            string strwebuy8_ClientAdmin_Users_ClientUserAccount = "公用";
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "指定运营中心添加会员";
                            Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                            Model_b011_InfoAlertMessage.UserID = Model_b005_UserID_Operation_ID.OperationCenterID_UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = ShopClientID;
                            Model_b011_InfoAlertMessage.Type = "Info_myYunYingAllMember";///运营中心所有会员   
                            Model_b011_InfoAlertMessage.TypeTableID = intTypeTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        }
                        #endregion 增加运营会员未处理信息  

                        #endregion 数据库还没有数据

                    }
                    else   //这里不能（查找下线 初期运行时使用。后期 不再使用  应该是 很消耗性能）  否则可能由于嵌套上下级的原因 递归 造成死循环。需要的话 其他地方解决这个问题
                    {////可能是需要手动更新 数据。这时间就强行更新
                        if (OperationCenterID_UserID > 0 && Model_b005_UserID_Operation_ID.OperationCenterID_UserID.toInt32() == 0)
                        {
                            Model_b005_UserID_Operation_ID.OperationCenterID_UserID = OperationCenterID_UserID;
                            Model_b005_UserID_Operation_ID.UpdateTime = DateTime.Now;
                            Model_b005_UserID_Operation_ID.UpdateBy = "需要手动更新数据OperationCenterID_UserID";
                        }
                        if (OperationCenterID > 0 && Model_b005_UserID_Operation_ID.OperationCenterID.toInt32() == 0 || OperationCenterID != Model_b005_UserID_Operation_ID.OperationCenterID.toInt32())
                        {
                            EggsoftWX.BLL.b002_OperationCenter BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                            EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = BLL_b002_OperationCenter.GetModel(OperationCenterID);
                            if (Model_b002_OperationCenter != null)
                            {
                                Model_b005_UserID_Operation_ID.OperationCenterID_UserID = Model_b002_OperationCenter.UserID;
                                Model_b005_UserID_Operation_ID.OperationCenterID = OperationCenterID;
                                Model_b005_UserID_Operation_ID.UpdateTime = DateTime.Now;
                                Model_b005_UserID_Operation_ID.UpdateBy = "需要手动更新数据OperationCenterID";
                            }
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("已有运营中数据，不添加", "运营中心", "访问商品的日志记录");
                        }
                        BLL_b005_UserID_Operation_ID.Update(Model_b005_UserID_Operation_ID);
                    }
                }
                catch (Exception e)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(e, "更新运营中心系列粉丝", "程序出错");
                }

                finally
                {
                    DateTime? runTime = null;
                    lock (statictasksupdate_b005_UserID_Operation_ID)
                    {
                        Dictionarystatictasksupdate_b005_UserID_Operation_ID.TryGetValue(UserID, out runTime);
                        if (runTime != null)
                        {
                            Dictionarystatictasksupdate_b005_UserID_Operation_ID.Remove(UserID);
                        }
                    }

                }
            }
        }
        #endregion

    }
}
