<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiKanJia_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WeiKanJia.WeiKanJia_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../Control/WebUC_DateTime.ascx" TagName="WebUC_DateTime" TagPrefix="uc1" %>
<%@ Register Src="../../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>tab_WeiKanJia</title>
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
                                <strong>砍 价  信  息</strong>
                            </th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>砍价主题：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtNameTopic" runat="server" Width="376px" CssClass="l_input" MaxLength="40"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="砍价主题不能为空!"
                                    ControlToValidate="txtNameTopic" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>起始价格：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="Textbox_StartPrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_StartPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="起始价格不能为空!"
                                    ControlToValidate="Textbox_StartPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="auto-style2" align="right" width="220">
                                <strong>最低成交价格：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:TextBox ID="TextboxEndPrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextboxEndPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="最低价格不能为空!"
                                    ControlToValidate="TextboxEndPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>每次砍价随机的最高价格：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextboxEachAction_HighPrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextboxEachAction_HighPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="每次砍价随机的最高价格不能为空!"
                                    ControlToValidate="TextboxEachAction_HighPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>每次砍价随机的最低价格：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="TextboxEachAction_LowPrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextboxEachAction_LowPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="每次砍价随机的最低价格不能为空!"
                                    ControlToValidate="TextboxEachAction_LowPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>发起砍价是否必须关注：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_MustSubscribe_Master" runat="server" Text="发起砍价是否必须关注" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>帮助砍价是否必须关注：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_MustSubscribe_Helper" runat="server" Text="帮助砍价是否必须关注" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>发起砍价是否有联系方式：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_MustAddress_Master" runat="server" Text="发起砍价是否必须输入收获地址" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>发起砍价资格：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_Agent" runat="server" Text="是否只有代理商才有资格发起砍价，否则跳至申请分销商资格页面" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>&nbsp;<strong>商品上架状态：</strong>
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
                                <strong>砍价活动终止时间：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <input
                                        type="text" value="<%=strTextbox_Timer1_Text%>" maxlength="100" id="Text-SecondBuyEnd"
                                        name="Text-SecondBuyEnd" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 0, -150)"
                                        class="calendarFocus l_input" readonly="true" class="calendarFocus l_input" style="cursor: pointer" />
                            </td>
                        </tr>
                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>砍价发起及参与规则详细描述：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_Content_KanJiaRule" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" OnInit="txtContent_KanJiaRule" CssClass="l_input" />
                                <span class="auto-style1">*</span></td>
                        </tr>
                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>参与砍价商品描述：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_KanJiaTopicDescContent" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" OnInit="txtContent_InitTopicDescContent" CssClass="l_input" />
                                <span class="auto-style1">*</span></td>
                        </tr>

                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>相关商品详情：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                  <asp:DropDownList ID="DropDownList_Goods" Height="20px"
                                    Width="201px" runat="server">
                                </asp:DropDownList>
                               
                             </td>
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
                ckeditor = CKEDITOR.replace("<%=TextBox_KanJiaTopicDescContent.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditor, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合


                ckeditor = CKEDITOR.replace("<%=TextBox_Content_KanJiaRule.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditor, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合

            });
     
            
            
            </script>
    </form>
</body>
</html>