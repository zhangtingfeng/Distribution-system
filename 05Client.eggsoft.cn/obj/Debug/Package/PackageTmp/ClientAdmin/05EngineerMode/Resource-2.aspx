<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resource-2.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.Resource_2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Control/WUC_btnIntroduction.ascx" TagName="WUC_btnIntroduction" TagPrefix="WUC_2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resource-2</title>
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <script src="Resource-0.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <link href="../skin/Resource-0.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <link href="../../Styles/layer-v3.0.3skin/layer.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/layer-v3.0.3/layer.js"></script>
    <script type="text/javascript">
        function altRows(id) {
            if (document.getElementsByTagName) {

                var table = document.getElementById(id);
                var rows = table.getElementsByTagName("tr");

                for (i = 0; i < rows.length; i++) {
                    if (i % 2 == 0) {
                        rows[i].className = "evenrowcolor";
                    } else {
                        rows[i].className = "oddrowcolor";
                    }
                }
            }
        }

        window.onload = function () {
            //配置一个透明的询问框
            var index = layer.load(0, { shade: false, time: 2000 }); //0代表加载的风格，支持0-2



            altRows('alternatecolor');
        }
    </script>




    <script type="text/javascript">
        function ImagesZhengWen() {
            var fullStr1 = "Resource-ZhengWen.aspx";
            window.location.href = fullStr1;
        }

        function ModifySelectThisJPG(intResource) {
            //配置一个透明的询问框
            var index = layer.load(0, {
                shade: true,
                time: 1000,
                end: function () {
                    //var varpageIndex="1";
                    var varpageIndex = getpageIndex();
                    var fullStr1 = "BoardJPG.aspx?type=ModifySelectThisJPG&ResourceID=" + intResource + "&pageIndex=" + varpageIndex;
                    jsPost(fullStr1, {
                        // 'ResourceID': intResource,
                        'ReturnURL': "Resource-2.aspx?type=ModifySelectThisJPG&ResourceID=" + intResource + "&pageIndex=" + varpageIndex
                    });
                }

            }); //0代表加载的风格，支持0-2

          
        }

        function getpageIndex() {
            var varpageIndex = "<%=Eggsoft.Common.Session.getQueryString("pageIndex")%>";
            if (varpageIndex == "") {
                varpageIndex = "1";
            }
            return varpageIndex;
        }

        function deleteThis(intResource) {
            var fullStr1 = "ResourceSavePost.aspx";
            var varpageIndex = getpageIndex();
            jsPost(fullStr1, {
                'type': 'Delete',
                'ResourceID': intResource,
                'ReturnURL': "Resource-2.aspx?pageIndex=" + varpageIndex
            });
        }



        function saveThis(intResource) {
            var varpageIndex = getpageIndex();



            var var_TextBox_Title_Marker = "#NewmyTextBoxTitle" + intResource + "";
            var var_TextBox_Title_Content = ($(var_TextBox_Title_Marker).val());


            var var_NewmyTextBox_Marker = "#NewmyTextBox" + intResource + "";
            var var_NewmyTextBox_Content = ($(var_NewmyTextBox_Marker).val());
            var varContent = htmlEncode(var_TextBox_Title_Content + "#@#$#" + var_NewmyTextBox_Content);


            var varURLMarker = "#TextBox_Url_" + intResource + "";
            var varURLContent = htmlEncode($(varURLMarker).val());

            var varPICURLMarker = "#SingalImage" + intResource + "";
            var varPICURLContent = htmlEncode($(varPICURLMarker).attr("src"));

            var varbool = varPICURLContent == "undefined";
            if (varbool) {
                alert("必须选择图片！");
                ModifySelectThisJPG(intResource)
                return;
            }



            var varReturnURL = "Resource-2.aspx?pageIndex=" + varpageIndex;

            var fullStr1 = "ResourceSavePost.aspx";
            //alert(varReturnURL);
            jsPost(fullStr1, {
                'PICURL': varPICURLContent,
                'TextURL': varURLContent,
                'TextContent': varContent,
                'type': 'Modify2',
                'ResourceID': intResource,
                'ReturnURL': varReturnURL
            });

        }

        //  
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ajax_tab_menu">
            <div class="tab_menu_tit">
                <WUC_2:WUC_btnIntroduction ID="WUC_btnIntroduction1" runat="server" />
            </div>
            <asp:Table ID="Table_Show" runat="server" CssClass="altrowstable">
                <asp:TableRow ID="TableRow1" runat="server" CssClass="CSSTableRow1">
                    <asp:TableCell ID="TableCell1_New" CssClass="Css400" runat="server" HorizontalAlign="Center" VerticalAlign="Middle">
                        <table style="width: 100%; background-color: Gray;">
                            <tr>
                                <td>
                                    <asp:Image ID="Image_Add_New" ImageUrl="../skin/Images/nothing.png" runat="server" CssClass="picCss" ToolTip="单击右边的上传按钮选择相应的封面图片。" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox_Title" runat="server" Height="20px" CssClass="picCss" TextMode="SingleLine" ToolTip="请在此处输入标题内容。"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox_ADD_New" runat="server" Height="100px" CssClass="picCss" TextMode="MultiLine" ToolTip="请在此处输入文本内容。"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox_Url_New" runat="server" Height="20px" CssClass="picCss" TextMode="SingleLine" ToolTip="编辑正文后，请复制连接地址到当前编辑框。请在此处输入原文链接。没有留空。"></asp:TextBox>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Http"
                                        runat="server" ControlToValidate="TextBox_Url_New" ErrorMessage="这不是网页地址"
                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:TableCell>

                    <asp:TableCell ID="TableCell2_New" runat="server" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="mymiddle">
                        <img src="../skin/Images/SelectJPG.png" runat="server" id="Images_New" onclick="ModifySelectThisJPG(-1)" style="adding-left: 0px; cursor: pointer" title="选择并保存-1">
                        <br />
                        <br />

                        <asp:ImageButton ID="ImageButton_ADD" runat="server" OnClick="ImageButton_ADD_Click" ToolTip="保存操作。" ImageUrl="../skin/Images/save.png" />
                    </asp:TableCell>


                </asp:TableRow>
            </asp:Table>
            <!--如需要正文链接。建议使用买家帮助文档。<a  target="_blank" href="/Admin/Help_Content/Board_HelpContent.aspx?BuyOrSalse=Buy" style="color: #0033CC">编辑成功后这里输入链接</a>。这样一举二得。-->
            <div class="listPage">
                <div length="0" class="prolistpager_3">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                        CurrentPageButtonClass="page-num-Current" EnableUrlRewriting="true"
                        MoreButtonsClass="page-num" NumericButtonCount="6"
                        NumericButtonTextFormatString="{0}" OnPageChanged="AspNetPager1_PageChanged"
                        PageSize="6" PagingButtonsClass="page-num" ShowFirstLast="False"
                        ShowPageIndexBox="Never" ShowPrevNext="False" UrlRewritePattern=""
                        Width="100%" AlwaysShow="True">
                    </webdiyer:AspNetPager>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
