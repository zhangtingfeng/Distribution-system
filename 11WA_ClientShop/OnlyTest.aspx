<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlyTest.aspx.cs" Inherits="_11WA_ClientShop.OnlyTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="ContactMan" HeaderText="ContactMan" SortExpression="ContactMan" />
                <asp:BoundField DataField="ContactPhone" HeaderText="ContactPhone" SortExpression="ContactPhone" />
                <asp:BoundField DataField="InsertTime" HeaderText="InsertTime" SortExpression="InsertTime" />
                <asp:BoundField DataField="Updatetime" HeaderText="Updatetime" SortExpression="Updatetime" />
                <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" SortExpression="UpdateBy" />
                <asp:BoundField DataField="NickName" HeaderText="NickName" SortExpression="NickName" />
                <asp:BoundField DataField="UserRealName" HeaderText="UserRealName" SortExpression="UserRealName" />
                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                <asp:BoundField DataField="Sheng" HeaderText="Sheng" SortExpression="Sheng" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="Area" HeaderText="Area" SortExpression="Area" />
                <asp:BoundField DataField="PostCode" HeaderText="PostCode" SortExpression="PostCode" />
                <asp:CheckBoxField DataField="Sex" HeaderText="Sex" SortExpression="Sex" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="Default_Address" HeaderText="Default_Address" SortExpression="Default_Address" />
                <asp:BoundField DataField="OpenID" HeaderText="OpenID" SortExpression="OpenID" />
                <asp:BoundField DataField="HeadImageUrl" HeaderText="HeadImageUrl" SortExpression="HeadImageUrl" />
                <asp:CheckBoxField DataField="Api_Authorize" HeaderText="Api_Authorize" SortExpression="Api_Authorize" />
                <asp:CheckBoxField DataField="Subscribe" HeaderText="Subscribe" SortExpression="Subscribe" />
                <asp:CheckBoxField DataField="IFShowCityHelp" HeaderText="IFShowCityHelp" SortExpression="IFShowCityHelp" />
                <asp:BoundField DataField="RemainingSum" HeaderText="RemainingSum" SortExpression="RemainingSum" />
                <asp:CheckBoxField DataField="IFSendWeiBaiQuan" HeaderText="IFSendWeiBaiQuan" SortExpression="IFSendWeiBaiQuan" />
                <asp:CheckBoxField DataField="IFSendWeiBaiQuan_LiuZong" HeaderText="IFSendWeiBaiQuan_LiuZong" SortExpression="IFSendWeiBaiQuan_LiuZong" />
                <asp:BoundField DataField="SocialPlatform" HeaderText="SocialPlatform" SortExpression="SocialPlatform" />
                <asp:BoundField DataField="ShopClientID" HeaderText="ShopClientID" SortExpression="ShopClientID" />
                <asp:BoundField DataField="AlipayNumOrWeiXinPay" HeaderText="AlipayNumOrWeiXinPay" SortExpression="AlipayNumOrWeiXinPay" />
                <asp:BoundField DataField="ShopUserID" HeaderText="ShopUserID" SortExpression="ShopUserID" />
                <asp:BoundField DataField="ParentID" HeaderText="ParentID" SortExpression="ParentID" />
                <asp:BoundField DataField="HowToGetProduct" HeaderText="HowToGetProduct" SortExpression="HowToGetProduct" />
                <asp:BoundField DataField="DefaultO2OShop" HeaderText="DefaultO2OShop" SortExpression="DefaultO2OShop" />
                <asp:BoundField DataField="multi_DuoKeFu_Lastupdatetime" HeaderText="multi_DuoKeFu_Lastupdatetime" SortExpression="multi_DuoKeFu_Lastupdatetime" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Shop.Earth17.Com_ConnectionString %>" SelectCommand="SELECT * FROM [tab_User] ORDER BY [ID] DESC"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
