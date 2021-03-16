<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02OperationCenter.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._02OperationCenter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>运营中心管理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 16%;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>运 营 中 心 管 理
            </h1>
            <div class="mselct">
                管理选项：
            <asp:Button ID="btnAdd" runat="server" Text="添加运营中心" OnClick="btnAdd_Click" />&nbsp;
            </div>
        </div>

        <div class="mselct">
            <table style="width: 90%;">
                <tr>
                    <td style="width: 16%; text-align: right;">运行状态:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:CheckBox ID="CheckBox_IfRunningState" runat="server" Text="是否在运行" Checked="True" />
                    </td>
                    <td style="width: 16%; text-align: right;">昵称:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_NickName" runat="server"></asp:TextBox></td>
                    <td style="width: 16%; text-align: right;">运营中心账户余额:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="DropDownList_DestinationPrice" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_YuE" runat="server" Width="114px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">联系人:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_MasterName" runat="server"></asp:TextBox></td>
                    <td style="text-align: right;">联系电话:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_MasterPhone" runat="server"></asp:TextBox></td>
                    <td style="text-align: right;">商铺ID:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_ShopUserID" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: left;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>

                </tr>

            </table>
        </div>

        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td width="100%" valign="top" align="center">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF"
                        CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound"
                        CssClass="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="序号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="昵称" DataField="NickName">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <%-- <asp:BoundField HeaderText="昵称" DataField="NickNameShopUserID">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="联系人" DataField="MasterName">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系电话" DataField="MasterPhone">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="微店ID" DataField="ShopUserID">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:CheckBoxField HeaderText="账户运营状态" DataField="RunningState">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField HeaderText="是否股东账户" DataField="ShareholderState">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="BankAccountNumber" HeaderText="银行账号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:HyperLinkField DataNavigateUrlFields="UserID" DataTextField="b003_TotalCredits_OperationCenterRemainingSum" DataNavigateUrlFormatString="11CenterUser_MoneyStatus.aspx?userid={0}" HeaderText="运营中心账户金额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>

                            <%--http://testclient.eggsoft.cn/ClientAdmin/09System_Status/UserStatus_Money.aspx?userid=43221--%>

                            <asp:HyperLinkField DataNavigateUrlFields="UserID" DataTextField="TotalCredits_Consume_Or_RechargeRemainingSum" DataNavigateUrlFormatString="../09System_Status/UserStatus_Money.aspx?userid={0}" HeaderText="个人中心账户金额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>
                            <%-- <asp:BoundField DataField="TotalCredits_Consume_Or_RechargeRemainingSum" HeaderText="个人中心账户金额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="操作运营权限">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="上级运营商">
                                <ItemTemplate>
                                    微店ID：
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ParentIDShopUserID") %>'></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ParentIDExprNickName") %>'></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ParentIDExprUserRealName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    &nbsp;
                <br />
                    <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;
                <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
                    &nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
                        Width="43px">
                    </asp:DropDownList>
                    页
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
