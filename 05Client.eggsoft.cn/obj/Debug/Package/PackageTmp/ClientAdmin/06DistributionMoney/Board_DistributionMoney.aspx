<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_DistributionMoney.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._06DistributionMoney.Board_DistributionMoney" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>分销方案提成级别</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
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
                <td valign="top" align="center" style="text-align: center;">
                    <br />
                    <strong>分销方案提成级别</strong>
                    <br />
                    <br />
                    管理选项：<asp:Button
                        ID="btnAdd" runat="server" Text="添加方案" OnClick="btnAdd_Click" Width="100px" /><br />
                    <font face="宋体">
                        <br />
                        <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="1px" BorderColor="DarkGray" CellSpacing="1" CellPadding="5"
                            Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound">
                            <HeaderStyle BackColor="#EAF7FF" Height="40" />
                            <AlternatingRowStyle BackColor="ActiveBorder" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="编号">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" Height="30px" />
                                </asp:BoundField>


                                <asp:BoundField DataField="Name" HeaderText="名称">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                   <asp:BoundField DataField="Partner" HeaderText="代理(%)">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                   <asp:BoundField DataField="Partner1" HeaderText="一级代理(%)">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                   <asp:BoundField DataField="Partner2" HeaderText="二级代理(%)">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                
                                 <asp:BoundField DataField="ShopGet" HeaderText="商铺所得(%)">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                                 
                                <asp:BoundField HeaderText="修改">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="删除">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
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
                        </asp:DropDownList>页</font></td>
            </tr>
        </table>
    </form>
</body>
</html>