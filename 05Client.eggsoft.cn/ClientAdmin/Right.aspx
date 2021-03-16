<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Right.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin.Right" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Admin_Index_Main</title>
    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <style type="text/css">
        #Label_In7Days {
            display: block;
            word-wrap: break-word;
            word-break: normal;
            width: 800px;
            text-align: left;
        }

        #Label_Over7Days {
            display: block;
            word-wrap: break-word;
            word-break: normal;
            width: 800px;
        }
    </style>

</head>
<body>
    <form id="Form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="99%" border="0">
            <tr bgcolor="#e3e3e3">

                <th class="title" valign="middle" width="100%" colspan="4" style="height: 24px">商户相关信息</th>

            </tr>
            <tr bgcolor="#e3e3e3">
                <td class="border" colspan="3" style="text-align: center;<%=strDisplayDone%>">
                    <table cellspacing="1" cellpadding="2" width="100%" border="0" cellspacing="2" cellpadding="0" align="center" class="mtab">
                        <tr bgcolor="#e3e3e3">
                            <td nowrap class="style1" align="right" width="200">使用说明书地址:</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <a target="_blank" href="http://net.eggsoft.cn/Upload/01Book/02book.pdf"><span style="color: blue">微云基石移动电商系统使用说明书</span></a> </td>
                        </tr>

                        <tr bgcolor="#e3e3e3">
                            <td nowrap class="style1" align="right" width="200">信息通知：</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <asp:Label ID="Label_Info" runat="server"></asp:Label></td>
                        </tr>

                        <tr bgcolor="#e3e3e3">
                            <td nowrap class="style1" align="right" width="200">本商户首页地址：<br />
                                已在CNNIC为贵商户<br />
                                申请开通专属域名</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <asp:Label ID="HeadPage" runat="server"></asp:Label>
                                <asp:Label ID="Label_WeiXin" runat="server"></asp:Label>

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

                      <%--  <tr bgcolor="#e3e3e3">
                            <td nowrap class="style1" align="right">未发货订单数量：</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <asp:Label ID="Label_Board_WaitGiveGoods" runat="server"></asp:Label></td>
                        </tr>--%>
                        <tr bgcolor="#e3e3e3" style="display: none;">
                            <td nowrap class="style1" align="right">已收资金统计7天内：</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <asp:Label ID="Label_In7Days" runat="server"></asp:Label></td>
                        </tr>
                        <tr bgcolor="#e3e3e3" style="display: none;">
                            <td nowrap class="style1" align="right">已收资金统计超过7天：</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <asp:Label ID="Label_Over7Days" runat="server"></asp:Label></td>
                        </tr>
                        <tr bgcolor="#e3e3e3">
                            <td nowrap class="style1" align="right">进入财付通财务处理系统：</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://pay.weixin.qq.com" Target="_blank">点击进行 退款 结账 转账等处理</asp:HyperLink>
                            </td>
                        </tr>
                        <tr bgcolor="#e3e3e3">
                            <td nowrap class="style1" align="right">参考图文编辑器：</td>
                            <td class="tdbg" bgcolor="#ecf5ff">
                                点击打开 <a href="http://editor.o2o10000.cn/index.html" target="_blank">http://editor.o2o10000.cn/index.html</a> 即可在线使用操作。<br />
                                编辑器必须在360极速浏览器下才可以完美使用，没有的建议先下载一个。 
                                <br />                               
                                <a target="_blank" href="http://www.eggsoft.cn/upload/001Vunihicn/图文编辑器教程.doc">图文编辑器教程</a> 、 <a target="_blank" href="http://www.eggsoft.cn/upload/001Vunihicn/公众微信号操作指南.doc">公众微信号操作指南</a> 

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="3">&nbsp;
                </td>
            </tr>
        </table>

        <div class="Loadingdiv" id="Loading"><font face="宋体"></font></div>

    </form>
</body>
</html>
