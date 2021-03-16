<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Board_TuanGouParterList.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._26WeiTuanGou.Board_TuanGouParterList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>微 分 销 团 购</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 16%;
        }
    </style>
    <script type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">
        function ModifyStatus(varTuanGouID, varPingTuanNum, var1Or0) {
            debugger;
            var varstrPubServicesURL = "<%=strPubServicesURL%>";

            varURL = varstrPubServicesURL + "/Pub/doTuanGou.asmx/doModifyStatus_TuanGou_Number";
            $.ajax({
                type: 'GET',
                url: varURL,
                dataType: "jsonp",
                jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
                data: "strTuanGouID=" + varTuanGouID + "&strtuangouidnumber=" + varPingTuanNum + "&strModifyStatus=" + var1Or0,
                jsonpCallback: "jsonp201607150642Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    document.body.style.cursor = "wait";
                },
                success: function (json) {
                    document.body.style.cursor = "default";
                    result = json.ErrorCode;
                    if (result == 0) {
                        //alert(msg);
                        window.location.reload();///页面自动刷新js版
                    };
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    document.body.style.cursor = "default";
                    console.log(XMLHttpRequest.status);
                    console.log(XMLHttpRequest.readyState);
                    console.log(textStatus);
                }
            })




        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>&nbsp;团 购 单 号 列 表
             
                （<asp:Literal ID="Literal_GoodName" runat="server"></asp:Literal>）
                <asp:Literal ID="Literal_SuccessOrFail" runat="server"></asp:Literal>
            </h1>
        </div>

        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td width="100%" valign="top" align="center">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF"
                        CellPadding="0" Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound"
                        class="mtab">
                        <Columns>
                            <asp:BoundField DataField="tuangouid" HeaderText="团购商品">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="团购单号" DataField="TuanGouIDNumber">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="查看当前人数" DataField="SuccessBuyPeopleCount">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>


                            <asp:BoundField DataField="CreateTime" HeaderText="开团时间">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:CheckBoxField DataField="IFFinshedCurMemberShip" HeaderText="当前状态">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>

                            <asp:BoundField DataField="IFFinshedCurMemberShip" HeaderText="修改状态">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                           <%-- <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="ID" HeaderText="团长二维码">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    </asp:DropDownList>
                    页
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
