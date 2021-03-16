<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreightTemplateOperating.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient.FreightTemplateOperating1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Image/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="/Scripts/showModalDialog.js"></script>
    <style type="text/css">
        .border input {
            height: auto;
        }
    </style>

    <script type="text/javascript">
        function QueryString() {
            var name, value, i;
            var str = location.search;
            var num = str.indexOf("?")
            str = str.substr(num + 1);
            var arrtmp = str.split("&");
            for (i = 0; i < arrtmp.length; i++) {
                num = arrtmp[i].indexOf("=");
                if (num > 0) {
                    name = arrtmp[i].substring(0, num);
                    value = arrtmp[i].substr(num + 1);
                    this[name] = value;
                }
            }
        }


        function DeleteAddShengArea(varAddMarker) {
            var Request = new QueryString();
            var vartype = Request["type"];
            var varID = Request["ID"];
            if ((!vartype) || (!varID)) {
                alert("保存后才能添加地区");
                return;
            }

            var varThisid = "0";
            if (varAddMarker == "add") { }
            else {
                varThisid = varAddMarker;
                varAddMarker = "modify";
            }

            //var argument1 = document.getElementById("TextBox1").value;
            //var argument2 = document.getElementById("TextBox2").value; 传递两个参数给模态窗口
            var arguments = new Array(varID, varAddMarker);
            var m = window.showModalDialog("FreightTemplateOperatingArea.aspx?FreightTemplate_ID=" + varID + "&type=Delete&FreightTemplateArea_ID=" + varThisid, arguments, "dialogWidth:800px,dialogHeight:600px,center:yes,resizable:yes,status:no"); //m接受模态窗口的返回值，前台代码执行到这里开始等待模态窗口返回值再往下走。


            if (m != null || rv === undefined) {
                windowreload();
                //document.getElementById("TextBox1").value = m[0];
                //document.getElementById("TextBox2").value = m[1]; //把接收到得模态窗口返回值显示在父窗口

            }

        }

        function windowreload() {
            window.location.reload(); //提交模态窗口后刷新页面
        }

        function showdialogAddShengArea(varAddMarker) {
            var Request = new QueryString();
            var vartype = Request["type"];
            var varID = Request["ID"];
            if ((!vartype) || (!varID)) {
                alert("保存后才能添加地区");
                return;
            }

            var varThisid = "0";
            if (varAddMarker == "add") { }
            else {
                varThisid = varAddMarker;
                varAddMarker = "modify";
            }

            //var argument1 = document.getElementById("TextBox1").value;
            //var argument2 = document.getElementById("TextBox2").value; 传递两个参数给模态窗口
            var arguments = new Array(varID, varAddMarker);
            var m = window.showModalDialog("FreightTemplateOperatingArea.aspx?FreightTemplate_ID=" + varID + "&type=" + varAddMarker + "&FreightTemplateArea_ID=" + varThisid, arguments, "dialogWidth:800px,dialogHeight:600px,center:yes,resizable:yes,status:no"); //m接受模态窗口的返回值，前台代码执行到这里开始等待模态窗口返回值再往下走。
            //alert("11122232");

            //if (m != null) {
            //    alert("1112223277");

            //document.getElementById("TextBox1").value = m[0];
            //document.getElementById("TextBox2").value = m[1]; //把接收到得模态窗口返回值显示在父窗口
            window.location.reload(); //提交模态窗口后刷新页面
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">
                    <strong>运 费 模 板</strong>
                </th>
            </tr>
            <tr class="tdbg" style="display: none;" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
                    <strong>编号：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
                    <strong>名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">

                    <asp:TextBox ID="TextBox_Name" runat="server" Width="137px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <span class="style1"><strong>*</strong></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ErrorMessage="运费名称不能为空!" ControlToValidate="TextBox_Name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
                    <strong>发货地址：</strong>
                </td>
                <td bgcolor="#ecf5ff">

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownList_Area1" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList_Area1_SelectedIndexChanged" Width="101px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_Area2" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList_Area2_SelectedIndexChanged" Width="101px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_Area3" runat="server" Height="20px" Width="101px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
                    <strong>默认运费：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 10%;">首件运费</td>
                            <td>
                                <asp:TextBox ID="txtFreight" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>元<span class="style1"><strong>*</strong></span>&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ErrorMessage="运费金额不能为空!" ControlToValidate="txtFreight"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFreight"
                                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">每添加一件</td>
                            <td>
                                <asp:TextBox ID="txtFreightMore" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>元
                <span class="style1"><strong>*</strong>
                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="每添加一件商品金额不能为空!" ControlToValidate="txtFreightMore"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFreightMore"
                                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <asp:Literal ID="LiteralRead" runat="server"></asp:Literal>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
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
                            <td style="width: 15%;">满多少钱包邮</td>
                            <td>
                                <asp:TextBox ID="TextBox_BaoYouMoney" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>元
                <span class="style1"><strong>*</strong>
                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server" ErrorMessage="满多少钱包邮不能为空!" ControlToValidate="TextBox_BaoYouMoney"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_BaoYouMoney"
                                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">满多少件包邮</td>
                            <td>
                                <asp:TextBox ID="TextBox_BaoYouGeShu" runat="server" Width="137px" CssClass="l_input">0</asp:TextBox>件<span class="style1"><strong>*</strong></span>&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ErrorMessage="满多少件包邮不能为空!" ControlToValidate="TextBox_BaoYouGeShu"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox_BaoYouGeShu"
                                    ErrorMessage="格式000.00" ForeColor="#FF3300" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator></td>
                        </tr>

                    </table>
                </td>
            </tr>
            <asp:Literal ID="LiteraldisplayOLDClass" runat="server"></asp:Literal>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
                    <strong>
                        <input id="Button1" type="button" onclick='showdialogAddShengArea("add");' value="增加地区" />
                    </strong>
                </td>
                <td bgcolor="#ecf5ff">&nbsp;</td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150px" height="35">
                    <strong>备注：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="376px" CssClass="l_input" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="center" height="45" class="style3">&nbsp;
                </td>
                <td align="left" bgcolor="#ecf5ff" height="45">
                    <asp:Button ID="btnAdd" runat="server" Text=" 保  存 " Width="72px" OnClick="btnAdd_Click"
                        CssClass="b_input"></asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnRefresh" runat="server" Text=" 刷 新 " Width="72px" OnClick="btnRefresh_Click"
                        CssClass="b_input"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
