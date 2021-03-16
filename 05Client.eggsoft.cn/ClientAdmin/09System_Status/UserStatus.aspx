<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserStatus.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._09System_Status.UserStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户统计</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mhead">
            <h1>用户统计</h1>

            <div class="mselct">
                <table style="width: 90%;">
                    <tr>
                        <td style="width: 12%; text-align: right;">是否关注:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:CheckBox ID="CheckBox_Subscribe" runat="server" Text="是否关注" Checked="True" /></td>
                        <td style="width: 12%; text-align: right;">用户编号:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ShopUserID" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">昵称:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_NickName" runat="server"></asp:TextBox></td>
                        <td style="width: 12%; text-align: right;">联系电话:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox_ContacPhone" runat="server"></asp:TextBox></td>

                    </tr>

                    <tr>
                        <td style="text-align: right;">&nbsp;</td>
                        <td style="text-align: left;" colspan="5">&nbsp;</td>

                        <td>&nbsp;</td>

                        <td style="text-align: left;">
                            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click_Query" Width="153px" Height="32px" /></td>
                    </tr>

                </table>
            </div>
        </div>


        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">

                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="6%" CssClass="divVisibleNone" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divVisibleNone" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ShopUserID" HeaderText="用户编号">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NickName" HeaderText="昵称">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="UserRealName" HeaderText="真实姓名">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>




                            <asp:BoundField DataField="ContactPhone" HeaderText="联系电话">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="address" HeaderText="地址">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:ImageField DataImageUrlField="HeadImageUrl" HeaderText="头像">
                                <ItemStyle Width="40px" />
                                <ControlStyle Width="40px" />
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />


                                <%--  <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                            </asp:ImageField>

                            <asp:CheckBoxField DataField="Subscribe" HeaderText="是否关注">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>

                            <asp:BoundField DataField="ID" HeaderText="现金￥">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" HeaderText="购物券￥">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

<%--                               <asp:BoundField DataField="TeamIDName" HeaderText="团队">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="ParentName" HeaderText="上级">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ParentShopUserID" HeaderText="上级编号">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:ImageField DataImageUrlField="ParentHeadImageUrl" HeaderText="上级头像">
                               <%-- <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>

                                  <ItemStyle Width="40px" />
                                <ControlStyle Width="40px" />
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:ImageField>




                             <asp:BoundField DataField="TeamUserRealName" HeaderText="团队">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ShopTeamID" HeaderText="团队编号">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:ImageField DataImageUrlField="TeamHeadImageUrl" HeaderText="团队头像">
                               <%-- <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>

                                  <ItemStyle Width="40px" />
                                <ControlStyle Width="40px" />
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:ImageField>


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
