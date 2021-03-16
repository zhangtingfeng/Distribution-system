<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameSendJiFenBoard.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._22GameSendJiFen.GameSendJiFenBoard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mhead">
            <h1>游 戏 推 广</h1>
        </div>
        <div class="mselct">
            <asp:Button ID="btnAdd_001yuebing2" runat="server" Text="添加中秋节吃月饼" OnClick="btnAdd_Click_001yuebing2" />
            &nbsp;&nbsp;
            <asp:Button ID="btnAdd_002qiangfeicui" runat="server" Text="添加土豪抢翡翠" OnClick="btnAdd_Click_002qiangfeicui" />
            &nbsp;&nbsp;
            <asp:Button ID="Button_003HowColor" runat="server" Text="添加看你有多色" OnClick="btnAdd_Click_003HowColor" />
            </div>
        <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0"
            Width="100%" Font-Size="12px" CssClass="mtab" Border="0" OnRowDataBound="gvAnnounce_RowDataBound">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField HeaderText="游戏名称" DataField="FromName">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField HeaderText="自定义名称" DataField="GameName">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField HeaderText="赠送类型" DataField="SendType">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="赠送金额" DataField="HowManyMoney">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="修改时间" DataField="UpdateTime">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="终止时间" DataField="EndTime">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField HeaderText="修改" DataField="FromName">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>

                <asp:BoundField HeaderText="删除" DataField="FromName">
                    <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
                <asp:BoundField HeaderText="二维码">
                    <HeaderStyle Width="10%" CssClass="centerAuto" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <table height="100%" cellspacing="0" style="text-align: center;" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="left" style="text-align: center;">
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
