<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Good_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.Good_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../Control/WebUC_DateTime.ascx" TagName="WebUC_DateTime" TagPrefix="uc1" %>
<%@ Register Src="../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Goods Add</title>
    <script src="Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../Upload_JS/ckeditor/ckeditor.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../Upload_JS/jquery-2.0.3.min.js?version=js201709121928" type="text/javascript"></script>
    <script src="../../Upload_JS/ckfinder/ckfinder.js?version=js201709121928" type="text/javascript"></script>

    <link href="skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="Images/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="Images/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
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
    </style>
    <script type="text/javascript">



        $(window).load(function () {
            var n = document.getElementById("tab").rows.length;

            document.getElementById("RowLength").value = document.getElementById("tab").rows.length;
        });

        function delrow() {

            //var i = document.getElementById("tab").rowIndex;  
            var n = document.getElementById("tab").rows.length;
            //     if (n!=1)//if not one line just delete line  
            //   {  
            document.getElementById("tab").deleteRow(n - 1);

            document.getElementById("RowLength").value = document.getElementById("tab").rows.length;

            // }  
            //else//if table have one line can not do delete  
            //{  
            //   alert("just one line can not delete");  
            //}  

            //  var newrow = document.getElementById('tab').insertRow();
            // var newcel = newrow.insertCell();
            // newcel.innerHTML = newrow.rowIndex;
            // newcel = newrow.insertCell();
            // newcel.innerHTML = "<td>选择种类<input id=\"Text1\" type=\"text\"/> 价格<input name= id=\"Text3\" type=\"text\"/></td>";

        }

        function addrow() {
            var varlength = document.getElementById('tab').rows.length;
            var newrow = document.getElementById('tab').insertRow(varlength);
            var newcel = newrow.insertCell();
            // newcel.innerHTML = newrow.rowIndex;
            newcel = newrow.insertCell();
            newcel.innerHTML = "<td>选择种类<input id=\"Text_Choice_Name" + varlength + "\" name=\"Text_Choice_Name" + varlength + "\" type=\"text\"/> 价格<input id=\"Text_Price_Num" + varlength + "\" name=\"Text_Price_Num" + varlength + "\"  type=\"text\"/></td>";
            document.getElementById("RowLength").value = document.getElementById("tab").rows.length;

        }
        function uploadComplete(sender, args) {
            var varIDIndex = "<%=Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users")%>";
            var fileName = args.get_fileName().lastIndexOf('.');
            var a = args.get_fileName().substring(fileName).toLowerCase();
            if (a == ".jpg" || a == ".jpeg" || a == ".png" || a == ".bmp" || a == ".gif" || a == ".ico" || a == ".gif") {
                $("#Image_Small").attr("src", "/upload/TempUpload/" + varIDIndex + "_" + args.get_fileName());
            }
        }

        function uploadError(sender, args) {
            alert("上传错误");
        }


        function CheckClientValidate() {
            if (Page_ClientValidate()) {
                var srcUp = $("#Image_Small").attr("src");
                if (srcUp.toString().length == 0) {
                    alert("相关图片必须选择！");
                    $("#Image_Small").focus();
                    return false;
                }
                return true;
            }
        }

    </script>
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
                                <strong>商 品  信  息</strong>
                            </th>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right" width="220">
                                <strong>商品名称：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtName" runat="server" Width="376px" CssClass="l_input" MaxLength="40"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="标题不能为空!"
                                    ControlToValidate="txtName" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>价格：</strong>
                            </td>
                            <td height="70" bgcolor="#ecf5ff">
                                <asp:Label ID="Label_MarketPrice" runat="server" Text="市场价格¥："></asp:Label>
                                <asp:TextBox ID="Textbox_Price" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_Price"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Price" runat="server" ErrorMessage="价格不能为空!"
                                    ControlToValidate="Textbox_Price" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="打折价格¥："></asp:Label>
                                <asp:TextBox ID="Textbox_PromotePrice" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_PromotePrice"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="打折价格不能为空!"
                                    ControlToValidate="Textbox_PromotePrice" ForeColor="#FF3300"></asp:RequiredFieldValidator>

                                <br />
                                <asp:Label ID="Label2" runat="server" Text="代理商利润¥："></asp:Label>
                                <asp:TextBox ID="Textbox_AgentPercentMoney" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_AgentPercentMoney"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="代理商利润不能为空!"
                                    ControlToValidate="Textbox_AgentPercentMoney" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Panel ID="Panel_GouWuQuan" runat="server">
                                    <asp:Label ID="Label3" runat="server" Text="购物券最大允许金额¥："></asp:Label>
                                    <asp:TextBox ID="Textbox_Total_Vouchers_Consume_Or_Recharge" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="格式000.00"
                                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_Total_Vouchers_Consume_Or_Recharge"
                                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="购物券最大允许金额不能为空!"
                                        ControlToValidate="Textbox_Total_Vouchers_Consume_Or_Recharge" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    <asp:Label ID="Label4" runat="server" Text="你已启用购物券及购物红包功能，本项必需填写"></asp:Label>
                                </asp:Panel>
                                 <br />
                                <asp:Panel ID="Panel_CaiFuJiFen" runat="server">
                                    <asp:Label ID="Label5" runat="server" Text="财富积分最大允许金额¥："></asp:Label>
                                    <asp:TextBox ID="Textbox_Total_CaiFuJiFen_Consume_Or_Recharge" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="格式000.00"
                                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_Total_CaiFuJiFen_Consume_Or_Recharge"
                                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="财富积分最大允许金额不能为空!"
                                        ControlToValidate="Textbox_Total_CaiFuJiFen_Consume_Or_Recharge" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    <asp:Label ID="Label6" runat="server" Text="你已启用财富积分购买功能，本项必需填写"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>商品单位：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:DropDownList ID="DropDownList_Unit" runat="server" Height="20px" Width="101px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>商品图片：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <uc2:Upload_MultiSeclect ID="Upload_MultiSeclect2" runat="server" MultiChoice="True" />
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>购买限制：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">最小购买数量：
                            <asp:DropDownList ID="DropDownList_MinSalesCount" runat="server" Height="20px" Width="101px">
                            </asp:DropDownList>
                                最大购买数量：
                            <asp:DropDownList ID="DropDownList_MaxSalesCount" runat="server" Height="20px" Width="101px">
                            </asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>商品重量：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">

                                <asp:TextBox ID="Textbox_GoodKg" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                Kg(单位公斤(千克),可精确到0.001,即1克；和运费设置有关，如包邮可忽略该项)<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="格式000.000"
                                        ValidationExpression="^[0-9]+(.[0-9]{3})?$" ControlToValidate="Textbox_GoodKg"
                                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>邮件模版：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">

                                <asp:DropDownList ID="DropDownList_FreightTemplet" Height="20px"
                                    Width="201px" runat="server">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3" <%=DisPlay("Shopping_Vouchers")%>>
                            <td class="style4" align="right">
                                <strong>是否使用购物券：</strong>
                            </td>
                            <td height="35" bgcolor="#ecf5ff">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="CheckBox_WeiBai_RedMoney" runat="server" Text="是否使用购物券" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>商品分类：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" height="35">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList_Class1" runat="server" Height="20px" Width="101px"
                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Class1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DropDownList_Class2" runat="server" AutoPostBack="True" Height="20px"
                                            Width="101px" Visible="False" OnSelectedIndexChanged="DropDownList_Class2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DropDownList_Class3" runat="server" Height="20px" Width="101px"
                                            Visible="False">
                                        </asp:DropDownList>
                                        <%--<asp:DropDownList ID="DropDownList_Class4" runat="server" Height="20px" Width="101px"
                                        Visible="False">
                                    </asp:DropDownList>--%>
                                        <%--  <select id="DropDownList_Class3" runat="server" visible="False" multiple="true" size="1">
                                    </select>
                                    <select id="DropDownList_Class4" runat="server" visible="False" multiple="true" size="1">
                                    </select>--%>
                                        <%--<asp:DropDownList ID="DropDownList_Class5" runat="server" Height="20px" Width="101px"
                                        Visible="False">
                                    </asp:DropDownList>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>分类列表：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" height="35">
                                <table id="tab">
                                    <asp:Literal ID="Literalmulti_Price_Line" runat="server" Text=""></asp:Literal>
                                </table>
                                <input id="RowLength" name="RowLength" style="display: none;" type='text' value='0' />
                                <input type='button' value='添加' onclick='addrow()' style="width: 70px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input type='button' value='删除' onclick='delrow()' style="width: 70px" />
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>&nbsp;<strong>售货状态：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:RadioButtonList ID="RadioButtonList_IsSaled" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">上架</asp:ListItem>
                                    <asp:ListItem Value="0">下架</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" class="style5">
                                <strong>库存量：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" class="style6">
                                <asp:TextBox ID="Textbox_KuCunLiang" runat="server" Width="100px" CssClass="l_input">10</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式数字"
                                    ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_KuCunLiang" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="库存量不能为空!"
                                    ControlToValidate="Textbox_KuCunLiang" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3" style="display:none;">
                            <td align="right" height="35" class="style4">
                                <strong>限时秒杀：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:CheckBox ID="CheckBox_DoTimer" runat="server" Text="是否限时秒杀" />
                                <asp:Panel ID="Panel_CheckBox_DoTimer" runat="server">
                                    &nbsp;开始时间：
                                <input type="text" value="<%=strTextbox_Timer0_Text%>" maxlength="100" id="Text-SecondBuyStart"
                                    name="Text-SecondBuyStart" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 0, -150)"
                                    readonly="true" class="calendarFocus l_input" style="cursor: pointer" />&nbsp;&nbsp;结束时间：<input
                                        type="text" value="<%=strTextbox_Timer1_Text%>" maxlength="100" id="Text-SecondBuyEnd"
                                        name="Text-SecondBuyEnd" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 0, -150)"
                                        class="calendarFocus l_input" readonly="true" class="calendarFocus l_input" style="cursor: pointer" />秒杀价格¥：<asp:TextBox
                                            ID="Textbox_Price_Timer" runat="server" Width="100px" CssClass="l_input"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6_Textbox_Price_Timer"
                                        runat="server" ErrorMessage="格式000.00" ValidationExpression="^[0-9]+(.[0-9]{2})?$"
                                        ControlToValidate="Textbox_Price_Timer" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Textbox_Price_Timer" runat="server"
                                        ErrorMessage="秒杀价格不能为空!" ControlToValidate="Textbox_Price_Timer" ForeColor="#FF3300"
                                        Enabled="False"></asp:RequiredFieldValidator>
                                    限时秒杀最大购买数量：<asp:DropDownList ID="DropDownList_Timer_MaxSalesCount" runat="server"
                                        Height="20px" Width="101px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>简短描述：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="TextBox_ShotInfo" runat="server" Height="47px" Width="573px" OnInit="txtContent_Init"
                                    MaxLength="120" CssClass="l_input" />
                                <font face="宋体">
                                    <span class="auto-style1">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="简短描述不能为空!！"
                                        ControlToValidate="TextBox_ShotInfo" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="不能超过50个字。"
                                        ValidationExpression="^(.){1,50}$" ControlToValidate="TextBox_ShotInfo" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>详细描述：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:TextBox ID="txtContent_LongInfo" runat="server" TextMode="MultiLine" Height="47px"
                                    Width="573px" OnInit="txtContent_Init" CssClass="l_input" />
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

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td class="style4" align="right">
                                <strong>购买本商品赠送：</strong>
                            </td>
                            <td height="70" bgcolor="#ecf5ff">
                                
                                <asp:Label ID="Label_Send_Money_IfBuy" runat="server" Text="购买本商品赠送现金¥："></asp:Label>
                                <asp:TextBox ID="Textbox_Send_Money_IfBuy" runat="server" Width="100px" CssClass="l_input">0.00</asp:TextBox>
                                <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="格式000.00"
                                    ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_Send_Money_IfBuy"
                                    ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="赠送现金不能为空!"
                                    ControlToValidate="Textbox_Send_Money_IfBuy" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Panel ID="Panel_Send_Vouchers_IfBuy" runat="server">
                                    <asp:Label ID="Label_Send_Vouchers_IfBuy" runat="server" Text="购买本商品赠送购物券"></asp:Label>
                                    <asp:TextBox ID="Textbox_Send_Vouchers_IfBuy" runat="server" Width="100px" CssClass="l_input">0.00</asp:TextBox>
                                    <span class="auto-style1">*</span><asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="格式000.00"
                                        ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="Textbox_Send_Vouchers_IfBuy"
                                        ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="购物券最大允许金额不能为空!"
                                        ControlToValidate="Textbox_Send_Vouchers_IfBuy" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                    </asp:Panel>

                            </td>
                        </tr>

                        <tr <%=DisPlay("Good_Voice")%> class="tdbg" bgcolor="#e3e3e3">
                            <td align="right" height="35" class="style4">
                                <strong>微信语音输入：</strong>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:FileUpload ID="FileUpload_Mp3" runat="server" />
                                <asp:HyperLink ID="HyperLink__Mp3path" runat="server"></asp:HyperLink>
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
                ckeditor = CKEDITOR.replace("<%=txtContent_LongInfo.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
                CKFinder.setupCKEditor(ckeditor, "../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合
            });
        </script>
    </form>
</body>
</html>