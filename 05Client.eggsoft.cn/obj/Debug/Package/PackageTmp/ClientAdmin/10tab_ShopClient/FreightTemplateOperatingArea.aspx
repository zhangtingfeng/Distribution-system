<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreightTemplateOperatingArea.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient.FreightTemplateOperatingArea1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self" />
    <!--这里千万不能少，否则模态窗口提交时会弹出新的窗口。ie6 放在head之间，ie7放在head之外-->
    <script type="text/javascript">
        function closewindow() {
            window.close();
        }
        function GetDataAndClose() {
            //var a = document.getElementById("TextBox1").value;
            //var b = document.getElementById("TextBox2").value;
            var array = new Array(0);
            window.returnValue = array; //设置模态窗口的返回值，供父窗口接收
            closewindow();
        }
        function doInit() {
            //var MyArgs = window.dialogArguments; 提取父窗口所传的参数
            //document.getElementById("TextBox1").value = MyArgs[0].toString();
            //document.getElementById("TextBox2").value = MyArgs[1].toString();

        }
    </script>
    <script src="../../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Image/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .border input {
            height: auto;
        }

        .auto-style1 {
            height: 22px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#Text_Shopping_Vouchers_Start").calendar();
            $("#Text_Shopping_Vouchers_End").calendar();
        });
    </script>
</head>
<body onload="doInit()">
    <form id="Form2" method="post" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">
                    <strong>选择地区</strong>
                </th>
            </tr>
            <tr class="tdbg" style="" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>区域运费：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style1">首件运费</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtFreight" runat="server" CssClass="l_input" Width="137px">0</asp:TextBox>
                                元 <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFreight" ErrorMessage="运费金额不能为空!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFreight" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">每添加一件商品：</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtFreightMore" runat="server" CssClass="l_input" Width="137px">0</asp:TextBox>
                                元 <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFreightMore" ErrorMessage="每添加一件商品金额不能为空!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFreightMore" ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>全场包邮条件（不填写表示不使用包邮条件）：</strong>
                </td>

                <td bgcolor="#ecf5ff">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 15%;">满多少公斤包邮</td>
                            <td>
                                <asp:TextBox ID="TextBox_Allkg" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>公斤<span class="style1"><strong>*</strong></span>&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server" ErrorMessage="满多少公斤包邮不能为空!" ControlToValidate="TextBox_Allkg"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox_Allkg"
                                    ErrorMessage="格式000.000" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{3})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">满多少元包邮：</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TextBox_BaoYouMoney" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>元
                <span class="style1"><strong>*</strong>
                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server" ErrorMessage="满多少钱包邮不能为空!" ControlToValidate="TextBox_BaoYouMoney"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_BaoYouMoney"
                                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">满多少件包邮</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TextBox_BaoYouGeShu" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>件<span class="style1"><strong>*</strong></span>&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ErrorMessage="满多少件包邮不能为空!" ControlToValidate="TextBox_BaoYouGeShu"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox_BaoYouGeShu"
                                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>

                    </table>
                </td>

            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>选择区域：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Literal ID="Literal_AddArea" runat="server"></asp:Literal>
                </td>
            </tr>


            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">
                    <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="72px" OnClick="btnAdd_Click"
                        CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>