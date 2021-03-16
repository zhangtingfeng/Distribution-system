using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._16SendMoney
{
    public partial class _16SendMoney_BoardABC : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                ArrayList ArrayListA; ArrayList ArrayListB; ArrayList ArrayListC;
                Eggsoft_Public_CL.Pub.SendRed_Get_ABC___Task(strShopClientID, out ArrayListA, out ArrayListB, out ArrayListC);


                for (int i = 0; i < ArrayListA.Count; i++)
                {
                    String strUserID = ArrayListA[i].ToString();
                    Literal1_A.Text += "  <br />顺序：" + (i + 1).ToString() + "  商城ID号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserID) + " 昵称:" + Eggsoft_Public_CL.Pub.GetNickName(strUserID);

                }

                for (int i = 0; i < ArrayListB.Count; i++)
                {
                    String strUserID = ArrayListB[i].ToString();
                    Literal2_B.Text += "  <br />顺序：" + (i + 1).ToString() + "  商城ID号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserID) + " 昵称:" + Eggsoft_Public_CL.Pub.GetNickName(strUserID);

                }
                for (int i = 0; i < ArrayListC.Count; i++)
                {
                    String strUserID = ArrayListC[i].ToString();
                    Literal3_C.Text += "  <br />顺序：" + (i + 1).ToString() + "  商城ID号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(strUserID) + " 昵称:" + Eggsoft_Public_CL.Pub.GetNickName(strUserID);

                }
            }
        }
    }
}