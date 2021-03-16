<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoardJPG.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.BoardJPG" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AnnounceManage</title>
    <link href="Resource-0.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="Resource-0.js?version=js201709121928" type="text/javascript"></script>
    <link href="../../Styles/layer-v3.0.3skin/layer.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/layer-v3.0.3/layer.js"></script>

    <script type="text/javascript">
        var index = layer.load(0, { shade: false, time: 4000 }); //0代表加载的风格，支持0-2


        //根据QueryString参数名称获取值
        function getQueryStringByName(name) {
            var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
            if (result == null || result.length < 1) {
                return "";
            }
            return result[1];
        }


        function upThisMedia(intFileNum) {
            var varType = "<%=Eggsoft.Common.Session.getQueryString("type")%>";
            var varResourceID = "<%=Eggsoft.Common.Session.getQueryString("ResourceID")%>";
            var ReturnURL = "<%=Eggsoft.Common.Session.Read("ReturnURL")%>";
            var fullStr1 = "/PHP/01Media/upload.php";
            var strpubGet_ACCESS_TOKEN = "<%=Eggsoft.Common.Session.Read("strpubGet_ACCESS_TOKEN")%>";

    var varMediaTYPE = "<%=Eggsoft.Common.Session.getQueryString("MediaTYPE")%>";
            var FilenameAHref = "#FilenameAHref" + intFileNum;
            var SelectUploadDiskSRC = $(FilenameAHref).attr('title');

            var SelectIMGSRC = ""; //need  pic
            var SelectIMG = "#SelectIMG" + intFileNum;
            SelectIMGSRC = $(SelectIMG);
            if (SelectIMGSRC.length > 0) {
                SelectIMGSRC = $(SelectIMG).attr('src');
            }


            jsPost(fullStr1, {
                'SelectIMGSRC': htmlEncode(SelectIMGSRC),
                'ResourceID': htmlEncode(varResourceID),
                'SelectUploadDiskSRC': htmlEncode(SelectUploadDiskSRC),
                'ACCESS_TOKEN': htmlEncode(strpubGet_ACCESS_TOKEN),
                'MediaTYPE': htmlEncode(varMediaTYPE),
                'ReturnURL': htmlEncode(ReturnURL)
            });

            //            jsPost(fullStr1, {
            //                       'MediaTYPE': 'Delete',
            //                       'ResourceID': '-1',
            //                       'ReturnURL': window.location.href
            //                   });
            //           

        }

        function SelectThisJPG(intFileNum) {
            var varType = "<%=Eggsoft.Common.Session.getQueryString("type")%>";
    var varResourceID = "<%=Eggsoft.Common.Session.getQueryString("ResourceID")%>";


    var fullStr1 = "<%=Eggsoft.Common.Session.Read("ReturnURL")%>";
    var SelectIMG = "#SelectIMG" + intFileNum;
    var SelectIMGSRC = $(SelectIMG).attr('src');

    jsPost(fullStr1, {
        'TextContent': "'" + SelectIMGSRC + "'"
        //                           'ReturnURL':ReturnURL
    });


    //            var fullStr1 = "ResourceSavePost.aspx";
    //            var SelectIMG="#SelectIMG"+intResource;
    //            var SelectIMGSRC=$(SelectIMG).attr('src');
    //            var ReturnURL="<%=Eggsoft.Common.Session.Read("ReturnURL")%>";

    //            jsPost(fullStr1, {
    //                'type': '<%=Eggsoft.Common.Session.Read("type")%>',
    //                'TextContent': "'" + SelectIMGSRC + "'",
    //                'ResourceID':'<%=Eggsoft.Common.Session.Read("ResourceID")%>',
    //                'ReturnURL':ReturnURL
    //            });
}





        function postwith(to, Content) {
            var myForm = document.create_r_rElement("form");
            myForm.method = "post";
            myForm.action = to;

            var myInput = document.create_r_r_rElement_x("input");
            myInput.setAttribute("name", "Text");
            myInput.setAttribute("value", Content);
            myForm.a(myInput);

            document.body.a(myForm);
            myForm.submit();
            document.body.removeChild(myForm);
        }




    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>素 材 管 理 </h1>

            <div class="mselct">
                管理选项：<asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="上  传" OnClick="Button1_Click"
                    ToolTip="上传的多媒体文件有格式和大小限制，如下：" />
            </div>
        </div>
        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center" style="text-align: center;">

                    <table cellspacing="0" cellpadding="0" border="0" rules="rows" id="gvAnnounce" class="mtab" style="border-color: #EFEFEF; border-width: 1px; border-style: solid; font-size: 12px; width: 100%;">
                        <tr style="background-color: #A4B6D7;">
                            <th scope="col" style="width: 8%;">序号</th>
                            <th scope="col" style="width: 15%;">文件名</th>
                            <th scope="col" style="width: 15%;">日期</th>
                            <th scope="col" style="width: 15%;">尺寸</th>
                            <th scope="col" style="width: 15%;">大小</th>
                            <th scope="col" style="width: 10%;">预览</th>
                            <asp:Literal ID="Literal_Choice" runat="server"></asp:Literal>
                        </tr>


                        <%=strMange%>
                    </table>

                    <asp:Table ID="Table1" runat="server">
                    </asp:Table>
                </td>
            </tr>
        </table>
        <div class="listPage">
            <div length="0" class="prolistpager_3">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server"
                    CurrentPageButtonClass="page-num-Current" EnableUrlRewriting="true"
                    MoreButtonsClass="page-num" NumericButtonCount="10"
                    NumericButtonTextFormatString="{0}" OnPageChanged="AspNetPager1_PageChanged"
                    PageSize="50" PagingButtonsClass="page-num" ShowFirstLast="False"
                    ShowPageIndexBox="Never" ShowPrevNext="False" UrlRewritePattern=""
                    Width="100%" AlwaysShow="True">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </form>
    <div style="margin-left: 30px;">
        上传的多媒体文件有格式和大小限制，如下：<p>
            &nbsp;
        </p>
        &nbsp;图片（image）: 128K，支持JPG格式

        <p>
            &nbsp;
        </p>
        语音（voice）：256K，播放长度不超过60s，支持AMR\MP3格式

        <p>
            &nbsp;
        </p>
        视频（video）：1MB，支持MP4格式

        <p>
            &nbsp;
        </p>
        缩略图（thumb）：64KB，支持JPG格式
    </div>
</body>
</html>
