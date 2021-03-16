<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="14WealthMoneyControlOperationCenter.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._14WealthMoneyControlOperationCenter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <style type="text/css">
        .border input {
        }

        .IdNumNotShow {
            display:none;
        }

    </style>
    <title>积分管理</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                    align="center" border="0">
                    <tr class="title" bgcolor="#a4b6d7">
                        <th align="left" bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25">积分管理
                        </th>
                    </tr>
                    <tr class="tdbg" bgcolor="#e3e3e3">
                        <td align="right">
                            <strong>输入用户昵称或者ID号码：</strong>
                        </td>
                        <td width="80%" height="35" bgcolor="#ecf5ff">微信昵称：<asp:TextBox ID="TextBox_NickName" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 用户编号：<asp:TextBox ID="TextBox_ShopUserID" runat="server"></asp:TextBox>

                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr class="tdbg" bgcolor="#e3e3e3">
                        <td align="middle" colspan="2">
                            <strong>
                                <font face="宋体">
                                    <asp:Button ID="btnChaXun" runat="server" Text=" 查询 " Width="100px" OnClick="btnChaXun_Click"
                                        OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>                   
                        </td>
                    </tr>

                    <asp:Panel ID="Panel_UserInfo" runat="server" Visible="false">

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right">
                                <strong>
                                    <asp:Label ID="Label_UserInfo" runat="server" Text="Label_UserInfo"></asp:Label>
                                  

                                </strong>
                            </td>
                            <td width="80%" height="35" bgcolor="#ecf5ff">

                                  <asp:Label ID="Label_Number" runat="server" CssClass="IdNumNotShow" Text="Label_Number"></asp:Label>

                                <asp:HyperLink ID="HyperLink_UserInfo_Wealth" runat="server">HyperLink</asp:HyperLink>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:HyperLink ID="HyperLink_Center_Money" runat="server">HyperLink</asp:HyperLink>

                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                <asp:Label ID="Label1ActiveOrderNum" runat="server" Text=""></asp:Label>
                                  
                            </td>
                        </tr>


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right">
                                <strong>充值金额：</strong>
                            </td>
                            <td width="80%" height="35" bgcolor="#ecf5ff"><asp:TextBox ID="TextBox_AddMoney" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextBox_AddMoney"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="充值金额不能为空!"
                                    ControlToValidate="TextBox_AddMoney" ForeColor="#FF3300"></asp:RequiredFieldValidator>  请仔细核对微信昵称和用户编号。微信昵称可能出现类似和重名。本平台充值的唯一条件是用户编号
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right">
                                <strong>增加或减少：</strong>
                            </td>
                            <td width="80%" height="35" bgcolor="#ecf5ff">
                                <asp:RadioButtonList ID="RadioButtonList_AddOrMinus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">减少</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">增加</asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right">
                                <strong>财富积分或运营中心所得：</strong>
                            </td>
                            <td width="80%" height="35" bgcolor="#ecf5ff">
                                <asp:RadioButtonList ID="RadioButtonList_DouWuQuanOrMoney" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">财富积分</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="1">运营中心所得</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="middle" colspan="2">
                                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                                <strong>
                                    <font face="宋体">
                                        <asp:Button ID="Button_DoAdd" runat="server" Text=" 执行 " Width="100px" OnClick="btn_Button_DoAdd_Click"
                                            CssClass="b_input"></asp:Button>                   
                            </td>
                        </tr>
                    </asp:Panel>



                </table>

            </ContentTemplate>


        </asp:UpdatePanel>


        <p style="text-align: center">
            &nbsp;
        </p>
    </form>
</body>
</html>