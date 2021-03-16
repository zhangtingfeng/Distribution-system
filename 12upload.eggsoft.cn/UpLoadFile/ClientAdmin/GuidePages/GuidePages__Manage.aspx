<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuidePages__Manage.aspx.cs" Inherits="_12upload.eggsoft.cn.UpLoadFile.ClientAdmin.GuidePages.GuidePages__Manage" %>

<%@ Register Src="../../../Control/UploadControl/Upload_MultiSeclect.ascx" TagName="Upload_MultiSeclect" TagPrefix="uc2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>MenuSet_Manage</title>
    <script src="../../../Upload_JS/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../../../Upload_JS/jquery-2.0.3.min.js" type="text/javascript"></script>
    <script src="../../../Upload_JS/ckfinder/ckfinder.js" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1 {
            color: #CC0000;
        }

        .style2 {
            height: 36px;
            width: 10%;
        }

        .style3 {
            width: 10%;
        }

        .style4 {
            color: #CC00CC;
        }

        #Form1 {
            text-align: center;
        }
        .maxWidth {
            max-width:40px;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table height="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
            <tr>
                <td valign="top" align="center" style="width: 100%">
                    <br>

                    <br>
                    <br>
                    <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                        <tr class="title" bgcolor="#a4b6d7">
                            <td align="center" colspan="2" height="25"><strong>信 息 管 理(本信息地址:<%=MenuLink%>)</strong></td>
                        </tr>
                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td bgcolor="#e3e3e3" class="style2">
                                <div align="right"><strong>信息名称：</strong></div>
                            </td>
                            <td style="height: 36px; width: 600px;" bgcolor="#ecf5ff">
                                <asp:TextBox ID="txtTitle" runat="server" Width="376px"></asp:TextBox>
                                <font face="宋体"><span class="style1"><strong>*</strong></span></font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="信息名称不能为空!" ControlToValidate="txtTitle"></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td bgcolor="#e3e3e3" class="style2">
                                <div align="right"><strong>信息图标：</strong></div>
                            </td>
                            <td align="left" bgcolor="#ecf5ff" height="35">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <uc2:Upload_MultiSeclect ID="Upload_MultiSeclect2" runat="server" MultiChoice="False" />
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0" multichoice="False">
                            <td align="right" bgcolor="#e3e3e3" height="22" class="style3">
                                <strong>链接类型：</strong>
                            </td>
                            <td bgcolor="#ecf5ff" height="22">
                                <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True"
                                    OnCheckedChanged="RadioButton1_CheckedChanged" Text="直接显示" />
                                <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True"
                                    OnCheckedChanged="RadioButton2_CheckedChanged" Text="链接显示" />
                            </td>
                        </tr>
                        <tr id="LinkShow" class="tdbg" bgcolor="#c0c0c0" runat="server">
                            <td align="right" bgcolor="#e3e3e3" height="22" class="style3">
                                <strong>信息链接：</strong></td>
                            <td bgcolor="#ecf5ff" height="22">
                                <asp:TextBox ID="txtLink" runat="server" Width="376px"></asp:TextBox>
                                <font face="宋体"><span class="style1"><strong>*</strong></span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ErrorMessage="信息链接不能为空!" ControlToValidate="txtLink"></asp:RequiredFieldValidator>
                                    &nbsp;</font></td>
                        </tr>

                        <tr id="TextShow" class="tdbg" bgcolor="#c0c0c0" runat="server">
                            <td align="right" bgcolor="#e3e3e3" height="22" class="style3">
                                <strong>文本内容：</strong></td>
                            <td bgcolor="#ecf5ff" height="22">
                                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="347px"
                                    Width="573px" />
                                <font face="宋体">
                                    <span class="style4">*</span></font></td>
                        </tr>


                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3">
                                <div align="right"><strong>排序位置：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="22">
                                <font face="宋体">
                                    <div align="left">
                                        <asp:TextBox ID="txtMenuPos" runat="server" Width="89px"></asp:TextBox>
                                        <font face="宋体">数字越大 排序越靠后  <span class="style1"><strong>*</strong></span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ErrorMessage="排序位置不能为空 必须是数字" ControlToValidate="txtMenuPos"
                                                ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                ErrorMessage="排序位置不能为空!" ControlToValidate="txtMenuPos"></asp:RequiredFieldValidator>
                                        </font>



                                    </div>
                                </font>



                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0" style="display: none;">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3">
                                <div align="right"><strong>发布者：</strong></div>
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="22">
                                <div align="left">
                                    <asp:TextBox ID="txtWriter" runat="server" Width="376px">Admin</asp:TextBox>
                                    <font face="宋体">
                                        <span class="style1"><strong>*</strong></span></font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="发布者不能为空!" ControlToValidate="txtWriter"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3">&nbsp;
                            </td>
                            <td align="center" bgcolor="#ecf5ff" height="22">
                                <div align="left">
                                    <asp:Button ID="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <br />
                                </div>
                            </td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3">&nbsp;</td>
                            <td align="center" bgcolor="#ecf5ff" height="22">&nbsp;</td>
                        </tr>

                        <tr class="tdbg" bgcolor="#e3e3e3">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3" colspan="2">
                                <font face="宋体">
                                    <asp:Button ID="btnAddClass" runat="server" Text="添加文章（列表显示）" OnClick="btnAddClass_Click" Width="151px" />
                                </font>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td align="center" height="22" class="style3" colspan="2"></td>
                        </tr>

                        <tr class="tdbg" bgcolor="#c0c0c0">
                            <td align="center" bgcolor="#e3e3e3" height="22" class="style3" colspan="2">

                                <asp:GridView ID="gvMenu" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="1px" BorderColor="DarkGray" CellSpacing="1" CellPadding="5"
                                    Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound">
                                    <HeaderStyle BackColor="#e3e3e3" Height="32px" />
                                    <AlternatingRowStyle BackColor="ActiveBorder" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="编号">
                                            <HeaderStyle Width="10%" CssClass="centerAuto" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="MenuName" HeaderText="菜单名称">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="MenuIcon" HeaderText="菜单图标" ControlStyle-CssClass="maxWidth">
                                            <HeaderStyle Width="10%"  CssClass="maxWidth" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="maxWidth" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="LinkOrText" HeaderText="显示方式">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="MenuLink" HeaderText="菜单链接">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>


                                        <asp:BoundField HeaderText="修改">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>



                                        <asp:BoundField HeaderText="删除">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField HeaderText="二维码">
                                            <HeaderStyle Width="10%" CssClass="centerAuto" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                                        </asp:BoundField>

                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>




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