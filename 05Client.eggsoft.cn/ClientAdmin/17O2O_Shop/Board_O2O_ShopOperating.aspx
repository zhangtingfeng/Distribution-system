<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_O2O_ShopOperating.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._17O2O_Shop.Board_O2O_ShopOperating" %>

<!DOCTYPE html>
<script runat="server">

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Scripts/showModalDialog.js?version=js201709121928"></script>
    <script src="../../Scripts/Times.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="../Image/jquery-calendar.js?version=js201709121928" type="text/javascript"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .border input {
        }
    </style>

    <script type="text/javascript">

        function oper() {
            var varLat = $("#TextBox_Lnt_Lat").val();
            varLat = varLat.replace(/,/g, '_').replace(/ /g, '');
            var address = window.showModalDialog("Board_O2O_ShopNav.html?ver=201506100601&Lnt_Lat=" + varLat, "aaaa", "dialogWidth=800px;dialogHeight=600px");
            if (address != undefined) {
                ParentOpenTest(address);
            }
        }

        function ParentOpenTest(varaddress) {
            document.form1.TextBox_Lnt_Lat.value = varaddress;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0"
            align="center" border="0">
            <tr class="title">
                <th align="center" colspan="2" height="25">
                    <strong>管 理 门 店</strong>
                </th>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>店铺名称：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="Shop_Name" runat="server" Width="137px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <span class="style1"><strong>*</strong></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ErrorMessage="店铺名称不能为空!" ControlToValidate="Shop_Name"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>联系人：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_Contactman" runat="server" Width="137px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>联系电话：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_Tel" runat="server" Width="137px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>省市县选择：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:DropDownList ID="DropDownList_Class1" runat="server" AutoPostBack="True" Height="20px"
                                OnSelectedIndexChanged="DropDownList_Class1_SelectedIndexChanged" Width="101px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_Class2" runat="server" AutoPostBack="True" Height="20px"
                                OnSelectedIndexChanged="DropDownList_Class2_SelectedIndexChanged" Width="101px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList_Class3" runat="server" Height="20px" Width="101px">
                            </asp:DropDownList>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>具体地址：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_Address" runat="server" Width="137px" CssClass="l_input" MaxLength="20"></asp:TextBox>
                    <span class="style1"><strong>*</strong></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ErrorMessage="具体地址不能为空!" ControlToValidate="TextBox_Address"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>百度经纬度定位：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_Lnt_Lat" runat="server" Width="200px" CssClass="l_input" MaxLength="40">112.953922, 32.874708</asp:TextBox>
                    <span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                        runat="server" ErrorMessage="经纬度定位不能为空!" ControlToValidate="TextBox_Lnt_Lat"></asp:RequiredFieldValidator>
                    <input id="Button1" type="button" value="一键获取经纬度" onclick="oper();" /></td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" class="style2">
                    <strong>上传店面logo：</strong><br />
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:Image ID="Image_ContactO2oLogo" runat="server" Width="50px" />
                    <asp:FileUpload ID="FileUpload_ContactO2oLogo" runat="server" />链接地址请到常用链接中查看</td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>简短商家推荐广告语：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_ShopAdMsg" runat="server" Width="674px" CssClass="l_input" MaxLength="120"></asp:TextBox>
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td class="style2" align="right" width="150" height="35">
                    <strong>营业时间：</strong>
                </td>
                <td bgcolor="#ecf5ff">
                    <asp:TextBox ID="TextBox_ShopDayTime" runat="server" Width="137px" CssClass="l_input" MaxLength="20">08:00-23:00</asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg" bgcolor="#e3e3e3">
                <td align="right" height="35" style="font-weight: 700;">Email：
                </td>
                <td bgcolor="#ecf5ff" height="35">
                    <asp:TextBox ID="Textbox_Email" runat="server" Width="156px" AutoPostBack="True"
                        OnTextChanged="Textbox_Email_TextChanged" CssClass="l_input" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Email不能为空!"
                        ControlToValidate="Textbox_Email"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email格式不对！"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Textbox_Email"></asp:RegularExpressionValidator>
                    <asp:Literal ID="Literal_CheckEmail" runat="server"></asp:Literal>
                    <asp:Button ID="Button_CheckEmail_Click" runat="server" Text="验证Email" OnClick="Button_CheckEmail_Click_Click" />
                </td>
            </tr>

            <tr class="tdbg" bgcolor="#e3e3e3">
                <td height="35" align="right">
                    <strong>绑定微信号：</strong>
                </td>
                <td align="center" bgcolor="#ecf5ff" height="35">
                    <div align="left">
                        <asp:LinkButton ID="LinkButton_SaoYiSao" runat="server" OnClick="LinkButton_SaoYiSao_Click">扫一扫关联o2o商户的微信号</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:LinkButton ID="LinkButton_Clear" runat="server" OnClick="LinkButton_Clear_Click">清空关联</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label_LinkWeiXin" runat="server" Text="关联微信号，及时和用户沟通交流！" ForeColor="#663300"></asp:Label>
                    </div>
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
