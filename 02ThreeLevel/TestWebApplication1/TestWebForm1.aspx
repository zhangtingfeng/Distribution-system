<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWebForm1.aspx.cs" Inherits="TestWebApplication1.TestWebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Uid" HeaderText="Uid" SortExpression="Uid" />
                <asp:BoundField DataField="Pid" HeaderText="Pid" SortExpression="Pid" />
                <asp:BoundField DataField="Wid" HeaderText="Wid" SortExpression="Wid" />
                <asp:BoundField DataField="Yid" HeaderText="Yid" SortExpression="Yid" />
                <asp:BoundField DataField="CreatTime" HeaderText="CreatTime" SortExpression="CreatTime" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Shop.Earh17.comConnectionString %>" SelectCommand="SELECT * FROM [AccountsRelation]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sq_beidou123ConnectionString %>" SelectCommand="SELECT * FROM [tab_NewsContent]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
