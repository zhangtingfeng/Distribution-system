<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01BoardINC_Manage_ShopClient_ShopPar.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient._01BoardINC_Manage_ShopClient_ShopPar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <style type="text/css">
        .border input {
        }

        .auto-style1 {
            height: 35px;
        }
    </style>
    <title>公司会员资料</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title" bgcolor="#a4b6d7">
                <th align="left" bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25">相关参数
                </th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每个有效转发店铺奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Price_MoneyShopShareGiveMoney" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Textbox_Price_MoneyShopShareGiveMoney" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ControlToValidate="Textbox_Price_MoneyShopShareGiveMoney" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每个有效转发店铺奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_GouWuQuan_ShopShareGiveVouchers" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Textbox_GouWuQuan_ShopShareGiveVouchers" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Textbox_GouWuQuan_ShopShareGiveVouchers" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每个有效转发商品奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Good_Money" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Textbox_Good_Money" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Textbox_Good_Money" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每个有效转发商品奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Good_GouWuQuan" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Textbox_Good_GouWuQuan" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Textbox_Good_GouWuQuan" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>关注公众号奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Money_Subs" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Textbox_Money_Subs" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Textbox_Money_Subs" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>关注公众号奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox2_GouWuQuan_Subs" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="Textbox2_GouWuQuan_Subs" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Textbox2_GouWuQuan_Subs" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>首次访问商铺奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Money_FirstVisit" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="Textbox_Money_FirstVisit" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Textbox_Money_FirstVisit" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>首次访问商铺奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_GouWuQuan_FirstVisit" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="Textbox_GouWuQuan_FirstVisit" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Textbox_GouWuQuan_FirstVisit" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每日分享商铺奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ShareShopXianJin_EveryDay" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="Textbox_ShareShopXianJin_EveryDay" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Textbox_ShareShopXianJin_EveryDay" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每日分享商铺奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ShareShopGouWuQuan_EveryDay" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="Textbox_ShareShopGouWuQuan_EveryDay" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="Textbox_ShareShopGouWuQuan_EveryDay" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>


            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每日分享商品奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ShareGoodXianJin_EveryDay" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="Textbox_ShareGoodXianJin_EveryDay" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="Textbox_ShareGoodXianJin_EveryDay" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每日分享商品奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ShareGoodGouWuQuan_EveryDay" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="Textbox_ShareGoodGouWuQuan_EveryDay" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="Textbox_ShareGoodGouWuQuan_EveryDay" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>未关注服务号提示文字：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_SubscribeTipInfo" runat="server" CssClass="l_input" Width="792px" MaxLength="255">亲,您还没有关注我们,点这里开始关注.</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Textbox_SubscribeTipInfo" ErrorMessage="不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>申请代理批准方式：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBoxList ID="CheckBoxList_AskAgent" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Enabled="False">自动审批（用户申请后，无条件自动给予代理分销权）</asp:ListItem>
                        <asp:ListItem Value="2">购买任何一款商品给予代理分销权</asp:ListItem>
                    </asp:CheckBoxList>
                    <%--功能暂时关闭--%>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>支付前必须输入收货地址：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_PayMoneyMustHaveAddress" runat="server" Text="支付前必须输入收货地址" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>首页只显示轮播图：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_OnlyShowLunBoTu" runat="server" Text="首页只显示轮播图" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>提现限制：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">最少多少元起提： 
                    <asp:TextBox ID="Textbox_LimitMoney" runat="server" CssClass="l_input" Width="100px">1.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="Textbox_LimitMoney" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Textbox_LimitMoney" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="Textbox_LimitMoney" ErrorMessage="微信企业转账规定必须大于1元" MaximumValue="9999999" MinimumValue="1"></asp:RangeValidator>
                    <br />
                    提现频率限制单位： 
                    <asp:RadioButtonList ID="RadioButtonList_LimitMoney_PresentFrequency" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="Unlimited">不限制</asp:ListItem>
                        <asp:ListItem Value="Hourly">每时</asp:ListItem>
                        <asp:ListItem Value="Daily">每天</asp:ListItem>
                        <asp:ListItem Value="Weekly">每周</asp:ListItem>
                        <asp:ListItem Value="Monthly">每月</asp:ListItem>
                        <asp:ListItem Value="Quarterly">每季度</asp:ListItem>
                        <asp:ListItem Value="Annually">每年</asp:ListItem>
                    </asp:RadioButtonList>
                         <br />
                    每(频率)最多多少元可提： 
                    <asp:TextBox ID="Textbox_LimitMoney_MAX" runat="server" CssClass="l_input" Width="100px">100.00</asp:TextBox>
                    <span class="auto-style1">*</span>0表示不限制<asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" ControlToValidate="Textbox_LimitMoney_MAX" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="Textbox_LimitMoney_MAX" ErrorMessage="最多多少可提不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="Textbox_LimitMoney_MAX" ErrorMessage="微信企业转账规定必须大于1元" MaximumValue="9999999" MinimumValue="1"></asp:RangeValidator>
                    <br />
                  每(频率)提现次数： <asp:TextBox ID="Textbox_LimitMoney_OnceEveryDay" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>0表示不限制
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_LimitMoney_OnceEveryDay" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="每(频率)提现次数不能为空!"
                        ControlToValidate="Textbox_LimitMoney_OnceEveryDay" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CheckBox ID="CheckBox_BankTime" runat="server" Text="只在银行工作日提现9：30-16：30" />
           
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>用户申请提现后马上转账：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_GiveMoneyAfterOntime" runat="server" Text="用户申请提现后马上转账,是否秒转" />
                </td>
            </tr>
          <%--  <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>自己购买商品是否享受分销提成：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_BuyMySelfIfGetMoney" runat="server" Text="在自己的代理店铺自己购买是否享受分销提成" />
                </td>
            </tr>--%>
            <%--<tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>是否使用顶级代理分销模式：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_TopAgent" runat="server" Text="顶级代理是否获得所有的代理利润（取消勾选只获取分级代理中的相应级别的部分）" />
                </td>
            </tr>--%>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>商品页面的总价包含运费：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_GoodsShowYunFei" runat="server" Text="商品页面的总价包含运费（是否实时显示商品页面的运费）" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>代理商是否重新挑选商品：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_AutoMidifyAgentGoods" runat="server" Text="更新商品时自动更新代理商的经销商品范围，代理商不用重新挑选商品" />
                </td>
            </tr>
           <%-- <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>分销提成优先级：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_ShareFirstManORLastMan" runat="server" Text="分销所得优先给予第一人还是给予最新的转发人。（举例：A转发给B，2天后C也转发给B。然后B购买了商品。B的上线是A还是C？选择表示上线是A，不选择表示上线是C。）" />
                </td>
            </tr>--%>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" class="auto-style1">
                    <strong>接收消息选项：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" class="auto-style1">
                    <div align="left">
                        <asp:CheckBoxList ID="CheckBoxLis_AcceptMsgList" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="false">接收用户付款通知(EMail)</asp:ListItem>
                            <asp:ListItem Selected="false">接收用户浏览商城通知(微信)</asp:ListItem>
                            <asp:ListItem Selected="false">接收用户即将付款通知(微信)</asp:ListItem>
                            <asp:ListItem Selected="false">接收用户付款通知(微信)</asp:ListItem>
                            <asp:ListItem Selected="false">o2o收浏览商品消息(微信)</asp:ListItem>
                            <asp:ListItem Selected="false">o2o接收即将付款通知(微信)</asp:ListItem>
                            <asp:ListItem Selected="false">o2o接收付款通知(微信)</asp:ListItem>
                            <asp:ListItem Selected="false">接收游戏消息</asp:ListItem>
                            <asp:ListItem Selected="false">接收微砍价消息</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>浏览消息启用微信模板(访客消息通知)：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_TempletVisitMessage" runat="server" Text="访客消息通知.必须在开发模式中设置访客消息通知 模板ID，才能启用该项（访客消息通知 模板ID是在微信公众平台取得的）。分销平台默认的图文消息或者文本消息发送不成功是否启用该项。" />
                    。<a href="https://mp.weixin.qq.com/wiki/2/def71e3ecb5706c132229ae505815966.html">为何启用模版消息。</a></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>浏览消息启用微信模板(成功付款通知)：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_TemplePayMessage" runat="server" Text="成功付款通知.必须在开发模式中设置成功付款通知 模板ID，才能启用该项（成功付款通知 模板ID是在微信公众平台取得的）。分销平台默认的图文消息或者文本消息发送不成功是否启用该项。" />
                    。<a href="https://mp.weixin.qq.com/wiki/2/def71e3ecb5706c132229ae505815966.html">为何启用模版消息。</a></td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>启用微信多客服：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_weixinMultiDuoKeFu" runat="server" Text="微信公众平台中启用多客服服务可启用该服务" />
                    。<a href="https://mp.weixin.qq.com/wiki/5/ae230189c9bd07a6b221f48619aeef35.html">将消息转发到多客服。</a></td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>启用新版微信支付：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_V3_js_API" runat="server" Text="启用新版微信支付V3版本（js API支付）" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>启用一键支付：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_Quick" runat="server" Text="启用一键快捷支付（客户点击商品页面的立即购买，可快捷弹出微信支付（如果条件具备、譬如收货地址已存在的情况下））" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>购物券名称：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_VouchersShopName" runat="server" CssClass="l_input" Width="200px" MaxLength="6">购物券</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price1" runat="server" ControlToValidate="Textbox_VouchersShopName" ErrorMessage="购物券不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>代理店铺描述：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_AgentShopTextDesc" runat="server" CssClass="l_input" Width="200px" MaxLength="20">代理店铺</asp:TextBox>
                    代理店铺/我为***代言<asp:RequiredFieldValidator ID="RequiredFieldValidator_Price0" runat="server" ControlToValidate="Textbox_AgentShopTextDesc" ErrorMessage="代理店铺不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>提现是否强制分享朋友圈：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_UserDrawMoneyShareFriend" runat="server" Text="用户申请提现必须分享朋友圈。分享朋友圈提现 有利于推广商城。" />
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每日签到奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_SignWorkingEveryDay_Money" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="Textbox_SignWorkingEveryDay_Money" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="Textbox_SignWorkingEveryDay_Money" ErrorMessage="每日签到奖励现金不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每日签到奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_SignWorkingEveryDay_GouWuQuan" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="Textbox_SignWorkingEveryDay_GouWuQuan" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="Textbox_SignWorkingEveryDay_GouWuQuan" ErrorMessage="每日签到奖励购物券不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>扫描代理二维码奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ScanAgentErWeiMaMoney" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="Textbox_ScanAgentErWeiMaMoney" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="Textbox_ScanAgentErWeiMaMoney" ErrorMessage="扫描代理二维码奖励现金不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>扫描代理二维码奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ScanAgentErWeiMaGouWuQuan" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="Textbox_ScanAgentErWeiMaGouWuQuan" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="Textbox_ScanAgentErWeiMaGouWuQuan" ErrorMessage="扫描代理二维码奖励购物券不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>商品分享量基数：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_GoodShareBase" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_GoodShareBase" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="商品分享量基数不能为空!"
                        ControlToValidate="Textbox_GoodShareBase" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                    &nbsp;

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>商品点赞量基数：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_HitCount" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_HitCount" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="商品点赞量基数不能为空!"
                        ControlToValidate="Textbox_HitCount" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>商品分享人数基数：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_SharePeopleNum" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_SharePeopleNum" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="商品分享人数基数不能为空!"
                        ControlToValidate="Textbox_SharePeopleNum" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>商品访问人数基数：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_VisitPeopleBaseNum" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_VisitPeopleBaseNum" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="商品访问人数基数不能为空!"
                        ControlToValidate="Textbox_VisitPeopleBaseNum" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>自助付款商品编号：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox1DoSelf_51_GoodID" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox1DoSelf_51_GoodID" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="商品自助付款商品编号不能为空!"
                        ControlToValidate="Textbox1DoSelf_51_GoodID" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>会员充值商品编号：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox1InputMoney_GoodID" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox1InputMoney_GoodID" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="会员充值商品编号不能为空!"
                        ControlToValidate="Textbox1InputMoney_GoodID" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>是否关闭购物券（积分）红包分享功能：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_CloseShareGouWuQuan" runat="server" Text="代理 分销商之间购物券可能不对等，使用代理功能的商户需要关闭积分红包功能" />
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>是否关闭现金红包分享功能：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox1_CloseShareXianJinHongBao" runat="server" Text="现金红包可能会被用户用来收集购买大额产品，如有需要可以关闭" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>天使分销功能：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_EveryOneAutoAgentOnlyIsAngel" runat="server" Text="天使分销功能，对标微信小店功能，任何访问都自动给予分销权，不过只有提出代理申请的用户才能参与分销提成、团队奖励。否侧给予相应的购物券奖励" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>是否关闭商品分享访问头像及统计：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_CloseGoodsShareAndStatus" runat="server" Text="商品页面的分享访问头像及统计可关闭，商户根据自身需要可选择勾选关闭" />
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每个有效转发咨询奖励现金：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_BonusMoney_ShareGuidePages" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="Textbox_BonusMoney_ShareGuidePages" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="Textbox_BonusMoney_ShareGuidePages" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>每个有效转发咨询奖励购物券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_BonusGouWuQuan_ShareGuidePages" runat="server" CssClass="l_input" Width="100px">0.00</asp:TextBox>
                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="Textbox_BonusGouWuQuan_ShareGuidePages" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="Textbox_BonusGouWuQuan_ShareGuidePages" ErrorMessage="价格不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>是否启用优惠券及购物红包功能：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff" align="left">
                    <asp:CheckBox ID="CheckBox_Shopping_Vouchers" runat="server" Text="是否启用优惠券及购物红包功能" Checked="True" />
                    <br />
                    优惠券及购物红包都不能提现,只能购物使用。优惠券都有号码,顾客输入号码使用，分发方法可采用线下分发。优惠券的金额在商家所得去除。举例：如一件商品100元，代理商利润设为20.优惠券使用了5元。那么商家所得是100-20-5=75。顾客需要支付95元现金。20元是代理所得。</td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>扫描代理证书是否推送优惠券：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_SendYouHuiQuanID" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    需要输入线上发放的优惠券ID号码(应用管理-》优惠券-》编号)。空或者0表示不推送<asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_SendYouHuiQuanID" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="Textbox_SendYouHuiQuanID" ErrorMessage="优惠券ID编号不能为空!" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>消费财富运营中心默认配置编号（中心ID）：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_ConsumptionCapital_OperationCenterID" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_ConsumptionCapital_OperationCenterID" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="运营中心默认配置编号（USRID）编号不能为空!"
                        ControlToValidate="Textbox_ConsumptionCapital_OperationCenterID" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>消费财富批准运营中心关联代理编号：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_YunYingZhongXin_AdvanceAgentID" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_YunYingZhongXin_AdvanceAgentID" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="批准运营中心关联代理编号不能为空!"
                        ControlToValidate="Textbox_YunYingZhongXin_AdvanceAgentID" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    运营中心必须有代理商资质。批准运营中心可自动添加代理商资质，并推送代理证书。新用户扫描该代理证书，可自动加入该运营中心</td>
            </tr>


              <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right">
                    <strong>为轮播图设置固定高度(px)：</strong>
                </td>
                <td width="80%" height="35" bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_tab_AnnouncePic" runat="server" CssClass="l_input" Width="100px">0</asp:TextBox>
                    <span class="auto-style1">*</span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_tab_AnnouncePic" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="为轮播图设置固定高度不能为空!"
                        ControlToValidate="Textbox_tab_AnnouncePic" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                </td>
            </tr>



        </table>

        <p style="text-align: center">
            <asp:Button ID="btnAdd" runat="server" Text=" 保存 " Width="100px" OnClick="btnAdd_Click"
                CssClass="b_input"></asp:Button>
        </p>
    </form>
</body>
</html>
