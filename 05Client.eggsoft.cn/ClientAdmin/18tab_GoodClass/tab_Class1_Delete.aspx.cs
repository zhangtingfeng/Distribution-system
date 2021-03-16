using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class tab_Class1_Delete : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            EggsoftWX.BLL.tab_Class1 tab_Class1Bll = new EggsoftWX.BLL.tab_Class1();
            EggsoftWX.BLL.tab_Class2 tab_Class2Bll = new EggsoftWX.BLL.tab_Class2();
            EggsoftWX.BLL.tab_Class3 tab_Class3Bll = new EggsoftWX.BLL.tab_Class3();

            int BigClassID = Int32.Parse(Request.QueryString["BigClassID"].ToString());
            if (tab_Class2Bll.Exists("Class1_ID=" + BigClassID.ToString()))
            {
                //Eggsoft.Common.JsUtil.ShowMsg("有下级分类，不能删除！");
                JsUtil.ShowMsg("有下级分类，不能删除！", "tab_Class_BoardSet.aspx");
                return;
            }
            EggsoftWX.BLL.tab_Goods Net_tab_GoodsBll = new EggsoftWX.BLL.tab_Goods();
            if (Net_tab_GoodsBll.Exists("Class1_ID=" + BigClassID + " and IsDeleted=0 and ShopClient_ID=" + str_Pub_ShopClientID))
            {
                //Eggsoft.Common.JsUtil.ShowMsg("有下级分类，不能删除！");
                JsUtil.ShowMsg("该分类有商品，不能删除！", "tab_Class_BoardSet.aspx");
                return;
            }
            EggsoftWX.BLL.tab_Goods tab_GoodsBll = new EggsoftWX.BLL.tab_Goods();


            //删除相关主题
            tab_GoodsBll.Update("isDeleted=0", "Class1_ID=" + BigClassID);
            ////删除相关二级分类
            //  tab_Class2Bll.Delete("Class1_ID=" + BigClassID.ToString());
            //  //删除相关3级分类
            //  tab_Class3Bll.Delete("Class1_ID=" + BigClassID.ToString());


            //删除一级分类


            EggsoftWX.Model.tab_Class1 Model = tab_Class1Bll.GetModel(BigClassID);//删除文件
            Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.ClassIcon));//删除文件
            Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.BigPicpath));//删除文件


            tab_Class1Bll.Delete(BigClassID);
            JsUtil.ShowMsg("删除成功!", "tab_Class_BoardSet.aspx");
        }
    }
}