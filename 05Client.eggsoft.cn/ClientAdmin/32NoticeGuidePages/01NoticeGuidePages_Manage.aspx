<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01NoticeGuidePages_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._32NoticeGuidePages._01NoticeGuidePages_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>公告管理</title>
    <script src="../../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Image/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />


    <style type="text/css">
        .border input {
            height: auto;
        }

        #CheckBoxList_GoodList {
            width: 98%;
            margin: 0px auto;
        }

            #CheckBoxList_GoodList input {
                padding-left: 10px;
                margin-left: 10px;
            }

            #CheckBoxList_GoodList label {
                padding-left: 4px;
            }
    </style>

    
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">
                    <strong>公 告 管 理</strong>
                </th>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>公告名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="b016_NoticeGuidePages_Title" runat="server" Width="376px" CssClass="l_input" MaxLength="50"></asp:TextBox>
                    <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="公告名称不能为空!" ControlToValidate="b016_NoticeGuidePages_Title"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>公告URL地址：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox1_Linkurl" runat="server" Width="376px" CssClass="l_input" MaxLength="250"></asp:TextBox>
                    <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ErrorMessage="公告URL地址不能为空!" ControlToValidate="TextBox1_Linkurl"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1111" runat="server" ErrorMessage="格式url"
                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" ControlToValidate="TextBox1_Linkurl" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    （友情提示。公告可利用咨询管理制作，粘贴URL地址到这里，或者利用微信公众平台制作出来的链接也可以，总之用户触摸公告可以打开看到一篇文章，文章里面显示详细的公告提示。）</td>
            </tr>
             <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>公告是否生效：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:CheckBox ID="CheckBox_Active" runat="server" Checked="True" />
                    公告是否生效。前端只显示生效的公告</td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>排序位置：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="Textbox_Pos" runat="server" Width="76px" CssClass="l_input"
                        MaxLength="50">0</asp:TextBox>
                    <span class="style1"><strong>*</strong></span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="格式数字"
                        ValidationExpression="^[0-9]*$" ControlToValidate="Textbox_Pos" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="排序位置不能为空!"
                        ControlToValidate="Textbox_Pos" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    &nbsp;越大的数字越排在后面</td>
            </tr>
            
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">
                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"
                        CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
