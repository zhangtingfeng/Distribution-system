<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_Agent_Level.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.Board_Agent_Level" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BoardGoodClass</title>
    <link href="../skin/default.css?version=css201507222145" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>代 理 级 别 管 理 </h1>
              <div class="mselct">
                管理选项：<asp:Button ID="Button_ReadDefult" runat="server" Text="读取默认代理级别" OnClick="Button_ReadDefult_Click" Style="text-align: center;display:none;" /><asp:Button ID="btnAdd" runat="server" Text="添加级别" OnClick="btnAdd_Click" Style="text-align: center" />
            &nbsp;&nbsp;&nbsp;
                  <asp:HyperLink ID="HyperLink_MustRead" runat="server">设置代理须知</asp:HyperLink>
            </div>
        </div>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">

                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="AgentLevelName" HeaderText="分类名称">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                                    
                            <asp:BoundField DataField="GouWuQuanGoodPrice" HeaderText="等值购物券商品金额" DataFormatString="{0:C}">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                                              
                            <asp:BoundField DataField="Sort" HeaderText="排序">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="15%" />
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
                    </asp:DropDownList>页 </td>
            </tr>
        </table>
    </form>
</body>
</html>