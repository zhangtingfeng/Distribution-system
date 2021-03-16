#region Usings
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Eggsoft.Common;
#endregion

//============================================================================
// 新软交易论坛 官方支持：eggsoft.cn
//
// 新软小组 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    /// <summary>
    /// 管理后台基类
    /// </summary>
    public class DotAdminPage__Admin : Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            CommAuthen._Admin_Check();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }


    /// <summary>
    /// 管理后台基类
    /// </summary>
    public class DotAdminPage_ClientAdmin : Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            CommAuthen.Client_CheckAdmin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }



    /// <summary>
    /// 普通用户管理后台基类
    /// </summary>
    public class DotAdminPage_UserAdmin : Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            CommAuthen.User_CheckAdmin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }

   


}
