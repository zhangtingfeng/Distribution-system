<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_14WcfService1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Class1_ID" HeaderText="Class1_ID" SortExpression="Class1_ID" />
                <asp:BoundField DataField="Class2_ID" HeaderText="Class2_ID" SortExpression="Class2_ID" />
                <asp:BoundField DataField="ClassName" HeaderText="ClassName" SortExpression="ClassName" />
                <asp:BoundField DataField="ClassIcon" HeaderText="ClassIcon" SortExpression="ClassIcon" />
                <asp:BoundField DataField="Updatetime" HeaderText="Updatetime" SortExpression="Updatetime" />
                <asp:BoundField DataField="Sort" HeaderText="Sort" SortExpression="Sort" />
                <asp:CheckBoxField DataField="IsShow" HeaderText="IsShow" SortExpression="IsShow" />
                <asp:CheckBoxField DataField="IsLock" HeaderText="IsLock" SortExpression="IsLock" />
                <asp:BoundField DataField="ShopClientID" HeaderText="ShopClientID" SortExpression="ShopClientID" />
                <asp:BoundField DataField="CreatTime" HeaderText="CreatTime" SortExpression="CreatTime" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Shop.Earth17.Com_ConnectionString %>" SelectCommand="SELECT DISTINCT * FROM [tab_Class3]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
