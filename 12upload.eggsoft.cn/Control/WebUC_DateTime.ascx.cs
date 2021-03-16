using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.Control
{
    public partial class WebUC_DateTime : System.Web.UI.UserControl
    {

        public string Text_selectTime
        {
            get { return this.TextBox_selectTime.Text; }
            set { TextBox_selectTime.Text = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ini_DropDownList_selectTime();

                //TextBox_selectTime.Text = DateTime.Now.AddDays(3).ToString();


            }


        }

        private void ini_DropDownList_selectTime()
        {

            for (int i = 0; i < 24; i++)
            {
                ListItem ListItemNew = new ListItem((i).ToString(), (i).ToString());
                DropDownList_Hour.Items.Add(ListItemNew);

            }
            for (int i = 0; i < 60; i++)
            {
                ListItem ListItemNew = new ListItem((i).ToString(), (i).ToString());
                DropDownList_Min.Items.Add(ListItemNew);

            }
        }


        protected void Button_selectTime_Click(object sender, EventArgs e)
        {
            Table_Show.Visible = !Table_Show.Visible;

            if (Table_Show.Visible)
            {
                Button_selectTime.Text = "确认选择";

                string strTime = TextBox_selectTime.Text;
                DateTime myoutResult = DateTime.Now;
                DateTime.TryParse(strTime, out myoutResult);

                //DateTime my=Convert.ToDateTime(strTime);

                this.Calendar1.SelectedDate = myoutResult;
                //this.Calendar1.se
                DropDownList_Hour.SelectedIndex = myoutResult.Hour;
                DropDownList_Min.SelectedIndex = myoutResult.Minute;

                //Calendar1.SelectedDate.Year = my.Year;
                //Calendar1.SelectedDate.Year = my.Year;
                //Calendar1.SelectedDate.Year = my.Year;
                //Calendar1.SelectedDate.Year = my.Year;



            }
            else
            {
                Button_selectTime.Text = "选择时间";

                DateTime DateTimenow = new DateTime(Calendar1.SelectedDate.Year, Calendar1.SelectedDate.Month, Calendar1.SelectedDate.Day, DropDownList_Hour.SelectedIndex, DropDownList_Min.SelectedIndex, 0);

                TextBox_selectTime.Text = DateTimenow.ToString();
            }

        }
    }
}