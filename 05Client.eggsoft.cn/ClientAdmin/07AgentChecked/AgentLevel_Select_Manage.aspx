<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentLevel_Select_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._07AgentChecked.AgentLevel_Select_Manage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>

    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .borderClass td {
            text-align: right;
            padding-left: 5px;
        }

        .auto-style1 {
            width: 20%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px;margin-top:20px;margin-left:20px;">
         <asp:Literal ID="Literal1" runat="server" Text="没有特别要求，不要调整这里的级别。用户申请是什么代理，就是什么代理"></asp:Literal>
      
         <br />
         <br />
      
        <asp:Literal ID="Literal_ParentID_Show" runat="server"></asp:Literal>
         <br />
        <asp:RadioButtonList ID="LevelRadioButtonList" runat="server">
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="Button1" runat="server" Text="确 认" OnClick="Button1_Click" />
    
    </div>
    </form>
</body>
</html>
