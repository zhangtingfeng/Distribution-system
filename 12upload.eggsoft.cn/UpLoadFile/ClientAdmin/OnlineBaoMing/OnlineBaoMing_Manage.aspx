<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineBaoMing_Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.OnlineBaoMing.OnlineBaoMing_Manage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>在线报名模块</title>
    <script src="../Scripts/Times.js"></script>
    <script src="../../../Upload_JS/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../../Upload_JS/jquery-2.0.3.min.js" type="text/javascript"></script>
    <script src="../../../Upload_JS/ckfinder/ckfinder.js" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.3.js"></script>
    <link href="../Scripts/jquery-calendar.css" rel="stylesheet" />
    <script src="../Scripts/jquery-calendar.js"></script>
    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .style2 {
            height: 36px;
            width: 150px;
        }

        .style3 {
            width: 150px;
        }

        .style4 {
            color: #CC00CC;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                       
                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                            <tr class="title" bgcolor="#e3e3e3">
                                <td align="center" colspan="2" height="25" style="text-align: center"><strong>在线报名模块</strong></td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <div align="right"><strong>名称：</strong></div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="txtTitle" runat="server" Width="376px" MaxLength="50"></asp:TextBox>
                                    <font face="宋体"><span class="style1"><strong>*</strong></span></font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="报名名称不能为空!" ControlToValidate="txtTitle"></asp:RequiredFieldValidator></td>
                            </tr>

                            <tr id="TextShow" class="tdbg" bgcolor="#c0c0c0" runat="server">
                                <td align="right" bgcolor="#e3e3e3" height="22" class="style3 styleRight">
                                    <div align="right"><strong>文本内容：</strong></div>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="347px"
                                        Width="573px" /></div>
			                        <font face="宋体">
                                        <span class="style4">*</span></font></td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <div align="right"><strong>报名截止日期：</strong></div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">

                                    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                                    </asp:ScriptManager>

                                    <asp:TextBox ID="TextBox1My_DeadLine" runat="server"></asp:TextBox>
                                    格式参考“2014-09-12”   
                                       <asp:CalendarExtender ID="TextBox1_CalendarExtender" Format="yyyy-MM-dd" runat="server" TargetControlID="TextBox1My_DeadLine"></asp:CalendarExtender>

                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <div align="right"><strong>是否需要审核显示：</strong></div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <asp:CheckBox ID="CheckBox_NeedCheck" runat="server" Text="是否需要审核显示" />
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <div align="right"><strong>是否需要输入详细地址：</strong></div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <asp:CheckBox ID="CheckBox_Need_WriteData" runat="server" Text="是否需要输入详细地址" />
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td bgcolor="#e3e3e3" class="style2 styleRight">
                                    <div align="right"><strong>自定义字段管理：</strong></div>
                                </td>
                                <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                    <asp:CheckBox ID="CheckBox_IFShow_Cus_Item" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_IFShow_Cus_Item_CheckedChanged" Text="是否要自定义字段" />

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Width="831px">
                                                <asp:ListBox ID="ListBox_Item" runat="server" Width="199px"></asp:ListBox>
                                                <asp:TextBox ID="TextBox_Item" runat="server"></asp:TextBox>
                                                <asp:Button ID="Button_Add" runat="server" OnClick="Button_Add_Click" Text="增加字段" />
                                                <asp:Button ID="Button_Del" runat="server" OnClick="Button_Del_Click" Text="删除字段" />

                                            </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#c0c0c0">
                                <td align="center" bgcolor="#e3e3e3" height="22" class="style3">&nbsp;
                                </td>
                                <td align="center" bgcolor="#e3e3e3" height="22">
                                    <div align="left">
                                        &nbsp;
											<asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>


                        <br>
                        <asp:GridView ID="GridView_ShowAll" runat="server" AutoGenerateColumns="False"
                            Width="100%" DataKeyNames="ID" AllowPaging="True" AllowSorting="True"
                            OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="编号"
                                    SortExpression="ID" ReadOnly="True">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LocalCall" HeaderText="电话"
                                    SortExpression="LocalCall">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="地址" SortExpression="Address">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UpdateTime" HeaderText="时间"
                                    SortExpression="UpdateTime">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="Valid" HeaderText="是否审核" ReadOnly="True">
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:CheckBoxField>
                                <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False"
                                            CommandName="Del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('您确定要删除吗？')" Text="删除"></asp:LinkButton>

                                        <asp:LinkButton ID="CheckBox_Pass" runat="server" CausesValidation="False"
                                            CommandName="Pass" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('您确定要审核吗？')" Text="审核/阻止"></asp:LinkButton>

                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                        <br>
                        &nbsp;

                        <br>
                        <br>
                        <%--<asp:Label ID="Label_ErWeiMa" runat="server" Text=""></asp:Label>--%>
                        <br>
                    </td>
                </tr>
            </table>
        </font>



    </form>

    <script type="text/javascript">
        var ckeditor; //定义全局变量 ckeditor
        $(function () {//当全部DOM元素加载完毕后执行下面语句，不加此句javascript将无法找到TextBox1
            ckeditor = CKEDITOR.replace("<%=txtContent.ClientID %>"); //用CKEDITOR.replace命令将TextBox1格式化成富文本
            CKFinder.setupCKEditor(ckeditor, "../../../Upload_JS/ckfinder/"); //用CKFinder.setupCKEditor命令将ckeditor与ckfinder进行整合
        });
    </script>

</body>
</html>
