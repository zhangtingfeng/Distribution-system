using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;

namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class GoodP_MakeHtml
    {
        public GoodP_MakeHtml()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static String VisitUserListImgeAndName(int pIntGoodID)
        {
            string strVisitUserListImgeAndName = "";
            try
            {
                EggsoftWX.BLL.tab_User_Good_History my_BLL_tab_User_Good_History = new EggsoftWX.BLL.tab_User_Good_History();
                //
                System.Data.DataTable DataTable_tab_User_Good_History = my_BLL_tab_User_Good_History.SelectList("SELECT  Parent_UserID, SUM(Count_Visit) AS Sum_Visit, GoodID FROM  tab_User_Good_History WHERE   (Parent_UserID > 0)  and GoodID=" + pIntGoodID + " GROUP BY Parent_UserID, GoodID order by Sum_Visit desc").Tables[0];
                int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(pIntGoodID);

                int intMax = 0;
                if (DataTable_tab_User_Good_History.Rows.Count > 0)/// //共多少人分享=0时间 改成 共多少人访问
                {
                    //string strLocal_Share_Html = "/MakeHTML/001ShareHeadImg/" + Eggsoft.Common.StringNum.Add000000Num(pIntGoodID, 8) + ".html";
                    //if (Eggsoft.Common.FileFolder.File_Exists(strLocal_Share_Html) == false)//不存在的话 请求一下
                    //{

                    for (int i = 0; i < DataTable_tab_User_Good_History.Rows.Count; i++)
                    {
                        string strParent_UserID = DataTable_tab_User_Good_History.Rows[i]["Parent_UserID"].ToString();

                        string strNicknbameP = "";
                        if (strParent_UserID != "0")
                        {
                            strNicknbameP = Eggsoft_Public_CL.Pub.GetNickName(strParent_UserID);
                        }
                        //远程文件
                        string strGet_HeadImage_Parent_UserID = Eggsoft_Public_CL.Pub.Get_HeadImage(strParent_UserID);

                        string strPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClientID);

                        //本地文件
                        string strHead_Parent_Image = strPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strParent_UserID), 6) + ".jpg";
                        if (Eggsoft.Common.FileFolder.RemoteFileExists(Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Parent_Image))//不存在的话 跳到下一个
                        {
                            //if (Eggsoft.Common.FileFolder.RemoteFileExists(strGet_HeadImage_Parent_UserID) == false) 
                            // continue;
                            //Eggsoft_Public_CL.GoodP.DownLoadFile_Service_ScaleBMP(strGet_HeadImage_Parent_UserID, strHead_Parent_Image, 80, 80, "HW");
                        }
                        else
                        {
                            continue;
                        }

                        intMax++;
                        if (intMax >= 14) break;
                        strVisitUserListImgeAndName += " <li>\n";
                        strVisitUserListImgeAndName += "       <a  target=\"_blank\" href=" + strGet_HeadImage_Parent_UserID + ">   <div class=\"thisLine_image  user" + strParent_UserID + "\">\n";
                        strVisitUserListImgeAndName += "            <img src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Parent_Image + "\">\n";
                        strVisitUserListImgeAndName += "        </div> </a>\n";

                        strVisitUserListImgeAndName += "    </li>\n";
                    }
                    string strGaoDao_SharePeopleNum = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "GaoDao_SharePeopleNum");///
                    Int32 intGaoDao_SharePeopleNum = 0;
                    Int32.TryParse(strGaoDao_SharePeopleNum, out intGaoDao_SharePeopleNum);

                    strVisitUserListImgeAndName += " <li>\n";
                    strVisitUserListImgeAndName += "        <div class=\"thisLine_image_User0\">\n";
                    strVisitUserListImgeAndName += "            <br />共" + (DataTable_tab_User_Good_History.Rows.Count+ intGaoDao_SharePeopleNum) + "人<br />分享\n";
                    strVisitUserListImgeAndName += "        </div>\n";
                    strVisitUserListImgeAndName += "    </li>\n";

                    //Eggsoft.Common.FileFolder.WriteHtmlFile(strVisitUserListImgeAndName, strLocal_Share_Html);
                    return strVisitUserListImgeAndName;
                    //}
                    //else
                    //{
                    //    return Eggsoft.Common.FileFolder.ReadHTML(strLocal_Share_Html);
                    //}

                }
                else//改成 共多少人访问
                {
                    //string strLocal_Visit_Html = "/MakeHTML/002VisitHeadImg/" + Eggsoft.Common.StringNum.Add000000Num(pIntGoodID, 8) + ".html";
                    //if (Eggsoft.Common.FileFolder.File_Exists(strLocal_Visit_Html) == false)//不存在的话 请求一下
                    //{

                    DataTable_tab_User_Good_History = my_BLL_tab_User_Good_History.SelectList("SELECT  UserID,SUM(Count_Visit) AS Sum_Visit FROM  tab_User_Good_History WHERE   (Parent_UserID = 0)  and GoodID=" + pIntGoodID + " GROUP BY UserID order by Sum_Visit desc").Tables[0];

                    for (int i = 0; i < DataTable_tab_User_Good_History.Rows.Count; i++)
                    {
                        string strParent_UserID = DataTable_tab_User_Good_History.Rows[i]["UserID"].ToString();
                        string strSum_Visit = DataTable_tab_User_Good_History.Rows[i]["Sum_Visit"].ToString();

                        string strNicknbameP = "";
                        if (strParent_UserID != "0")
                        {
                            strNicknbameP = Eggsoft_Public_CL.Pub.GetNickName(strParent_UserID);
                        }

                        //远程文件
                        string strGet_HeadImage_Parent_UserID = Eggsoft_Public_CL.Pub.Get_HeadImage(strParent_UserID);
                        //本地文件
                        string strPath = Eggsoft_Public_CL.Upload.getUploadPathFromShopClientID(intShopClientID);


                        string strHead_Parent_Image = strPath + "/HeadImage/User" + Eggsoft.Common.StringNum.Add000000Num(Int32.Parse(strParent_UserID), 6) + ".jpg";
                        if (Eggsoft.Common.FileFolder.RemoteFileExists(Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Parent_Image))//不存在的话 跳到下一个
                        {
                            //if (Eggsoft.Common.FileFolder.File_Exists(strHead_Parent_Image) == false)//不存在的话 跳到下一个
                            //{

                            //if (Eggsoft.Common.FileFolder.RemoteFileExists(strGet_HeadImage_Parent_UserID) == false) 

                            //Eggsoft.Common.Download.DownLoadFile_Service(strGet_HeadImage_Parent_UserID, strHead_Parent_Image);
                            //Eggsoft_Public_CL.GoodP.ScaleBMP(strHead_Parent_Image, 100, 100, "HW");

                        }
                        else
                        {
                            continue;
                        }
                        intMax++;
                        if (intMax >= 14) break;
                        strVisitUserListImgeAndName += " <li>\n";
                        strVisitUserListImgeAndName += "    <a  target=\"_blank\" href=" + strGet_HeadImage_Parent_UserID + ">    <div class=\"thisLine_image  user" + strParent_UserID + "\">\n";
                        strVisitUserListImgeAndName += "            <img alt=\"" + strSum_Visit + "\" src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strHead_Parent_Image + "\">\n";
                        strVisitUserListImgeAndName += "        </div></a>\n";

                        strVisitUserListImgeAndName += "    </li>\n";
                    }

                    string strGaoDao_VisitPeopleBaseNum = Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "GaoDao_VisitPeopleBaseNum");///
                    Int32 intGaoDao_VisitPeopleBaseNum = 0;
                    Int32.TryParse(strGaoDao_VisitPeopleBaseNum, out intGaoDao_VisitPeopleBaseNum);


                    strVisitUserListImgeAndName += " <li>\n";
                    strVisitUserListImgeAndName += "        <div class=\"thisLine_image_User0\">\n";
                    strVisitUserListImgeAndName += "            <br />共" + (DataTable_tab_User_Good_History.Rows.Count+ intGaoDao_VisitPeopleBaseNum) + "人<br />访问\n";
                    strVisitUserListImgeAndName += "        </div>\n";
                    strVisitUserListImgeAndName += "    </li>\n";
                    //Eggsoft.Common.FileFolder.WriteHtmlFile(strVisitUserListImgeAndName, strLocal_Visit_Html);
                    return strVisitUserListImgeAndName;
                    //}
                    //else
                    //{
                    //    return Eggsoft.Common.FileFolder.ReadHTML(strLocal_Visit_Html);
                    //}
                }


            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "商品静态页");
            }
            finally
            {

            }
            return strVisitUserListImgeAndName;


        }



        public static String MakeHtml_AnnouncePic_GoodList(int pIntGoodID)
        {
            string strReturnAnnouncePic_GoodList = "";

            try
            {
             
                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);





                String[] StringmePicList = my_Model_tab_Goods.Icon.Split(';');

                ArrayList alStringPicList = new ArrayList();
                for (int i = 0; i < StringmePicList.Length; i++)
                {
                    if (String.IsNullOrEmpty(StringmePicList[i]) == false)
                    {
                        string strGetAppConfiugUplaod = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + StringmePicList[i];
                        if (Eggsoft.Common.FileFolder.RemoteFileExists(strGetAppConfiugUplaod))
                        {
                            alStringPicList.Add(StringmePicList[i]);
                        }
                    }
                }


                string strAnnouncePic_GoodList = "";
                string strAnnouncePic_Dot_GoodList = "";
                ///start  保证有2个 在循环
                //if (alStringPicList.Count == 1)
                //{
                //    alStringPicList.Add(alStringPicList[0]);
                //}
                //end  保证有2个 在循环

                strReturnAnnouncePic_GoodList += "  <ul style=\"list-style: none outside none; width: 3200px; transition-duration: 500ms;transform: translate3d(0px, 0px, 0px);\">\n";
                strReturnAnnouncePic_GoodList += "            ###AnnouncePic_GoodList###\n";
                strReturnAnnouncePic_GoodList += "        </ul>\n";
                if (alStringPicList.Count > 1)///有2个才出现轮播图下面的位置示意图
                {
                    strReturnAnnouncePic_GoodList += "        <ol>\n";
                    strReturnAnnouncePic_GoodList += "            ###AnnouncePic_Dot_GoodList###\n";
                    strReturnAnnouncePic_GoodList += "        </ol>\n";
                }

                for (int k = 0; k < alStringPicList.Count; k++)
                {
                    string strimg = alStringPicList[k].ToString();
                    string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strimg;


                    strAnnouncePic_GoodList += "<li style=\"width: 640px; display: table-cell; vertical-align: top;\"><a href=\"#\"><img src=\"" + GoodIcon + "\" alt=\"" + my_Model_tab_Goods.Name + "\" style=\"width:100%;\"></a></li>\n";

                    if (k == 0)
                    {
                        strAnnouncePic_Dot_GoodList += "<li class=\"on\"></li>";
                    }
                    else
                    {
                        strAnnouncePic_Dot_GoodList += "<li class=\"\"></li>";
                    }

                }

                strReturnAnnouncePic_GoodList = strReturnAnnouncePic_GoodList.Replace("###AnnouncePic_GoodList###", strAnnouncePic_GoodList);
                strReturnAnnouncePic_GoodList = strReturnAnnouncePic_GoodList.Replace("###AnnouncePic_Dot_GoodList###", strAnnouncePic_Dot_GoodList);

                //Eggsoft.Common.FileFolder.WriteHtmlFile(strReturnAnnouncePic_GoodList, strLocal_AnnouncePic_Good_Html);

                //  return strAnnouncePic_GoodList;
                //}
                //else
                //{
                //    strReturnAnnouncePic_GoodList = Eggsoft.Common.FileFolder.ReadHTML(strLocal_AnnouncePic_Good_Html);

                //}

            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "商品静态页");
            }
            finally
            {

            }

            return strReturnAnnouncePic_GoodList;
        }




    }
}