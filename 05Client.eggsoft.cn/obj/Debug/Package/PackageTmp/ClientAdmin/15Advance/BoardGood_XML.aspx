<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardGood_XML.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._15Advance.BoardGood_XML" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>商 品 XML 管 理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>商 家 字 典 管 理 </h1>
            <div class="mselct">
                管理选项：<asp:Button ID="btnAdd" runat="server" Text="添加XML定义" OnClick="btnAdd_Click" Style="text-align: center" />
            </div>
        </div>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">

                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="XMLName" HeaderText="XML名称">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ID" HeaderText="影响商品数量统计">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <%--     <asp:BoundField DataField="FenXiaoMoney" HeaderText="与微店的共享比率" 
            DataFormatString="{0}%">
        <HeaderStyle Width="10%" />
       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:BoundField>--%>



                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="20%" />
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
