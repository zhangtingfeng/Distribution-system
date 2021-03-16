<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="16SendMoney_BoardABC.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._16SendMoney._16SendMoney_BoardABC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
<title>分 红 方 案 管  理</title>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 	


<style type="text/css">
input{ height:auto;}

    .auto-style1 {
        color: #FF3300;
    }

</style>
</head>
<body>
<form id="Form1" method="post" runat="server">

<div class="mhead">
<h1>  查看三类用户（代理分销商\已购买、已关注）</h1>
                   
<div class="mselct"> <span class="auto-style1">A类（代理分销商）：</span><asp:Literal ID="Literal1_A" runat="server"></asp:Literal>
    </div>
<div class="mselct"> <span class="auto-style1">B类（已购买）：</span><asp:Literal ID="Literal2_B" runat="server"></asp:Literal>
    </div>
<div class="mselct"> <span class="auto-style1">C类（已关注）：</span><asp:Literal ID="Literal3_C" runat="server"></asp:Literal>
    </div>
</div>

</form>
</body>
</html>