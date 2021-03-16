<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_12upload.eggsoft.cn.Control.UploadControl.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="/Control/UploadControl/scripts/uploadify.css?version=css201508180713" rel="stylesheet" type="text/css" />
    <link href="/Control/UploadControl/scripts/default.css?version=css201508180713" rel="stylesheet" type="text/css" />
    <script src="/Control/UploadControl/scripts/jquery-1.7.2.min.js?version=js201508180713" type="text/javascript"></script>
    <script src="/Control/UploadControl/scripts/swfobject.js?version=js201508180713" type="text/javascript"></script>
    <script src="/Control/UploadControl/scripts/jquery.uploadify.min.js?version=js201508180713" type="text/javascript"></script>
    <script type="text/javascript">

        var BaseUrl = '<%=BaseUrl%>';
        var pathU = '/Control/UploadControl/scripts/Handler.ashx?op=1&BaseUrl=' + BaseUrl;

        $(function () {
            $("#file_upload").uploadify({
                //开启调试
                'debug': false,
                //是否自动上传
                'auto': false,
                'buttonText': '选择照片',
                //flash
                'swf': "/Control/UploadControl/scripts/uploadify.swf",
                //文件选择后的容器ID
                'queueID': 'uploadfileQueue',
                'uploader': pathU,
                'width': '75',
                'height': '24',
                'multi': false,
                'fileTypeDesc': '支持的格式：',
                'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png',
                'fileSizeLimit': '1MB',
                'removeTimeout': 1,

                //返回一个错误，选择文件的时候触发
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    //__doPostBack('UpdaterButton', '');
                }
            });
        });

        function UpdatePanel2_doUplaod() {

            setTimeout(function () {
                // IE	
                if (document.all) {
                    document.getElementById("Button1").click();
                }
                // 其它浏览器	
                else {
                    var e = document.createEvent("MouseEvents");
                    e.initEvent("click", true, true);
                    document.getElementById("Button1").dispatchEvent(e);
                }
            }, 2000);

            __doPostBack('<%= UpdatePanel2.ClientID %>', '');
        }


        function doUplaod() {
            $('#file_upload').uploadify('upload', '*');
            //setTimeout(UpdatePanel2_doUplaod, 2000);
            UpdatePanel2_doUplaod();
        }

        function closeLoad() {
            $('#file_upload').uploadify('cancel', '*');
        }

        function changeImg(element) {
            debugger;
            var ID = element.id;
            var varSrc = $(element).attr("src");
            //var varSrc = $(element).attr("src");
            window.returnValue = varSrc;
            //alert(window.returnValue);
            //window.close();//
            //'strThis = strThis + strThis;
            //----------方法二--start-------   
            //var obj = window.dialogArguments; //父页面对象   
            //obj.elements["txtReturnValue"].value = varSrc; //给父页面对象赋值   
            // obj.elements["fag2"].value = TNO;
            //----------方法二--end-------   
            //setCookie("txtReturnValue", varSrc);
            //alert(getCookie("txtReturnValue"));
            var pWindow = window.dialogArguments;
            //alert(varSrc);
            //pWindow.document.getElementById("txtReturnValue").value = varSrc;
            //top.document.getElementById('txtReturnValue').value = varSrc;
            //session.setAttribute("txtReturnValue", varSrc);
            //alert(varSrc);

            if (window.opener != undefined && window.opener.ParentOpenTest != undefined) { //forchrome 
                window.opener.ParentOpenTest(varSrc); //关闭前调用父窗口方法
            }
            else {
                window.returnValue = contentIds;
            }
            window.close();


        }

        function deleteImg() {
            if (confirm("确定删除吗")) {
                var strimg = "";
                $(".AClick").each(function (i, n) {
                    strimg += $(this).children(":eq(0)").attr("src") + "|";
                })
                if (strimg != "") {
                    $.ajax({
                        url: "/Control/UploadControl/scripts/Handler.ashx?op=" + 2,
                        type: "post",
                        data: { "strImg": strimg },
                        datatype: "json",
                        timeout: 10000,
                        async: true,
                        success: function (msg) {
                            $(".AClick").remove();
                        }
                    });
                } else {
                    alert("请选择！");
                }
            }
        }

        function UpdateColor(obj) {
            if ($(obj).parent().attr("class") == "AClick") {
                $(obj).parent().removeClass("AClick");
            } else {
                $(obj).parent().addClass("AClick");
            }
        }

    </script>
    <style type="text/css">
        .AClick {
            background-color: black;
        }

        .displayNone {
            display: none;
        }

        .worksbox {
            width: 100%;
            margin: 0px;
            text-align: center;
        }

            .worksbox a {
                border: 1px solid #ccc;
                /*background-color: #eee;*/
                padding: 5px;
                display: block;
            }

                .worksbox a:hover {
                    border: 1px solid black;
                    background-color: black;
                    text-decoration: none;
                }

                .worksbox a span {
                    display: none;
                    text-align: center;
                    font-size: 12px;
                }

                .worksbox a:hover span {
                    color: yellow;
                    display: block;
                    background-color: black;
                    width: 132px;
                    position: absolute;
                    top: 95px;
                    left: 0px;
                    line-height: 20px;
                }

                .worksbox a img {
                    max-width: 120px;
                    max-height: 120px;
                }

        #UpdatePanel2 {
            margin: 0px;
        }

        .divpage ul {
            list-style: none;
            margin-left: 25%;
        }

        .divpage li {
            float: left;
            margin-left: 20px;
        }

        #UpdatePanel1 {
            display: block;
            text-align: center;
        }

        #divpage {
            width: 960px;
            display: block;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="worksbox">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" style="margin: 0 auto; width: 700px; height: 500px; overflow-y: scroll; border: 1px solid;">
                <ContentTemplate>
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="5" HorizontalAlign="Center"
                        AutoPostBack="true" RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <a href="#">
                                <asp:Image ID="Image1" ondblclick="changeImg(this)" onclick="UpdateColor(this)" runat="server"
                                    ImageUrl='<%#getPath(Eval("Name").ToString())%>' />
                            </a>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Timer ID="Timer1" runat="server" Interval="10000000" OnTick="Timer1_Tick">
                    </asp:Timer>
                    <%--<% =DateTime.Now.ToString()%> --%>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" CssClass="displayNone" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divpage" class="divpage">
                    <ul>
                        <li>
                            <asp:Button ID="btnFirst" runat="server" Text="首页" OnClick="btnFirst_Click" /></li>
                        <li>
                            <asp:Button ID="btnPrev" runat="server" Text="上一页" OnClick="btnPrev_Click" /></li>
                        <li>
                            <asp:Button ID="btnNext" runat="server" Text="下一页" OnClick="btnNext_Click" /></li>
                        <li>
                            <asp:Button ID="btnLast" runat="server" Text="尾页" OnClick="btnLast_Click" /></li>
                        <li style="display: none;">
                            <input id="Button2" type="button" onclick="deleteImg()" value="删除" /></li>
                    </ul>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <br />
    <table width="300" border="0" align="center" cellpadding="0" cellspacing="0" id="__01" style="display: block; clear: both;">
        <tr>

            <td height="50" >
               
                <div id="file_upload">
                </div>
            </td>

            <td height="50" align="center" valign="middle" colspan="2">
                <br />
                <div id="uploadfileQueue" style="padding: 3px;">
                </div>
            </td>

            <td height="50" align="center" valign="middle" colspan="2" style="margin-top:5px;padding-top:12px;">
                <%--                <img alt="" src="images/FreshUpload.gif" width="77" height="23" onclick="UpdatePanel2_doUplaod()"
                    style="cursor: hand" />--%>
                <img alt="" src="images/BeginUpload.gif" width="77" height="23" onclick="doUplaod()"
                    style="cursor: hand" />
                <img alt="" src="images/CancelUpload.gif" width="77" height="23" onclick="closeLoad()"
                    style="cursor: hand" />

            </td>
        </tr>
    </table>
</body>
</html>
