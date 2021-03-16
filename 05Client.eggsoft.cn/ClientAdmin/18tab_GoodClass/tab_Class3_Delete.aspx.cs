using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass
{
    public partial class tab_Class3_Delete : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String str_Pub_ShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            int Class3_ID = Int32.Parse(Request.QueryString["Class3_ID"].ToString());

            EggsoftWX.BLL.tab_Class3 Net_SmallClassBll = new EggsoftWX.BLL.tab_Class3();


            EggsoftWX.BLL.tab_Goods Net_tab_GoodsBll = new EggsoftWX.BLL.tab_Goods();
            if (Net_tab_GoodsBll.Exists("Class3_ID=" + Class3_ID + " and IsDeleted=0 and ShopClient_ID=" + str_Pub_ShopClientID))
            {
                //Eggsoft.Common.JsUtil.ShowMsg("有下级分类，不能删除！");
                JsUtil.ShowMsg("该分类有商品，不能删除！", "tab_Class_BoardSet.aspx");
                return;
            }

            EggsoftWX.Model.tab_Class3 Model = Net_SmallClassBll.GetModel((Class3_ID));//删除文件
            Eggsoft.Common.FileFolder.DeleteFile(Server.MapPath(Model.ClassIcon));//删除文件

            Net_SmallClassBll.Delete(Class3_ID);

            EggsoftWX.BLL.tab_Goods topicBll = new EggsoftWX.BLL.tab_Goods();
            ////删除相关帖子
            //string tids = "";

            //DataTable dtTopic = topicBll.GetList("ID", "SmallClassID=" + SmallClassID).Tables[0];
            //if (dtTopic != null && dtTopic.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtTopic.Rows.Count; i++)
            //    {
            //        tids += dtTopic.Rows[i]["ID"].ToString();
            //        if (i != dtTopic.Rows.Count-1)
            //        {
            //            tids += ",";
            //        }
            //    }
            //}


            //删除相关主题
            topicBll.Update("isDeleted=0", "Class3_ID=" + Class3_ID);
            JsUtil.ShowMsg("删除成功!", "tab_Class_BoardSet.aspx");
        }
    }
}