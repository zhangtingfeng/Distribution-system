using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._34WuLiu
{
    public partial class Board34Price_Manage : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        EggsoftWX.BLL._031_Price bllView = new EggsoftWX.BLL._031_Price();


        string strBoard = "Board34PriceChange.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            int intPageIndex = Request.QueryString["PageIndex"].toInt32();
            strBoard += "?PageIndex=" + intPageIndex;

            if (!IsPostBack)
            {

                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");

                    bllView.Update("IsDeleted=1", "ID=@ID", ID);//.Delete(Int32.Parse(ID));
                    JsUtil.ShowMsg("删除成功!", strBoard);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass(sender, e);
                }
            }
        }

        private void SetClass(object sender, EventArgs e)
        {
            EggsoftWX.BLL._031_Channel bllView1111 = new EggsoftWX.BLL._031_Channel();
            DropDownList1Channel.DataSource = bllView1111.GetDataTable("1000", "ChannelName", " and isnull(IsDeleted,0)=0 and ShopClient_ID = " + Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString());
            DropDownList1Channel.DataTextField = "ChannelName";
            DropDownList1Channel.DataValueField = "ChannelName";
            DropDownList1Channel.DataBind();

            System.Collections.ArrayList ar = new System.Collections.ArrayList();
            ar.Add("Package");
            ar.Add("Envelope");
            ar.Add("Pak");
            ar.Add("文件");
            this.TypeDropDownList1.DataSource = ar;
            this.TypeDropDownList1.DataBind();



            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model._031_Price Model = bllView.GetModel(Int32.Parse(ID));

                DropDownList1Channel.SelectedValue = Model.Channel.ToString();
                TypeDropDownList1.SelectedValue = Model.Type.ToString();
                TextBox1Kgs.Text = Model.Kgs.toString();
                TextBox1.Text = Model._1.toString();
                TextBox2.Text = Model._2.toString();
                TextBox3.Text = Model._3.toString();
                TextBox4.Text = Model._4.toString();
                TextBox5.Text = Model._5.toString();
                TextBox6.Text = Model._6.toString();
                TextBox7.Text = Model._7.toString();
                TextBox8.Text = Model._8.toString();
                TextBox9.Text = Model._9.toString();
                TextBoxA.Text = Model.A.toString();
                TextBoxB.Text = Model.B.toString();
                TextBoxD.Text = Model.D.toString();
                TextBoxE.Text = Model.E.toString();
                TextBoxF.Text = Model.F.toString();
                TextBoxG.Text = Model.G.toString();
                TextBoxH.Text = Model.H.toString();
                TextBoxM.Text = Model.M.toString();
                TextBoxN.Text = Model.N.toString();
                TextBoxO.Text = Model.O.toString();
                TextBoxP.Text = Model.P.toString();
                TextBoxQ.Text = Model.Q.toString();
                TextBoxR.Text = Model.R.toString();
                TextBoxS.Text = Model.S.toString();
                TextBoxT.Text = Model.T.toString();
                TextBoxU.Text = Model.U.toString();
                TextBoxV.Text = Model.V.toString();
                TextBoxX.Text = Model.X.toString();
                TextBoxY.Text = Model.Y.toString();
                TextBoxZ.Text = Model.Z.toString();


                //txtXianJin.Text = Model.XianJinMoney.ToString();
                //TextBoxShortDesc.Text = Model.ShortDesc.ToString();
                //RadioButtonList_ChangeDestination.Items[(Convert.ToInt32(Model.ChangeDestination) - 1)].Selected = true;
                //RadioButtonList_ChangeAutoOrHand.Items[(Model.ChangeAuto == "Auto" ? 0 : 1)].Selected = true;
                //RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
                btnAdd.Text = "保 存";


                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                //   RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }






        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.Model._031_Price Model = bllView.GetModel(Int32.Parse(ID));

                Model.Channel = DropDownList1Channel.SelectedValue;
                Model.Type = TypeDropDownList1.SelectedValue;
                Model.Kgs = TextBox1Kgs.Text.toDecimal();

                Model._1 = TextBox1.Text.toDecimal();
                Model._2 = TextBox2.Text.toDecimal();
                Model._3 = TextBox3.Text.toDecimal();
                Model._4 = TextBox4.Text.toDecimal();
                Model._5 = TextBox5.Text.toDecimal();
                Model._6 = TextBox6.Text.toDecimal();
                Model._7 = TextBox7.Text.toDecimal();
                Model._8 = TextBox8.Text.toDecimal();
                Model._9 = TextBox9.Text.toDecimal();
                Model.A = TextBoxA.Text.toDecimal();
                Model.B = TextBoxB.Text.toDecimal();
                Model.D = TextBoxD.Text.toDecimal();
                Model.E = TextBoxE.Text.toDecimal();
                Model.F = TextBoxF.Text.toDecimal();
                Model.G = TextBoxG.Text.toDecimal();
                Model.H = TextBoxH.Text.toDecimal();
                Model.M = TextBoxM.Text.toDecimal();
                Model.N = TextBoxN.Text.toDecimal();
                Model.O = TextBoxO.Text.toDecimal();
                Model.P = TextBoxP.Text.toDecimal();
                Model.Q = TextBoxQ.toDecimal();
                Model.R = TextBoxR.toDecimal();
                Model.S = TextBoxS.toDecimal();
                Model.T = TextBoxT.toDecimal();
                Model.U = TextBoxU.toDecimal();
                Model.V = TextBoxV.toDecimal();
                Model.X = TextBoxX.toDecimal();
                Model.Y = TextBoxY.toDecimal();
                Model.Z = TextBoxZ.toDecimal();





                Model.UpdateTime = DateTime.Now;

                bllView.Update(Model);
                JsUtil.ShowMsg("修改成功!", strBoard);

            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.Model._031_Price Model = new EggsoftWX.Model._031_Price();
                Model.ShopClient_ID = strINCID.toInt32();
                Model.Channel = DropDownList1Channel.SelectedValue;
                Model.Type = TypeDropDownList1.SelectedValue;
                Model.Kgs = TextBox1Kgs.Text.toDecimal();

                Model._1 = TextBox1.Text.toDecimal();
                Model._2 = TextBox2.Text.toDecimal();
                Model._3 = TextBox3.Text.toDecimal();
                Model._4 = TextBox4.Text.toDecimal();
                Model._5 = TextBox5.Text.toDecimal();
                Model._6 = TextBox6.Text.toDecimal();
                Model._7 = TextBox7.Text.toDecimal();
                Model._8 = TextBox8.Text.toDecimal();
                Model._9 = TextBox9.Text.toDecimal();
                Model.A = TextBoxA.Text.toDecimal();
                Model.B = TextBoxB.Text.toDecimal();
                Model.D = TextBoxD.Text.toDecimal();
                Model.E = TextBoxE.Text.toDecimal();
                Model.F = TextBoxF.Text.toDecimal();
                Model.G = TextBoxG.Text.toDecimal();
                Model.H = TextBoxH.Text.toDecimal();
                Model.M = TextBoxM.Text.toDecimal();
                Model.N = TextBoxN.Text.toDecimal();
                Model.O = TextBoxO.Text.toDecimal();
                Model.P = TextBoxP.Text.toDecimal();
                Model.Q = TextBoxQ.toDecimal();
                Model.R = TextBoxR.toDecimal();
                Model.S = TextBoxS.toDecimal();
                Model.T = TextBoxT.toDecimal();
                Model.U = TextBoxU.toDecimal();
                Model.V = TextBoxV.toDecimal();
                Model.X = TextBoxX.toDecimal();
                Model.Y = TextBoxY.toDecimal();
                Model.Z = TextBoxZ.toDecimal();

                bllView.Add(Model);
                JsUtil.ShowMsg("添加成功!", strBoard);
            }
        }
    }

}