<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shopping_Vouchers_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._01Shopping_Vouchers.Shopping_Vouchers_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>购物券管理</title>
    <script src="../../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Image/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />


    <style type="text/css">
        .border input {
            height: auto;
        }

        #CheckBoxList_GoodList {
            width: 98%;
            margin: 0px auto;
        }

            #CheckBoxList_GoodList input {
                padding-left: 10px;
                margin-left: 10px;
            }

            #CheckBoxList_GoodList label {
                padding-left: 4px;
            }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#Text_Shopping_Vouchers_Start").calendar();
            $("#Text_Shopping_Vouchers_End").calendar();
        });
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">
                    <strong>优 惠 券 管 理</strong>
                </th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>优惠券名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="Vouchers_Title" runat="server" Width="376px" CssClass="l_input" MaxLength="50"></asp:TextBox>
                    <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="购物券名称不能为空!" ControlToValidate="Vouchers_Title"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>面值金额：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Money" runat="server" Width="137px" CssClass="l_input"></asp:TextBox>元
                <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="购物券金额不能为空!" ControlToValidate="Textbox_Money"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Textbox_Money"
                        ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>发放总量：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_HowManyNum" runat="server" Width="76px" CssClass="l_input"
                        MaxLength="50">10</asp:TextBox>
                    <span class="style1"><strong>*</strong></span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_HowManyNum" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="张数不能为空!"
                        ControlToValidate="Textbox_HowManyNum" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Textbox_HowManyNum" ErrorMessage="发放总量只能是正整数，在1-3000之间" MaximumValue="3000" MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>可使用商品：</strong>
                </td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:Label ID="LabelGoodListMarkerShow" runat="server" Text="没有可使用购物券的商品，必须先为商品设置购物券，这里才能添加优惠券" Visible="False" ForeColor="Red"></asp:Label>
                    <asp:CheckBoxList ID="CheckBoxList_GoodList" runat="server" RepeatDirection="Horizontal" DataValueField="ID" DataTextField="Name" RepeatLayout="Flow">
                    </asp:CheckBoxList>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>限制使用方式：</strong>
                </td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:RadioButtonList ID="RadioButtonList3HowToUse" runat="server" RepeatDirection="Vertical">
                        <asp:ListItem Value="0" Selected="True">不限制（按照商品最大购物券规则）（有效代替规则，多余的面额由系统回收）</asp:ListItem>
                        <asp:ListItem Value="1">满足多少金额才能使用（按照商品最大购物券规则）</asp:ListItem>
                    </asp:RadioButtonList>
                    <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <asp:TextBox ID="TextBox_HowmanyMoneyCanUse" runat="server">0.00</asp:TextBox>
                    元
                <span class="style1"><strong>*</strong></span><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_HowmanyMoneyCanUse"
                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>每人限领：</strong></td>
                <td bgcolor="#ecf5ff">
                    <asp:DropDownList ID="DropDownList1LimitHowMany" runat="server" Width="114px">
                        <asp:ListItem Value="0">不限张</asp:ListItem>
                        <asp:ListItem Value="1">1张</asp:ListItem>
                        <asp:ListItem Value="2">2张</asp:ListItem>
                        <asp:ListItem Value="3">3张</asp:ListItem>
                        <asp:ListItem Value="4">4张</asp:ListItem>
                        <asp:ListItem Value="5">5张</asp:ListItem>
                        <asp:ListItem Value="6">6张</asp:ListItem>
                        <asp:ListItem Value="7">7张</asp:ListItem>
                        <asp:ListItem Value="8">8张</asp:ListItem>
                        <asp:ListItem Value="9">9张</asp:ListItem>
                        <asp:ListItem Value="10">10张</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>过期方式：</strong></td>
                <td bgcolor="#ecf5ff">

                    <asp:CheckBox ID="CheckBox1ValidateTypeAbsoluteCheck" runat="server" Text="有效期起始日期" />
                    <strong>
                        <input type="text" value="<%=strText_Shopping_Vouchers_Start%>" maxlength="100" id="Text_Shopping_Vouchers_Start"
                            name="Text_Shopping_Vouchers_Start" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 3, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                        至<input type="text" value="<%=strText_Shopping_Vouchers_End%>" maxlength="100" id="Text_Shopping_Vouchers_End"
                            name="Text_Shopping_Vouchers_End" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 3, 0)"
                            readonly="true" class="calendarFocus l_input" style="cursor: pointer" /></strong>

                    <br />
                    <asp:CheckBox ID="CheckBox2ValidateTypeRelativeCheck" runat="server" Text="领用后多少天过期" Checked="True" /><asp:TextBox ID="Textbox1ValidateDateTypeRelative" runat="server" Width="76px" CssClass="l_input"
                        MaxLength="50">7</asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox1ValidateDateTypeRelative" ForeColor="#FF3300"></asp:RegularExpressionValidator>


                </td>
            </tr>







            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>领取方式</strong></td>
                <td bgcolor="#ecf5ff" align="left">
                    <asp:RadioButtonList ID="RadioButtonList2HowToGet" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">线上发放（可在线领取）</asp:ListItem>
                        <asp:ListItem Value="1">线下发放（指定领取,必须选中有效起始日期）</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">
                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"
                        CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
