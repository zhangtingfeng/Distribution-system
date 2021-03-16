<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="25WeiXianChang_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._25WeiXianChang._25WeiXianChang_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnnounceAdd</title>
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25"><strong>微 现 场 管 理</strong></th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>活动名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtActivityName" runat="server" Width="276px" CssClass="l_input" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextboxUserPassword0" runat="server"
                        ErrorMessage="活动名称不能为空!" ControlToValidate="txtActivityName" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style3">
                    <strong>现场活动二维码：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff" height="35">
                    <asp:DropDownList ID="DropDownList_ShowAgentErWeiMa_UserID_ByAgent" runat="server">
                    </asp:DropDownList>
                    公众平台二维码或者代理二维码</td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35">
                    <strong>大屏幕密码：</strong>
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="TextboxPassword" runat="server" TextMode="Password" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextboxUserPassword" runat="server"
                        ErrorMessage="密码不能为空!" ControlToValidate="TextboxPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>开启状态：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:CheckBox ID="CheckBox_ActivityState" runat="server" Text="本活动当前是否开启" Checked="True" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>参与必须关注：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:CheckBox ID="CheckBox_Subscribe_Must" runat="server" Text="参与活动是否必须关注公众号" Checked="True" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>重复中奖：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:CheckBox ID="CheckBox_GetBonusRepeat" runat="server" Text="现场粉丝参与本次活动是否可以重复得奖" />
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>参与者详细信息：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:CheckBox ID="CheckBox_Address_Must" runat="server" Text="参与活动是否必须有收获地址" Checked="True" />
                    （如果现场发货，请不要选择该项。选择该项后中奖奖项会自动计入订单系统）</td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>大屏幕背景图片：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Image ID="Image_Background_PIC_BigScreen" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_Background_PIC_BigScreen" runat="server" />
                   大屏幕背景图片,建议尺寸大小和演示大屏幕的分辨率一致
                </td>
            </tr>

             <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>大屏幕背景音乐(mp3)：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:HyperLink ID="HyperLinkBackground_SoundPath" runat="server"></asp:HyperLink>
                    <asp:FileUpload ID="FileUpload_Background_SoundPath" runat="server" />
                </td>
            </tr>


           <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="45">
                    <strong>大屏幕显示轨道数：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_MaxTracks" runat="server" Width="160px" CssClass="l_input">15</asp:TextBox>大屏幕显示轨道数<asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="TextBox_MaxTracks"
                        Display="Dynamic" ErrorMessage="大屏幕显示轨道数不能为空!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_MaxTracks"
                        Display="Dynamic" ErrorMessage="大屏幕显示轨道数只能是数字类型!" ValidationExpression="^\d{1,}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    &nbsp;
                </td>
            </tr>


            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="45">
                    <strong>摇动时间：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_MAXLongShakeTime" runat="server" Width="160px" CssClass="l_input">30</asp:TextBox>
                    S(秒)(价值高的奖品可设置时间长一点)<asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="TextBox_MAXLongShakeTime"
                        Display="Dynamic" ErrorMessage="摇动时间不能为空!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_MAXLongShakeTime"
                        Display="Dynamic" ErrorMessage="排列位置只能是数字类型!" ValidationExpression="^\d{1,}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    &nbsp;
                </td>
            </tr>

              <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="45">
                    <strong>摇动次数：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_CountHowMany" runat="server" Width="160px" CssClass="l_input">600</asp:TextBox>
                    次数(摇动多少次中奖,第一个用户摇动就全部停止,建议价值高的产品可适当设置更多的时间)<asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="TextBox_CountHowMany"
                        Display="Dynamic" ErrorMessage="摇动时间不能为空!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox_CountHowMany"
                        Display="Dynamic" ErrorMessage="摇动次数只能是数字类型!" ValidationExpression="^\d{1,}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    &nbsp;
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="45">
                    <strong>后台页面排序位置：</strong>
                </td>
                <td align="left" bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtClassPos" runat="server" Width="260px" CssClass="l_input">0</asp:TextBox>
                    排序从小到大，0最小<asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtClassPos"
                        Display="Dynamic" ErrorMessage="排列位置不能为空!" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtClassPos"
                        Display="Dynamic" ErrorMessage="排列位置只能是数字类型!" ValidationExpression="^\d{1,}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    &nbsp;
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">

                    <asp:Button ID="btnAdd" runat="server" Text=" 添加 " Width="72px"
                        OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
