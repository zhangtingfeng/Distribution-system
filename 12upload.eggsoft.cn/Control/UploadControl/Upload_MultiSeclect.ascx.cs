using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _12upload.eggsoft.cn.Control.UploadControl
{
    public partial class Upload_MultiSeclect : System.Web.UI.UserControl
    {
        public string Upload_MultiSeclect__ = "";
        public string public_Upload_Path = "";


        protected bool _MultiChoice = true;
        //这个就是简单属性  多选 或者 只能选一个
        public bool MultiChoice
        {
            get { return _MultiChoice; }
            set { _MultiChoice = value; }
        }


        public void OnInit(String argUpload_MultiSeclect, String argUpload_Path)
        {
            if (!IsPostBack)
            {
                TextBox_txtReturnValue.Text = argUpload_MultiSeclect;
                Upload_MultiSeclect__ = argUpload_MultiSeclect;
                public_Upload_Path = argUpload_Path;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Upload_MultiSeclect__ = TextBox_txtReturnValue.Text;
        }
    }
}
