<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tab_Class_BoardSet.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class_BoardSet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>产品分类</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="./Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <%--    <style type="text/css"> 
/*table中偶数行*/ 
.tabEven 
{ 
background: #9d8e8b; 
background-color:Gray;
} 
/*table中奇数行*/ 
.tabOdd 
{ 
background: #red; 
border:1px solid yellow;
background-color:Blue;
} 
</style> 
<script type="text/javascript">
$(document).ready(function () {
	$("#DataList1_dlst2Class_0 tr:even").addClass("tabEven");
	$("#DataList1_dlst2Class_0 tr:odd").addClass("tabOdd");
}); 
</script> --%>
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <div class="mhead">
            <h1>分 类 设 置 </h1>
            <div class="mselct">
                管理选项：<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加一级分类" />
            </div>
        </div>

        <asp:DataList ID="DataList1" runat="server" CellSpacing="0" CellPadding="0" Width="100%"
            OnItemDataBound="DataList1_ItemDataBound"
            HorizontalAlign="Center">
            <HeaderTemplate>
                <table id="DataList2" width="100%" border="0" cellpadding="0" cellspacing="0" class="border pleft10">
                    <tr bgcolor="#0000CC" class="title">
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>分类名称</strong></th>
                        <th height="20" align="center" style="background: #FFC; color: #666;">
                            <strong>操作选项</strong></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr bgcolor="<%#_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class_BoardSet.getColor(Eval("ID").ToString())%>" class="tdbg">
                    <td height="22" width="20%" class="ystd1">
                        <img src="../Image/tree_folder4.gif" width="15" height="15"><%#Eval("ClassName")%> (<a href="Board_Good.aspx?Class1_ID=<%#Eval("ID")%>">入驻商品(<%#Eval("Class1_IDCount")%>)</a>)</td>
                    <td align="center" width="70%">
                        <a href='tab_Class2_Add.aspx?BigClassID=<%#Eval("ID")%>'>添加二级分类</a> | <a href='tab_Class1_Modify.aspx?BigClassID=<%#Eval("ID")%>'>修改一级分类</a> | <a href='tab_Class1_Delete.aspx?BigClassID=<%#Eval("ID")%>'
                            onclick="return confirm('确定删除吗,相关产品将不可见,且不可恢复!!!')">删除一级分类</a></td>
                </tr>
                <tr bgcolor="<%#_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class_BoardSet.getColor(Eval("ID").ToString())%>" width="100%">
                    <td colspan="2" width="100%">
                        <asp:DataList ID='dlst2Class' EnableViewState='false' runat='server' OnItemDataBound="DataList2_ItemDataBound" Width="100%">
                            <HeaderTemplate>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#000000" class="border">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="<%#_05Client.eggsoft.cn.ClientAdmin._18tab_GoodClass.tab_Class_BoardSet.getColor(Eval("ID").ToString())%>">
                                    <td height="22" width="35%" class="ystd1">
                                        <asp:HiddenField ID="Class2_ID" runat="server" Value='<%#Eval("ID") %>' />
                                        &nbsp;&nbsp&nbsp;&nbsp<img src="../Image/tree_folder3.gif" width="15" height="15"><%#Eval("ClassName")%>(<a href="Board_Good.aspx?Class2_ID=<%#Eval("ID")%>">入驻商品(<%#Eval("Class2_IDCount")%>)</a>)</td>
                                    <td align="center" width="65%">
                                        <a href='tab_Class3_Add.aspx?Class2_ID=<%#Eval("ID")%>'>添加三级分类</a> | <a href='tab_Class2_Modify.aspx?SmallClassID=<%#Eval("ID")%>'>修改二级分类</a> | <a href='tab_Class2_Delete.aspx?SmallClassID=<%#Eval("ID")%>'
                                            onclick="return confirm('确定删除吗,相关产品将不可见,且不可恢复!!!')">删除二级分类</a></td>
                                </tr>

                                <tr bgcolor="#ECF5FF" width="100%">
                                    <td colspan="2" width="100%">
                                        <asp:DataList ID='dlst3Class' EnableViewState='false' runat='server' Width="100%">
                                            <HeaderTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#000000" class="border">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr bgcolor="#FFFF66">
                                                    <td height="22" width="35%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="../Image/tree_folder3.gif" width="15" height="15" /><%#Eval("ClassName")%>(<a href="Board_Good.aspx?Class3_ID=<%#Eval("ID")%>">入驻商品(<%#Eval("Class3_IDCount")%>)</a>)</td>
                                                    <td align="center" width="65%">
                                                        <a href='tab_Class3_Modify.aspx?Class3_ID=<%#Eval("ID")%>'>修改三级分类</a> | <a href='tab_Class3_Delete.aspx?Class3_ID=<%#Eval("ID")%>'
                                                            onclick="return confirm('确定删除吗,相关产品将不可见,且不可恢复!!!')">删除三级分类</a></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:DataList>
                    </td>
                </tr>

            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>

    </form>


</body>
</html>