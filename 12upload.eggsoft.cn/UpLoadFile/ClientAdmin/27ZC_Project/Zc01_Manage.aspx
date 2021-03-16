<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zc01_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin._27ZC_Project.Zc01_Manage1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../Control/WebUC_DateTime.ascx" TagName="WebUC_DateTime" TagPrefix="uc1" %>
<%@ Register Src="../../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>众筹详情页面</title>
    <script src="../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../../Upload_JS/ckeditor/ckeditor.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../../Upload_JS/jquery-2.0.3.min.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../../Upload_JS/ckfinder/ckfinder.js?version=js201709121928" type="text/javascript"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Images/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Images/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Textbox_Timer0").calendar();
            $("#Textbox_Timer1").calendar();
        });
    </script>
    <style type="text/css">
        .border input {
        }

        .auto-style1 {
            color: #CC0000;
        }

        .auto-style2 {
            height: 35px;
        }
    </style>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
                        align="center" border="0">

                        <tr class="title">
                            <th align="center" colspan="2" height="25">
                                <strong>产&nbsp; 品 &nbsp;信 &nbsp;息</strong>
                            </th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>相关商品详情：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:DropDownList ID="DropDownList_Goods" Height="20px"
                                    Width="201px" runat="server">
                                </asp:DropDownList>

                            &nbsp;平台不支持众筹库存量/销量自动统计</td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>众筹目标金额：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="Textbox_DestinationMoney" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_DestinationMoney"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="众筹目标金额不能为空!"
                                    ControlToValidate="Textbox_DestinationMoney" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>众筹结束时间：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" class="auto-style2">

                                <input type="text" value="<%=strTextbox_Timer1_Text%>" maxlength="100" id="Text_SecondBuyEndWhenEndAllGroup"
                                    name="Text_SecondBuyEndWhenEndAllGroup" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 0, -150)"
                                    class="calendarFocus l_input" readonly="true" class="calendarFocus l_input" style="cursor: pointer" /><asp:Literal ID="Literal1" runat="server" Text="指定时间结束众筹"></asp:Literal>
                            </td>
                        </tr>
                      
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>&nbsp;<strong>众筹上架状态：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:RadioButtonList ID="RadioButtonList_IsSaled" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">上架</asp:ListItem>
                                    <asp:ListItem Value="0">下架</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>


                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>众筹原因：</strong><br />
                                （我们为什么众筹）
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_content_ZCReason" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px"  CssClass="l_input" OnInit="TextBox_content_ZCReason_Init" />
                                <span class="auto-style1">*</span></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>承诺与回报：</strong><br />
                                （任何众筹都不得向投资人承诺现金回报）
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_content_ZCPromiseAndReturn" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" CssClass="l_input" OnInit="TextBox_content_ZCPromiseAndReturn_Init" />
                                <span class="auto-style1">*</span></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>众筹描述：</strong><br />
                                （众筹规则描述）
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_content_ZCDescribe" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" CssClass="l_input" OnInit="TextBox_content_ZCDescribe_Init" />
                                <span class="auto-style1">*</span></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>排序位置：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="txtMenuPos" runat="server" Width="89px" ToolTip="数字越大 排序越靠后" CssClass="l_input">0</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="排序位置不能为空 必须是数字"
                                    ControlToValidate="txtMenuPos" ValidationExpression="^[0-9]*$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="排序位置不能为空!"
                                    ControlToValidate="txtMenuPos" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMenuPos"
                                    Display="Dynamic" ErrorMessage="范围0-1000" MaximumValue="1000" MinimumValue="0"
                                    Type="Integer" ForeColor="#FF3300"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="right" height="45" class="style4">&nbsp;
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="45">
                                <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="100px" OnClientClick="return CheckClientValidate();"
                                    OnClick="btnAdd_Click" CssClass="b_input"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    &nbsp;
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            var ckeditor; //定义全局变量 ckeditor
            $(function () {//当全部DOM元素加载完毕后执行下面语句，不加此句javascript将无法找到TextBox1
                //var aa = "";
                var ckeditorTextBox_content_ZCReason = CKEDITOR.replace("<%=TextBox_content_ZCReason.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditorTextBox_content_ZCReason, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合

                var ckeditorTextBox_content_ZCPromiseAndReturn = CKEDITOR.replace("<%=TextBox_content_ZCPromiseAndReturn.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditorTextBox_content_ZCPromiseAndReturn, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合

                var ckeditorTextBox_content_ZCDescribe = CKEDITOR.replace("<%=TextBox_content_ZCDescribe.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditorTextBox_content_ZCDescribe, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合


            });



        </script>
    </form>
</body>
</html>