<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="writeHtml_StyleModel2.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.WriteHtml.writeHtml_StyleModel2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title>3</title>
      <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
 
    <style type="text/css">
        .auto-style1 {
            font-size: medium;
        }
    </style>
 
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>

                    <span class="auto-style1">请稍等。马上完成（部分资源云更新可能需要<span style="color:red">5-15分钟,请5-15分钟后</span>再检查移动前端）。</span><br />

                    <asp:Label ID="Label_Memory" Text="0" runat="server" Visible="False"></asp:Label>

                    <asp:Timer ID="Timer1" runat="server" Interval="100" OnTick="Timer1_Tick">
                    </asp:Timer>

                    <asp:Label ID="lResult0_Show" runat="server" style="text-align: center; font-size: medium;"></asp:Label>

                </ContentTemplate>

            </asp:UpdatePanel>


        </div>
    </form>
    
</body>
</html>
