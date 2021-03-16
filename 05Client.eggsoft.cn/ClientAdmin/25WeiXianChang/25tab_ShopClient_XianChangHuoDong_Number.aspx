<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="25tab_ShopClient_XianChangHuoDong_Number.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._25WeiXianChang._25tab_ShopClient_XianChangHuoDong_Number" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现场活动 摇一摇 编号列表</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr class="title">
                <th height="35">现场活动 摇一摇 （编号列表<asp:Literal ID="Literaltab_tab_ShopClient_XianChangHuoDong_Number" runat="server"></asp:Literal>
                    &nbsp;）</th>
            </tr>
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">

                        <Columns>

                            <asp:BoundField DataField="XianChangHuoDongNumberbyShopClientID" HeaderText="活动编号">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:BoundField DataField="BeginTime" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndTime" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:HyperLinkField DataTextField="ASCOUNTUserID" HeaderText="参与用户中奖情况" DataNavigateUrlFields="XianChangHuoDongNumberbyShopClientID" DataNavigateUrlFormatString="25XianChangHuoDong_Number_UserShakeNum.aspx?XianChangHuoDongNumberbyShopClientID={0}">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>

                            <asp:TemplateField>
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDel" runat="server" OnClientClick="return confirm('确定删除吗?')" CommandArgument='<%#Eval("ID")%>'
                                        Text='删除中奖' OnClick="lnkDel_Click"></asp:LinkButton>
                                </ItemTemplate>
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
                    </asp:DropDownList>页 
            
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
