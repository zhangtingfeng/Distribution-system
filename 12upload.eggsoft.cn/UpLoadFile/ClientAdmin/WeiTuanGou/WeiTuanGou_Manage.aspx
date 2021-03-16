<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiTuanGou_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WeiTuanGou.WeiTuanGou_Manage" %>

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
                                <strong>团&nbsp; 购&nbsp;信&nbsp;息</strong>
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
                               
                             </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>组团成功人数：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="Textbox_HowManyPeople" runat="server" Width="100px" CssClass="l_input">3</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式数字"
                                    ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_HowManyPeople" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="组团成功人数不能为空!"
                                    ControlToValidate="Textbox_HowManyPeople" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="Textbox_HowManyPeople"
                                    Display="Dynamic" ErrorMessage="范围0-1000" MaximumValue="1000000" MinimumValue="1"
                                    Type="Integer" ForeColor="#FF3300"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>组团价格：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="Textbox_EachPeoplePrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_EachPeoplePrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="组团价格不能为空!"
                                    ControlToValidate="Textbox_EachPeoplePrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="auto-style2" align="right" width="220">
                                <strong>代理商利润：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:TextBox ID="TextboxAgentPrice" runat="server" Width="100px" CssClass="l_input">0</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="TextboxAgentPrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="代理商利润不能为空!"
                                    ControlToValidate="TextboxAgentPrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="auto-style2" align="right" width="220">
                                 <strong>组团成功团长奖励：</strong></td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                <asp:CheckBox ID="CheckBox_TuanZhang_AgentGet" runat="server" Text="取得代理商利润（三级分销所得全部转给团长）" />
                                (由于团长不是代理商，所以代理商统计中不显示团长所得)<br />
                                  <asp:TextBox ID="Textbox1_TuanZhang_GouWuQuan" runat="server" Width="100px" CssClass="l_input">0</asp:TextBox>组团成功奖励购物券,0表示不奖励
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox1_TuanZhang_GouWuQuan"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="组团价格不能为空!"
                                    ControlToValidate="Textbox1_TuanZhang_GouWuQuan" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                
                                  <asp:TextBox ID="Textbox2_TuanZhang_Money" runat="server" Width="100px" CssClass="l_input">0</asp:TextBox>组团成功奖励现金,0表示不奖励
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox2_TuanZhang_Money"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="组团价格不能为空!"
                                    ControlToValidate="Textbox2_TuanZhang_Money" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="auto-style2" align="right" width="220">
                                <strong>组团结束条件：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" class="auto-style2">
                                        <asp:CheckBox ID="CheckBox_HowmanyHoursEnd" runat="server" Text="开团多少小时后自动结束" Checked="True" />
                                <asp:TextBox ID="Textbox_HowmanyHoursEnd" runat="server" Width="100px" CssClass="l_input">24</asp:TextBox>
                                （开团指定小时后未达到指定人数由客服介入处理退款事宜）<asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="格式数字"
                                    ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_HowmanyHoursEnd" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="不能为空!"
                                    ControlToValidate="Textbox_HowmanyHoursEnd" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="Textbox_HowmanyHoursEnd"
                                    Display="Dynamic" ErrorMessage="范围1-10000" MaximumValue="10000" MinimumValue="1"
                                    Type="Integer" ForeColor="#FF3300"></asp:RangeValidator><br /><asp:CheckBox ID="CheckBoxWhenEndAllGroup" runat="server" Text="组团终止时间" />

                                <input
                                        type="text" value="<%=strTextbox_Timer1_Text%>" maxlength="100" id="Text_SecondBuyEndWhenEndAllGroup"
                                        name="Text_SecondBuyEndWhenEndAllGroup" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 0, -150)"
                                        class="calendarFocus l_input" readonly="true" class="calendarFocus l_input" style="cursor: pointer" /><asp:Literal ID="Literal1" runat="server" Text="指定时间结束全部拼团;成功组团可发货,失败组团客服介入处理"></asp:Literal>
                            </td>
                        </tr>
                         <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>发起组团是否必须关注：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_MustSubscribe_Master" runat="server" Text="发起组团（成为团长）是否必须关注" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>参与组团是否必须关注：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_MustSubscribe_Helper" runat="server" Text="参与组团（成为团员）是否必须关注" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>成为团员是否有联系方式：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_MustAddress_Master" runat="server" Text="成为团员是否必须输入收获地址" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>发起组团资格：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_Agent" runat="server" Text="是否只有代理商才有资格发起组团（成为团长），否则跳至申请代理分销商资格页面" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>是否允许购买多个：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_BuyMultiOnlyOneAccount" runat="server" Text="同一微信号（本店不支持一个手机切换微信号）是否可以在一次团购活动中购买多个。（在购物车/订单表中只能购买一次）" />
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
                                <strong>团购描述：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_Content_TuanFouRule" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" OnInit="txtContent_TuanFouRule" CssClass="l_input" />
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
                ckeditor = CKEDITOR.replace("<%=TextBox_Content_TuanFouRule.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditor, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合
                          
            });
     
            
            
            </script>
    </form>
</body>
</html>