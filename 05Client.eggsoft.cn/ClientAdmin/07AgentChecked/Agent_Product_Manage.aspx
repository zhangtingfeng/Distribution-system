<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Product_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.Agent_Product_Manage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>代理管理</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .borderClass td {
            text-align: right;
            padding-left: 5px;
        }

        .auto-style1 {
            width: 20%;
        }



        .table-striped tbody tr:nth-child(even) td,
        .table-striped tbody tr:nth-child(even) th {
            background-color: #e3e3e3;
        }

        .table-striped tbody tr:nth-child(odd) td,
        .table-striped tbody tr:nth-child(odd) th {
            background-color: #eaf3e3;
        }
    </style>
    <script type="text/javascript">

        function checkAll(obj) {
            $('input[type="checkbox"]').attr("checked", obj.checked);
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table id="TableIDDD" height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                        <br>
                        <br>
                        <br>
                        <table class="borderClass" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                            <tr class="title" bgcolor="#a4b6d7">
                                <td align="center" colspan="2" height="25" style="text-align: center; font-size: 30px;">团队授权管理 <%=pub_RightSLevelGoods%></td>
                            </tr>


                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>是否授权：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:CheckBox ID="CheckBox_Agent" runat="server" Checked="True" />
                                    </div>
                                    <div align="right" style="margin-right: 100px;">
                                    </div>
                                </td>
                            </tr>
                             <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>团队ID：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="LabelShopTeamID" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>代理联系人：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_ContactMan" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>代理店铺名：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_ShopClientName" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0" style="display: none">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>购物券余额：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_GouWuQuanYuE" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>


                              <tr class="tdbg" bgcolor="#c0c0c0">
                                <td bgcolor="#e3e3e3" align="right" class="auto-style2">
                                    <div align="right"><strong>选择团队上级：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" class="auto-style3">

                                    <div align="left" style="margin-right: 100px;">
                                        <asp:DropDownList ID="DropDownListChoiceTeamParentIDList" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                       

                            <tr class="tdbg" bgcolor="#c0c0c0" style="display: none">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>本代理级别等值购物券商品价格：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:Label ID="Label_GouWuQuanGoodPrice" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0" style="display: none">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>购物券充值金额：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:TextBox ID="TextBox_Vouchers_Consume_Or_Recharge" runat="server">0.00</asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" Text="为用户账户的充值金额，如果需要冲红请输入负值"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                                            ValidationExpression="^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$" ControlToValidate="TextBox_Vouchers_Consume_Or_Recharge"
                                            ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="购物券充值金额不能为空!"
                                            ControlToValidate="TextBox_Vouchers_Consume_Or_Recharge" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                        <asp:Literal ID="Literal_ParentID_Show" runat="server"></asp:Literal>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#c0c0c0" style="display:none;">
                                <td bgcolor="#e3e3e3" height="22" align="right" class="auto-style1">
                                    <div align="right"><strong>代理商品及代理所得授权：</strong></div>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">

                                    <div align="left">
                                        <table id="tab" cellspacing="1" class="table-striped">
                                            <asp:Literal ID="Literal_Agent_Percent_Line" runat="server" Text=""></asp:Literal>
                                        </table>
                                    </div>
                                    <div align="left">
                                        全部选择 
                                        <input type="checkbox" onclick="checkAll(this)" id="checkbox1">
                                    </div>
                                </td>
                            </tr>



                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="auto-style1">&nbsp;
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        &nbsp;<br />
                                        &nbsp;<asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br>
                        <br>
                        <br>
                        &nbsp;</td>
                </tr>
            </table>
        </font>
    </form>




</body>
</html>
