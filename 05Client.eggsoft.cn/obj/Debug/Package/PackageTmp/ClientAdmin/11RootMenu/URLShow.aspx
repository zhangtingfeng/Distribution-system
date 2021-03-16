<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="URLShow.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._11RootMenu.URLShow" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>BasicInfo</title>

    <script type="text/javascript" script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td {
            height: 30px;
        }

        .auto-style1 {
            width: 20%;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="auto-style1">
                    <font face="宋体">
                        <strong>我的店铺(逛街)：</strong></font></td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="Label_MyShop" runat="server" Text=""></asp:Label>
                </td>
            </tr>



            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="left">
                    <font face="宋体">
                        <strong>我要开店(申请代理,管理代理)：</strong></font></td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="Label2_IWillOpen" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>o2o店面地址：</strong></font></td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="Label_o2oShopLink" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>购物车：</strong></font></td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="Label_Cart" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>帐户总览(我)：</strong></font></td>
                <td bgcolor="#ecf5ff">


                    <asp:Label ID="Label_MyWeBuy" runat="server" Text=""></asp:Label>

                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>总代理收入：</strong></font></td>
                <td bgcolor="#ecf5ff">


                    <asp:Label ID="Label_Multibutton_Customer" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>帐户余额：</strong></font></td>
                <td bgcolor="#ecf5ff">


                    <asp:Label ID="Label_Multibutton_showmoneydata" runat="server" Text=""></asp:Label>

                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>海报地址：</strong></font></td>
                <td bgcolor="#ecf5ff">


                    <asp:Label ID="Label1Poster" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>代理证书地址：</strong></font></td>
                <td bgcolor="#ecf5ff">


                    <asp:Label ID="Label_MarkerErWeiMaShow" runat="server" Text=""></asp:Label>

                </td>
            </tr>
           
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>拨打电话：</strong></font></td>
                <td bgcolor="#ecf5ff">


                    <asp:Label ID="Label_DialTel" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>发短信：</strong></font></td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="Label_DialSMS" runat="server" Text=""></asp:Label>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td>
                    <font face="宋体">
                        <strong>业绩排行榜：</strong></font></td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="Label_SalesOrder" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>QQ在线服务：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_LinkWeiXin0" runat="server" NavigateUrl="QQMake/GetPic.aspx">上传关联商户的QQ推广二维码，提供在线服务</asp:HyperLink>
                    &nbsp;&nbsp;
                    <asp:Label ID="Label_LinQQ" runat="server" Text="关联手机QQ，迅即和用户沟通交流！" ForeColor="#663300"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>微信在线服务：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_LinkWeiXin" runat="server" Target="_blank">商户个人的微信推广二维码请到基本资料中上传</asp:HyperLink>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>购物券兑换中心：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_GouWuQuanChange" runat="server" Target="_blank">购物券兑换现金 或者 积分</asp:HyperLink>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>现场活动：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink101Shake_Parter" runat="server" Target="_blank">现场活动 现场抽奖</asp:HyperLink>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>自助付款：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_PaySelf" runat="server" Target="_blank">用户自助付款,适合餐饮业等流量大的场所</asp:HyperLink>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="Image1_PaySelf" runat="server" ToolTip="二维码素材" Width="60px" />
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>微团购：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_WeiTuanGou" runat="server" Target="_blank">微分销团购</asp:HyperLink>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>会员充值：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink1_InputMoney" runat="server" Target="_blank">会员充值</asp:HyperLink>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" style="font-weight: 700;">
                    <strong>在线报名总览：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:HyperLink ID="HyperLink_OnlineList" runat="server" Target="_blank">在线报名总览</asp:HyperLink>
                </td>
            </tr>



            
            <%=pub__addGoodAndGoodClassShortUrl%>
        </table>
    </form>
</body>
</html>
