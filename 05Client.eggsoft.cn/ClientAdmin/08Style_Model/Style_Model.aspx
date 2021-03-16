<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Style_Model.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._08Style_Model.Style_Model" %>

<%@ Register Namespace="Karpach.WebControls" TagPrefix="WebControls" %>
<%@ Register Assembly="_7he7.ColorPicker" Namespace="Karpach.WebControls" TagPrefix="cc_ColorPicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />

    <style type="text/css">
        .styleTable {
            text-align: left;
            margin: auto;
        }

        .style1 {
            width: 600px;
            text-align: center;
            margin: auto;
            height: 303px;
        }

        .style2 {
            text-align: center;
            margin: auto;
            height: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1px" bordercolor="#000000" cellspacing="0px" style="border-collapse: collapse">
                <tr>
                    <td align="center" class="style1">
                        <img alt="" height="300px" src="images/01.jpg"
                            width="200px" /></td>
                    <td align="center" class="style1">
                        <img alt="" height="300px" src="images/02.jpg"
                            width="200px" /></td>
                    <td align="center" class="style1">
                        <img alt="" height="300px" src="images/03.jpg"
                            width="200px" /></td>
                    <td align="center" class="style1">
                        <img alt="" height="300px" src="images/04.jpg"
                            width="200px" /></td>
                </tr>


                <tr>
                    <td class="styleTable">
                        <asp:RadioButton ID="RadioButton0" runat="server" AutoPostBack="True"
                            OnCheckedChanged="RadioButton0_CheckedChanged" Text="模板1" />
                    </td>
                    <td class="styleTable">
                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True"
                            OnCheckedChanged="RadioButton1_CheckedChanged" Text="模板2" />
                    </td>
                    <td class="styleTable">
                        <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True"
                            OnCheckedChanged="RadioButton2_CheckedChanged" Text="模板3" />
                    </td>
                    <td class="styleTable">
                        <asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="True"
                            OnCheckedChanged="RadioButton3_CheckedChanged" Text="模板4" />
                    </td>
                </tr>
                <tr>
                    <td class="styleTable" colspan="4">

                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="CheckBox_StyleModeDoSelf" runat="server" Style="text-align: left" Text="商城首页自定义,点击选择并设置" OnCheckedChanged="CheckBox_StyleMode_CheckedChanged" AutoPostBack="True" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>

                <tr>
                    <td class="styleTable" colspan="4">&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
