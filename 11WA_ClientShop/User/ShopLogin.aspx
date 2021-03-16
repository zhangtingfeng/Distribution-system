<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopLogin.aspx.cs" Inherits="_11WA_ClientShop.User.ShopLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关联微派账号</title>

<!-- Mobile Devices Support @begin -->
            <meta content="no-cache,must-revalidate" http-equiv="Cache-Control" /><meta content="no-cache" http-equiv="pragma" /><meta content="0" http-equiv="expires" /><meta content="telephone=no, address=no" name="format-detection" /><meta content="width=device-width, initial-scale=1.0" name="viewport" /><meta name="apple-mobile-web-app-capable" content="yes" /> <!-- apple devices fullscreen -->
            <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
        <!-- Mobile Devices Support @end -->
<link href="Styles/<%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityTemplet")%>/style.css?version=css201709121928" rel="stylesheet" type="text/css" />



</head>
<body>
  
        <div class="top">关联账号<a href="ShopReg.aspx" class="btn_2">注册</a></div>
    <div class="box_weixin">

      <form id="form1" runat="server" class="form_box">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
        <div>  
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody><tr>
                <td class="title">
                    类型：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">用户名</asp:ListItem>
                        <asp:ListItem Value="2">手机号码</asp:ListItem>
                        <asp:ListItem Value="3">Email地址</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </tbody></table>
         <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody><tr>
                <td class="title">                    
                    <asp:Literal ID="Literal__Change" runat="server" Text="用户名："></asp:Literal>
                </td>
                <td class="input">
                    <asp:TextBox ID="TextBox_uid" runat="server"></asp:TextBox>
                  </td>
            </tr>
        </tbody></table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody><tr>
                <td class="title">
                    密码：
                </td>
                <td class="input">
                        <asp:TextBox ID="TextBox_pwd" type="password"  runat="server"></asp:TextBox>
              </td>
            </tr>
        </tbody></table>
        <div class="btn">
            <asp:Button ID="btn_ok" runat="server" Text="确认关联" 
                onclientclick="return CheckNull();" onclick="btn_ok_Click" />
         </div>
        </div>

     
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      </asp:UpdatePanel>
        
   
        </form>
    </div>
   
   <script src="../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
   <script type="text/javascript">
        $(function () {

        });
        function CheckNull() {
            var uid = $.trim($("#TextBox_uid").val().replace(/[　]/g, ""));
            var pwd = $.trim($("#TextBox_pwd").val().replace(/[　]/g, ""));
            if (uid == "") {
                $("#lbl_message").html("用户名不能为空");
                // alert("用户名不能为空");
                $("#txt_uid").val("");
                return false;
            }
            if (pwd == "") {
                $("#lbl_message").html("密码不能为空");
                //   alert("密码不能为空");
                $("#txt_pwd").val("");
                return false;
            }
            return true;
        }
    </script>
       <asp:Panel ID="WUC_Bottom" runat="server">
    </asp:Panel>
</body>
</html>