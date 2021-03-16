<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="result_list.aspx.cs" Inherits="_01WA_WebDestop._05XianChangHuoDong.result_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>


    <style type="text/css">
        .gridddd {
            text-align: center;
            margin: 0px auto;
            width: 90%;
        }

        .width20 {
            width: 20%;
            display: inline-block;
            overflow: hidden;
            text-overflow: ellipsis;
        }
    </style>
</head>
<body>
    <div style="height:15px;display:block;"></div>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Logs_EveryDayID" CellPadding="4" CssClass="gridddd" Font-Size="X-Large" HorizontalAlign="Center" EnablePersistedSelection="True" EnableSortingAndPagingCallbacks="True" GridLines="None" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ShopUserID" HeaderText="用户ID" ReadOnly="True" SortExpression="ShopUserID">
                    <ControlStyle CssClass="width20" />
                    <FooterStyle CssClass="width20" />
                    <HeaderStyle CssClass="width20" />
                    <ItemStyle CssClass="width20" />
                </asp:BoundField>
                <asp:BoundField DataField="UserNickName" HeaderText="昵称" SortExpression="UserNickName">
                    <ControlStyle CssClass="width20" />
                    <FooterStyle CssClass="width20" />
                    <HeaderStyle CssClass="width20" />
                    <ItemStyle CssClass="width20" />
                </asp:BoundField>
                <asp:BoundField DataField="UserShakeNumber" HeaderText="成绩" SortExpression="成绩排序">
                    <ControlStyle CssClass="width20" />
                    <FooterStyle CssClass="width20" />
                    <HeaderStyle CssClass="width20" />
                    <ItemStyle CssClass="width20" />
                </asp:BoundField>
                <asp:ImageField DataImageUrlField="HeadImageUrl" HeaderText="标志">
                     <ControlStyle CssClass="width20" />
                    <FooterStyle CssClass="width20" />
                    <HeaderStyle CssClass="width20" />
                    <ItemStyle CssClass="width20" />

                </asp:ImageField>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="5" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
    </form>
</body>
</html>
