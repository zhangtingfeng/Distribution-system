<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_29AdminPower.aspx.cs" Inherits="_05ClientEggsoftCn.ClientAdmin._28Member.Board_29AdminPower" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>账号权限管理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <div class="mhead">
            <h1>账号权限管理</h1>

            <div class="mselct">
                管理选项：
                <asp:Button
                    ID="Button1" runat="server" Text="添加管理账号" OnClick="btnAdd_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button
                    ID="Button2" runat="server" Text="管理角色" OnClick="btnAdd_Click_Role" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button
                    ID="Button3" runat="server" Text="管理企业组织机构" OnClick="btnAdd_Click_Organization" />
            </div>
        </div>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserRealName" HeaderText="真实姓名">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ShopClientAdmin" HeaderText="用户名称">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ShopClient_Role_PowerID" HeaderText="角色名称">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField DataField="EnterpriseOrganizationID" HeaderText="所在团队">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>



                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="12%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="12%" />
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
                    </asp:DropDownList>页 

                    <%-- <p style="text-align:left;">
        1.什么是充值奖励，<a href="http://www.eggsoft.cn/news/info.aspx?id=160" target="_blank">点击查看</a>
    </p>--%>
                </td>
            </tr>
        </table>

    </form>

</body>
</html>
