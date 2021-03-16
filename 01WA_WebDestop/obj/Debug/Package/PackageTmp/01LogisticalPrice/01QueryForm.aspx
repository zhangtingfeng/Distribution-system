<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01QueryForm.aspx.cs" Inherits="_01WA_WebDestop._01LogisticalPrice._01QueryForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body {
            background: url("16sucai_p20161009105_0da.JPG") no-repeat;
            height: 100%;
            width: 100%;
            overflow: hidden;
            background-size: cover;
            display: flex; /*开启盒子布局*/
            /*text-align: center;
            text-align: center;
            display: -webkit-box;
            -webkit-box-pack: center;
            -webkit-box-orient: horizontal;
            align-items: center;*/
            position: relative;
            font-size: 30px;
        }

        .mytable {
            width: 600px;
            height: 300px;
            /*margin: 0 auto;*/
            position: absolute;
            left: 50%;
            top: 50%;
            margin-left: -300px;
            margin-top: 300px;
        }

        .muinput {
            font-size: 30px;
            height: 44px;
        }

        .mybutton {
            font-size: 30px;
            height: 60px;
            width: 200px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="Table1" CssClass="mytable" runat="server">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">目的国家</asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="TextBox_DesCountry" runat="server" CssClass="muinput"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">称重</asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="TextBox1kgs" runat="server" CssClass="muinput"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">类型</asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:DropDownList ID="DropDownListtype" CssClass="muinput" runat="server">
                            <asp:ListItem Value="Envelope">Envelope</asp:ListItem>
                            <asp:ListItem Selected="True" Value="Package">Package</asp:ListItem>
                             <asp:ListItem Value="Envelope">Pak</asp:ListItem>
                             <asp:ListItem Value="文件">文件</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"></asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Button ID="Button1" runat="server" CssClass="mybutton" OnClick="Button1_Click" Text="运费计算" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
