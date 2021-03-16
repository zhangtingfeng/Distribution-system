<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="25WeiXianChang_BoardSet.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._25WeiXianChang._25WeiXianChang_BoardSet" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>现 场 活 动 管 理</title>
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
            <h1>现 场 活 动 面 板 </h1>
            <div class="mselct">
                管理选项：<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加现场活动" />
            </div>
        </div>

        <asp:DataList ID="DataList1tab_ShopClient_XianChangHuoDong" runat="server" CellSpacing="0" CellPadding="0" Width="100%"
            OnItemDataBound="DataList1_ItemDataBoundtab_ShopClient_XianChangHuoDong"
            HorizontalAlign="Center">
            <HeaderTemplate>
                <table id="DataList2" width="100%" border="0" cellpadding="0" cellspacing="0" class="border pleft10">
                    <tr bgcolor="#0000CC" class="title">
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>序号</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>活动名称</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>现场二维码</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>是否必须关注</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>必须收获地址</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>重复抽奖</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>活动状态</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>活动次数</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>抽奖次数</strong></th>
                        <th height="25" align="center" style="background: #FFC; color: #666;">
                            <strong>大屏幕</strong></th>
                        <th height="20" align="center" style="background: #FFC; color: #666;">
                            <strong>操作选项</strong></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr bgcolor="<%#_05Client.eggsoft.cn.ClientAdmin._25WeiXianChang._25WeiXianChang_BoardSet.GetColor(Eval("ID").ToString())%>" class="tdbg">
                    <td height="22" width="8%" class="ystd1AutoCenter"><%#Eval("ID")%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter">
                        <%#Eval("ActivityName")%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter"><%#getShowAgentErWeiMa_UserID_ByAgent(Eval("ShowAgentErWeiMa_UserID_ByAgent").ToString())%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter"><%#Eval("Subscribe_Must")%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter"><%#Eval("Address_Must")%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter"><%#Eval("GetBonusRepeat")%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter"><%#Eval("ActivityState")%></td>
                    <td height="22" width="8%" class="ystd1AutoCenter">
                        <asp:HyperLink ID="HyperLink_Numbers" runat="server"></asp:HyperLink></td>
                    <td height="22" width="8%" class="ystd1AutoCenter">
                        <asp:HyperLink ID="HyperLink_Bonus" runat="server"></asp:HyperLink></td>
                    <td height="22" width="8%" class="ystd1AutoCenter">
                        <asp:HyperLink ID="HyperLinkBigScreenLink" runat="server"></asp:HyperLink></td>
                    <td align="center" width="8%">
                        <a href='25WeiXianChang_Manage.aspx?type=modify&ID=<%#Eval("ID")%>'>修改现场活动</a><br />
                        <a href='25WeiXianChang_Manage.aspx?type=delete&ID=<%#Eval("ID")%>'
                            onclick="return confirm('确定删除吗,相关活动将不可见,且不可恢复!!!')">删除现场活动</a></td>
                </tr>
                <%--<tr bgcolor="<%#_05Client.eggsoft.cn.ClientAdmin._25WeiXianChang._25WeiXianChang_BoardSet.GetColor(Eval("ID").ToString())%>" width="100%">
                    <td colspan="9" width="100%">
                        <asp:DataList ID='dlst2_XianChangHuoDong_Number' EnableViewState='false' runat='server' Width="100%">
                            <HeaderTemplate>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#000000" class="border">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="<%#_05Client.eggsoft.cn.ClientAdmin._25WeiXianChang._25WeiXianChang_BoardSet.GetColor(Eval("ID").ToString())%>">
                                    <td height="22" width="25%" class="ystd1">
                                        <asp:HiddenField ID="XianChangHuoDongID" runat="server" Value='<%#Eval("ID") %>' />
                                        活动编号:<%#Eval("XianChangHuoDongNumberbyShopClientID")%></td>
                                    <td height="22" width="25%" class="ystd1">开始时间:<%# ((DateTime)Eval("BeginTime")).ToString("yyyy-MM-dd HH:mm:ss") %></td>
                                    <td height="22" width="25%" class="ystd1"><a href="25XianChangHuoDong_Number_UserShakeNum.aspx?XianChangHuoDongNumberbyShopClientID=<%#Eval("XianChangHuoDongNumberbyShopClientID").ToString()%>">参与人数:<%#Eval("ASCOUNTUserID").ToString()%></a></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:DataList>
                    </td>
                </tr>--%>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>

    </form>


</body>
</html>
