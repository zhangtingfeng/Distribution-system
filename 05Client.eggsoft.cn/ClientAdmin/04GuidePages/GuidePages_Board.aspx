<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuidePages_Board.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._04GuidePages.GuidePages_Board" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>MenuSet</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        a:link {
            color: blue;
        }

        .centerAuto {
            margin: auto;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center" class="centerAuto">
                    <br />
                    <strong>&nbsp;资 讯 管 理</strong>
                    <br />
                    <br />
                    管理选项：<asp:Button
                        ID="btnAdd" runat="server" Text="添加信息" OnClick="btnAdd_Click" /><br />
                    <font face="宋体">
                        <br />
                        <asp:GridView ID="gvMenu" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="1px" BorderColor="DarkGray" CellSpacing="1" CellPadding="5"
                            Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound">
                            <HeaderStyle Height="35px" BackColor="#efefef" />
                            <AlternatingRowStyle BackColor="ActiveBorder" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="编号">
                                    <HeaderStyle Width="8%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MenuName" HeaderText="信息名称">
                                    <HeaderStyle Width="20%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MenuIcon" HeaderText="信息图标">
                                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="LinkOrText" HeaderText="显示方式">
                                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MenuLink" HeaderText="信息链接">
                                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>


                                <asp:BoundField HeaderText="修改">
                                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>



                                <asp:BoundField HeaderText="删除">
                                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="二维码">
                                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        &nbsp;
                        <br />
                    </font></td>
            </tr>
        </table>
    </form>
</body>
</html>