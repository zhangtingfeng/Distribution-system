<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowGoodID_ErWieMa.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._14System_WeiXin.ShowGoodID_ErWieMa" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<title>RegisterOpenID</title>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />    
<script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script> 
</HEAD>
<body>
<form id="Form1" runat="server">
<table cellSpacing="0" cellPadding="0" width="99%" border="0">
    <tr class="title"> 
        <th class="title" vAlign="middle" width="100%" colspan="4" style="HEIGHT: 24px"><%=Eggsoft_Public_CL.tab_System_And_.getTab_System("CityName")%>
            商品二维码微信相关信息</th>
    
    </tr>
    <tr>
        <td class="border" colSpan="3" style=" text-align:center;">
            <table cellSpacing="1" cellPadding="2" width="748" border="0" align="center">
                <tr>
                    <td nowrap class="style1" height="35">可分发二微码</td>
                    <td class="tdbg">
                        <asp:Image ID="Image_GoodID" runat="server" />
                    </td>
                </tr>
                
                <%--
                <tr>
                    <td nowrap class="style1">本商户产品页地址：</td>
                    <td class="tdbg"><asp:label id="Pro3D" runat="server"></asp:label></td>
                </tr>
                 <tr>
                    <td nowrap class="style1">本商户首页相册地址：</td>
                    <td class="tdbg"><asp:label id="Xiangce3D" runat="server"></asp:label></td>
                </tr>--%>              
                </table>
        </td>
    </tr> 
    <tr>
        <td colSpan="3">&nbsp;
            </td>
    </tr>
</table> 
<div class="Loadingdiv" id="Loading"> </div> 
</form>
</body>
</HTML>
