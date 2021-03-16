<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="13OperationCenter_OrderManage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._1313OperationCenter_OrderManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>收 单 管 理</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js?version=js201709121928" type="text/javascript"></script>

    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }

        .auto-style2 {
            width: 80%;
            height: 36px;
        }
    </style>

    <script type="text/javascript">
        function CheckClientValidate() {
            var a = confirm("确定提交吗？");
            if (a == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title">
                            <th align="center" colspan="2" height="25"><strong>管 理 运 营 订 单 </strong></th>
                        </tr>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>支付用户微店ID：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="txtUserID" runat="server" Width="460px" CssClass="l_input" MaxLength="200" OnTextChanged="txtUserID_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtUserID" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="微店ID不能为空!" ControlToValidate="txtUserID"></asp:RequiredFieldValidator>
                                        <asp:Image ID="Image1UserID" runat="server" ImageAlign="Middle" Width="24px" />
                                        <asp:Label ID="Label1UserID" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>支付用户真实姓名：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="TextBoxUserReaname" runat="server" Width="460px" CssClass="l_input" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>支付运营商品ID：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="TextBoxb004_OperationGoodsID" runat="server" Width="460px" CssClass="l_input" MaxLength="200">1</asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxb004_OperationGoodsID" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator4" runat="server" ErrorMessage="支付运营商品ID不能为空!" ControlToValidate="TextBoxb004_OperationGoodsID"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                

                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>购买数量：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="TextBox2OrderCount" runat="server" Width="460px" CssClass="l_input" MaxLength="200">1</asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox2OrderCount" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5" runat="server" ErrorMessage="购买数量不能为空!" ControlToValidate="TextBox2OrderCount"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>上级用户微店ID：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="TextBox3ParentID" runat="server" Width="460px" CssClass="l_input" MaxLength="200" OnTextChanged="TextBox3ParentID_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox3ParentID" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator6" runat="server" ErrorMessage="上级用户微店ID不能为空!" ControlToValidate="TextBox3ParentID"></asp:RequiredFieldValidator>
                                        <asp:Image ID="Image1ParentID" runat="server" ImageAlign="Middle" Width="24px" />
                                        <asp:Label ID="LabelParentID" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>上上级用户微店ID：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="TextBox4GrandParentID" runat="server" Width="460px" CssClass="l_input" MaxLength="200" OnTextChanged="TextBox4GrandParentID_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox4GrandParentID" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator7" runat="server" ErrorMessage="上上级用户微店ID不能为空!" ControlToValidate="TextBox4GrandParentID"></asp:RequiredFieldValidator>
                                        <asp:Image ID="Image1GrandParentID" runat="server" ImageAlign="Middle" Width="24px" />
                                        <asp:Label ID="Label1GrandParentID" runat="server"  Text="" ></asp:Label>
                                    </td>
                                </tr>
                                <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td class="auto-style1" align="right">
                                        <strong>运营中心ID：</strong>
                                    </td>
                                    <td bgcolor="#ecf5ff" class="auto-style2">
                                        <asp:TextBox ID="TextBox5b002_OperationCenterID" runat="server" Width="460px" CssClass="l_input" MaxLength="200" OnTextChanged="TextBox5b002_OperationCenterID_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox5b002_OperationCenterID" ErrorMessage="格式整数" ForeColor="#FF3300" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator8" runat="server" ErrorMessage="运营中心ID不能为空!" ControlToValidate="TextBox5b002_OperationCenterID"></asp:RequiredFieldValidator>


                                        <asp:Image ID="Image1OperationCenterID" runat="server" ImageAlign="Middle" ImageUrl="" Width="24px" />
                                        <asp:Label ID="Label1OperationCenterID" runat="server" Text=""></asp:Label>


                                    </td>
                                </tr>

                                 <tr class="tdbg" bgcolor="#e3e3e3">
                                    <td height="35" class="style3" align="right">
                                        <strong>支付流水号：</strong>
                                    </td>
                                    <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                        <asp:TextBox ID="TextBox3_PaySerialNumber" runat="server" Width="460px" CssClass="l_input" MaxLength="200" OnTextChanged="TextBox3_PaySerialNumber_TextChanged" AutoPostBack="True"></asp:TextBox>
                                         <asp:Label ID="TextBox3_PaySerialNumber_TipInfo" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td height="35" class="style3" align="right">
                                    <strong>支付时间：</strong>
                                </td>
                                <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                   <%-- <input
                                        type="text" value="<%=strTextBox_PayTime%>"  maxlength="460px" id="TextBox_PayTime"
                                        name="TextBox_PayTime" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"
                                        readonly="true" class="calendarFocus l_input" style="cursor: pointer;Width:460px" />--%>
                                    <asp:TextBox ID="TextBox_PayTime" runat="server" Width="460px" onclick="SelectDate(this, 'yyyy-MM-dd HH:mm:ss', 50, 0)"  CssClass="l_input calendarFocus" MaxLength="450"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td height="35" class="style3" align="right">
                                    <strong>发货描述：</strong>
                                </td>
                                <td style="height: 36px; width: 80%;" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="TextBox2DeliveryText" runat="server" Width="460px" CssClass="l_input" MaxLength="20">自动发货</asp:TextBox>
                                    如果填写数据表示属于自动发货处理。否则需要待发货处理哦</td>
                            </tr>


                            </ContentTemplate>
                        </asp:UpdatePanel>

                      


                        <tr class="tdbg" bgcolor="#ecf5ff">
                            <td align="center" height="35" style="width: 72px">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="35">&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px"
                        OnClick="btnAdd_Click" OnClientClick="return CheckClientValidate();" CssClass="b_input"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>



    </form>
</body>
</html>
