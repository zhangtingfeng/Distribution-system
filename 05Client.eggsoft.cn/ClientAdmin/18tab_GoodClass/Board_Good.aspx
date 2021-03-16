<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_Good.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.Board_Good" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>商 品 管 理</title>
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
            <h1>商 品 管 理
            </h1>
            <div class="mselct">
                管理选项：
            <asp:Button ID="btnAdd" runat="server" Text="添加商品" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
        </div>

        <div class="mselct">
            <table style="width: 90%;">
                <tr>
                    <td style="width: 16%; text-align: right;">上架状态:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:CheckBox ID="CheckBox_IfUp" runat="server" Text="是否上架" Checked="True" />
                    </td>
                    <td style="width: 16%; text-align: right;">名称:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_GoodName" runat="server"></asp:TextBox></td>
                    <td style="width: 16%; text-align: right;">价格:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="DropDownList_Price" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_Price" runat="server" Width="114px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">打折价格:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_PromotePrice" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_PromotePrice" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">代理商利润:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_AgentPrice" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_AgentMoney" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">购物券最大金额:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="DropDownList_GouWuQuan" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_ShopMaxMoney" runat="server" Width="114px"></asp:TextBox></td>

                </tr>

                <tr>
                    <td style="text-align: right;">库存:</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DropDownList_KuCunCount" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_KuCunCount" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">简短描述:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_ShortInfo" runat="server"></asp:TextBox></td>
                    <td style="text-align: right;">详细描述:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:TextBox ID="TextBox_LongInfo" runat="server"></asp:TextBox></td>

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
                        GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0"
                        CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound"
                        class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="商品编码">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="名称">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" HeaderText="类别">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="updatetime" HeaderText="更新时间">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="打折价格" DataField="PromotePrice">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:BoundField DataField="Sort" HeaderText="排序">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="isSaled" HeaderText="上架状态">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" HeaderText="商品二维码">
                                <HeaderStyle Width="10%" />
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
                    </asp:DropDownList>
                    页
                </td>
            </tr>
        </table>
    </form>
</body>
</html>