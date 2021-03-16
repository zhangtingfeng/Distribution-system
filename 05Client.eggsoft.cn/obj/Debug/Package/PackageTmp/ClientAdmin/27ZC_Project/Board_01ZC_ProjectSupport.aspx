<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_01ZC_ProjectSupport.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._27ZC_Project.Board_01ZC_ProjectSupport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>众 筹 档 位</title>
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
            <h1>众 筹 档 位
                （<asp:Label ID="LabelGoodName" runat="server"></asp:Label>
                ）</h1>
            <div class="mselct">
                管理选项：
            <asp:Button ID="btnAdd" runat="server" Text="添加档位" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>

        <div class="mselct">
            <table style="width: 96%;">
                <tr>
                    <td style="width: 16%; text-align: right;">上架状态:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:CheckBox ID="CheckBox_IfUp" runat="server" Text="是否上架" Checked="True" />
                    </td>
                    <td style="width: 16%; text-align: right;">档位名称:</td>
                    <td style="width: 16%; text-align: left;">
                        <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox></td>
                    <td style="width: 16%; text-align: right;">档位金额:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="DropDownList_SalesPrice" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox_SalesPrice" runat="server" Width="114px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">承诺与回报:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_SalesPricePromiseAndReturn" runat="server"></asp:TextBox></td>
                    <td style="text-align: right;">参与人数:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="DropDownList1PartnerAllCount" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox1PartnerAllCount" runat="server" Width="114px"></asp:TextBox></td>
                    <td style="text-align: right;">参与金额:</td>
                    <td style="text-align: left;" class="auto-style1">
                        <asp:DropDownList ID="DropDownList2ParterMoney" runat="server">
                            <asp:ListItem Selected="True">&gt;</asp:ListItem>
                            <asp:ListItem>=</asp:ListItem>
                            <asp:ListItem>&lt;</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBox2ParterMoney" runat="server" Width="114px"></asp:TextBox></td>

                </tr>

                <tr>
                    <td style="text-align: right;">开奖方法:</td>
                    <td style="text-align: left;">
                        <asp:RadioButtonList ID="RadioButtonListSupportWay" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem Selected="True" Value="-1">所有</asp:ListItem>
                            <asp:ListItem Value="0">无</asp:ListItem>
                            <asp:ListItem Value="1">双色球</asp:ListItem>
                            <asp:ListItem Value="2">福彩3D</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 16%; text-align: right;">参与众筹人员微信昵称:</td>
                    <td>
                        <asp:TextBox ID="TextBox_NcikName" runat="server"></asp:TextBox></td>
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
                             <asp:BoundField DataField="Name" HeaderText="档位名称">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="上架状态" DataField="IsSales">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                         
                            <asp:BoundField HeaderText="档位金额" DataField="SalesPrice">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="回报" DataField="SupportWay">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            
                               <asp:BoundField HeaderText="名额限制" DataField="SalesLimit">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sort" HeaderText="排序">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:HyperLinkField DataNavigateUrlFields="ID" HeaderText="已参与人数" DataNavigateUrlFormatString="Board_01ZC_ProjectParterList.aspx?ID_Support={0}" DataTextField="AllParterCount">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>

                            <asp:HyperLinkField DataNavigateUrlFields="ID" HeaderText="已参与金额" DataNavigateUrlFormatString="Board_01ZC_ProjectParterList.aspx?ID_Support={0}" DataTextField="SalesAllMoney">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:HyperLinkField>

                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="删除">
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
                    </asp:DropDownList>
                    页
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
