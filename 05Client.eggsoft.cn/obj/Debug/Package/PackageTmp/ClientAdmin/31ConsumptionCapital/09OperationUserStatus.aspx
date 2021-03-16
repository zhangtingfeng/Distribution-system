<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="09OperationUserStatus.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._09OperationUserStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户统计</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
          <div class="mhead">
            <h1>财富用户统计</h1>

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
                        <td style="text-align: right;">&nbsp;</td>
                        <td style="width: 12%; text-align: right;">运营中心编号:</td>
                        <td style="width: 12%; text-align: left;">
                            <asp:TextBox ID="TextBox1OperationCenterID" runat="server"></asp:TextBox></td>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;</td>

                        <td>
                            &nbsp;</td>

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
                                <HeaderStyle Width="8%"  CssClass="divVisibleNone" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="divVisibleNone" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ShopUserID" HeaderText="用户编号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NickName" HeaderText="昵称">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="UserRealName" HeaderText="真实姓名">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                                                   
                            <asp:BoundField DataField="UserParentIDNickName" HeaderText="上级代理/天使">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                          
                            <asp:BoundField DataField="OperationCenterNickName" HeaderText="所属运营中心">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:CheckBoxField DataField="Subscribe" HeaderText="是否关注">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>
                              <asp:BoundField DataField="ActiveAccount" HeaderText="是否激活">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                              <asp:BoundField DataField="ID" HeaderText="财富￥">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                           <asp:BoundField DataField="ID" HeaderText="现金￥">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" HeaderText="购物券￥">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
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
