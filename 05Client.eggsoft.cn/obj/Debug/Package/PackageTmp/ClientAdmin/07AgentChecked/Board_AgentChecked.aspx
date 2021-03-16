<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_AgentChecked.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.Board_AgentChecked" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>代理商管理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        a:link {
            color: blue;
        }

        .centerAuto {
            margin: auto;
            text-align: center;
        }

        .floatRight {
            right:10px;
            position:absolute;
        }
    </style>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>&nbsp;分 销 商 / 
            代 理 商&nbsp; 管 理
            </h1>
            <div class="mselct">
                管理选项：<asp:HyperLink ID="HyperLink_MustRead" runat="server">设置分销须知</asp:HyperLink>
                &nbsp;&nbsp;&nbsp;
            &nbsp;<asp:HyperLink ID="HyperLink_MustReadAd0" runat="server" NavigateUrl="Agent__AddExpListTextShow_Manage.aspx">设置自定义字段</asp:HyperLink>
            </div>
            <div class="mselct">
                <table style="width: 90%;">
                    <tr>
                        <td style="width: 12%; text-align: right;">用户编号:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ShopUserID" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">店铺名称:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ShopName" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">联系人:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ContactMan" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">昵称:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_NickName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: left;">
                            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>
                    </tr>

                </table>
            </div>
        </div>

        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center" style="text-align: center;">

                    <font face="宋体">
                        <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="1px" BorderColor="DarkGray" CellSpacing="1" CellPadding="5"
                            Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound">
                            <HeaderStyle BackColor="#EAF7FF" Height="40" />
                            <AlternatingRowStyle BackColor="ActiveBorder" />
                            <Columns>
                                <asp:BoundField DataField="UserID" HeaderText="用户编号">
                                    <HeaderStyle Width="7%" CssClass="divVisibleNone" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divVisibleNone" Height="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ShopUserID" HeaderText="用户编号">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" Height="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ShopName" HeaderText="店铺名称">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="UserRealName" HeaderText="联系人">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="NickName" HeaderText="昵称">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ContactPhone" HeaderText="联系电话">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>




                                <asp:BoundField DataField="AlipayNumOrWeiXinPay" HeaderText="支付账号">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="UpdateTime" HeaderText="更新日期">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:CheckBoxField DataField="Empowered" HeaderText="是否授权">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:CheckBoxField>
                                <asp:BoundField DataField="AgentLevelSelect" HeaderText="代理级别">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="操作代理权限">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID" HeaderText="现金￥">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID" HeaderText="购物券￥">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TeamParentID" HeaderText="上级代理商">
                                    <HeaderStyle Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        &nbsp;
                        <br />

                        &nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
                        &nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
                            Width="43px">
                        </asp:DropDownList>页</font>
                    <asp:LinkButton ID="LinkButton_DownLoad" CssClass="floatRight" runat="server" OnClick="lbtnLast_Click_DownLoad">表格下载</asp:LinkButton>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
