<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KuaiDiQuery.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._19tab_Order.KuaiDiQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>快递查询</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .width30 {
            width: 20%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr class="title">
                <th height="35">快递查询 </th>
            </tr>
            <tr>
                <td width="100%" valign="top" align="center" style="text-align: center;">&nbsp;
			<br />


                    <asp:Table ID="Table_QueryKuaidi" runat="server">
                        <asp:TableRow runat="server" CssClass="title" BackColor="#F8F8F8">
                            <asp:TableCell runat="server" CssClass="width30">序号</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="width30">时间</asp:TableCell>
                            <asp:TableCell runat="server" CssClass="width30">内容</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>


                    <asp:Localize ID="Localize_ShowInfo" Visible="false" runat="server"></asp:Localize>


                </td>
            </tr>
        </table>
    </form>
</body>
</html>
