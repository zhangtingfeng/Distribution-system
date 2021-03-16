<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeyAnswer_Board.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.KeyAnswer_Board" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnnounceManage</title>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />     

</head>
<body>
    <form id="Form1" method="post" runat="server"> 
<div class="mhead">
<h1>  关 键 词 回 复 管 理</h1> 
<div class="mselct">     <asp:RadioButtonList ID="RadioButtonList_Like_Or_Same" runat="server" 
                        AutoPostBack="True" 
                        onselectedindexchanged="RadioButtonList_Like_Or_Same_SelectedIndexChanged" 
                        RepeatDirection="Horizontal" Width="300px" CssClass="centerAuto">
                        <asp:ListItem Value="0">模糊匹配</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1">精确匹配</asp:ListItem>
                    </asp:RadioButtonList> 
                    管理选项：<asp:Button 
                        ID="btnAdd" runat="server" Text="添加关键词" OnClick="btnAdd_Click" />
    </div>
</div>
        <table height="100%" cellspacing="0" style=" text-align:center;" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center" style=" text-align:center;" >
                    
                        <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0" BorderColor="#EFEFEF" CellSpacing="0" CellPadding="0" 
                            Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab" >
                        
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="编号">
                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="16%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                                    CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Marker" HeaderText="关键词名称">
                                <HeaderStyle Width="16%" />
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                                                           
                             
                               <asp:BoundField DataField="MarkerContent" HeaderText="素材类型">
                                <HeaderStyle Width="16%" />
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                        

                               <asp:BoundField HeaderText="素材ID">
                                <HeaderStyle Width="16%" />
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="16%" />
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>

                           
                                 <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="16%" />
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        &nbsp;
                        <br />
                        <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">首页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">尾页</asp:LinkButton>
                        &nbsp;跳到<asp:DropDownList ID="ddlGoPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGoPage_SelectedIndexChanged"
                            Width="43px">
                        </asp:DropDownList>页 </td></tr></table>
     </form>
</body>
</html>