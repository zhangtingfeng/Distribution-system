<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscribe.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.Subscribe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关注时回复</title> 
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" class="altrowstable"  >
        <tr class="title"><th>关注时回复</th></tr>
            
            <tr>
                <td>
                    网页请输入网址。消息回复。请在素材编辑中查找ID并输入。<a target="_blank" href="Resource-1.aspx">查看文本消息</a> 
                    <a target="_blank" href="Resource-2.aspx">查看单图文</a> <a target="_blank" href="Resource-3.aspx">查看多图文</a> <br />
                    <br />
                    <table style="width:100%;">
                        <tr>
                            <td class="stylePercent40">
                                菜单类型：</td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList_View_Click" runat="server" 
                                    RepeatDirection="Horizontal">
                                   <asp:ListItem Value="1">文本回复</asp:ListItem>
                                   <asp:ListItem Selected="True" Value="2">单图文回复</asp:ListItem>
                                    <asp:ListItem Value="3">多图文回复</asp:ListItem>
                                    <%--<asp:ListItem Value="6">图文消息</asp:ListItem>--%>
                                   <%-- <asp:ListItem Value="6">语音消息</asp:ListItem>
                                    <asp:ListItem Value="7">视频消息</asp:ListItem>
                                    <asp:ListItem Value="8">音乐消息</asp:ListItem>--%>
                                 </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylePercent40">
                                菜单内容：</td>
                            <td>
                                <asp:TextBox ID="TextBox_MenuContent" runat="server" Width="296px" 
                                    ToolTip="请输入http网址 或者 相应素材ID" CssClass="l_input"></asp:TextBox>
                                					<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" 
                                            ErrorMessage="请输入相应素材ID" ControlToValidate="TextBox_MenuContent"></asp:RequiredFieldValidator>
	
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_MenuContent" 
                                    runat="server" ErrorMessage="只能输入数字" ControlToValidate="TextBox_MenuContent" 
                                    ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                					<asp:RequiredFieldValidator id="RequiredFieldValidator_TextBox_MenuContent" runat="server" 
                                            ErrorMessage="菜单顺序不能为空，序号小的排前面!" ControlToValidate="TextBox_MenuContent"></asp:RequiredFieldValidator>
	
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
&nbsp;&nbsp;&nbsp;           
&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button_Save" runat="server" Text="保存信息" 
                                    onclick="Button_Save_Click" CssClass="b_input" />
&nbsp;&nbsp;&nbsp;
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <p style="line-height: 1.5; word-wrap: break-word; margin-left: 10px; margin-right: 10px; color: rgb(51, 51, 51); font-family: 'Microsoft Yahei', 宋体, Tahoma, Arial; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); ">
                        &nbsp;</p>
                    <p style="line-height: 1.5; word-wrap: break-word; margin-left: 10px; margin-right: 10px; color: rgb(51, 51, 51); font-family: 'Microsoft Yahei', 宋体, Tahoma, Arial; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); ">
                        &nbsp;</p>
                    <pre style="font-style: normal; font-variant: normal; font-weight: normal; font-size: 12px; font-family: 'MicroSoft YaHei', 'Courier New', 'Andale Mono', monospace; padding: 5px 10px; border: 1px solid rgb(204, 204, 204); color: rgb(51, 51, 51); background-color: rgb(248, 248, 248); line-height: 20px; white-space: pre-wrap; word-wrap: break-word; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; margin-left: 10px; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; ">关注时回复。
1.是用户扫描公众号二维码或者查找到你的公众号名称后，进行关注。关注后会打开微信的首页面。下面出现3X5菜单。上面出现你所设置的消息。
2.如果用户已经关注，测试的办法是找到该公众号-〉右上角-〉不再关注。按照1重新操作就可以了。</pre>
                    <pre style="font-style: normal; font-variant: normal; font-weight: normal; font-size: 12px; font-family: 'MicroSoft YaHei', 'Courier New', 'Andale Mono', monospace; padding: 5px 10px; border: 1px solid rgb(204, 204, 204); color: rgb(51, 51, 51); background-color: rgb(248, 248, 248); line-height: 20px; white-space: pre-wrap; word-wrap: break-word; border-top-left-radius: 3px; border-top-right-radius: 3px; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; margin-left: 10px; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; ">
</pre>
                    <p style="line-height: 1.5; word-wrap: break-word; margin-left: 10px; margin-right: 10px; color: rgb(51, 51, 51); font-family: 'Microsoft Yahei', 宋体, Tahoma, Arial; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); ">
                        <br />
                    </p>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
