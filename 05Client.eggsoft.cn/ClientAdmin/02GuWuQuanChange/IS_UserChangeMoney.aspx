<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IS_UserChangeMoney.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._02GuWuQuanChange.IS_UserChangeMoney" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>用 户 兑 换 管 理(<asp:Literal ID="Literal_Desc" runat="server"></asp:Literal>)</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <style type="text/css">
        input {
            height: auto;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">


        <script type="text/javascript">


            function IS_Admin_check_Async_ShowFuction(ID, IS_Admin_Passed) {
                //定义一个全局变量来接受$post的返回值
                if (IS_Admin_Passed == "1") {

                    if (confirm('确认已经处理过该申请?该操作不可逆')) {
                    }
                    else {
                        return;
                    }
                }
                var myurl = "IS_UserChangeMoney_Asnc.aspx?ID=" + ID + "&IS_Admin_Passed=" + IS_Admin_Passed;
                var result;

                //用ajax的“同步方式”调用一般处理程序 
                $.ajax({
                    url: myurl,
                    async: true, //改为同步方式
                    type: "get",
                    data: { Sqls: "sql4" },
                    success: function (courseDT4) {
                        var varIS_Admin_check_AsyncStatus = "IS_Admin_check_AsyncStatus" + ID;
                        var vardocumentElement = document.getElementById(varIS_Admin_check_AsyncStatus);

                        if (courseDT4 == "1") {
                            var strinnerHTML = "已处理";
                            //vardocumentElement.innerHTML = strinnerHTML;
                            $(("#" + varIS_Admin_check_AsyncStatus)).html(strinnerHTML);
                        }
                        else if (courseDT4 == "-1") {
                            alert("兑换出错,请联系管理员处理该问题！");
                        }
                    }
                });
                return result;
            }
        </script>

        <div class="mhead">
            <h1>
                用 户 兑 现 管 理(<asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>)</h1>
            <div class="mselct">
                <asp:CheckBox ID="CheckBox_IIS_Admin" runat="server" Text="显示已审核过"
                    AutoPostBack="True" OnCheckedChanged="CheckBox_IIS_Admin_CheckedChanged" />
            </div>
        </div>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td width="100%" valign="top" align="center">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" BorderWidth="0px" BorderColor="#EFEFEF" CellPadding="0"
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="申请编号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divVisibleNone" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ShopUserID" HeaderText="微店号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" HeaderText="申请人">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ContactPhone" HeaderText="手机号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserGouWuQuan" HeaderText="申请购物券">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RemainingSum_Vouchers" HeaderText="购物券余额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RemainingSum" HeaderText="现金余额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateTime" HeaderText="申请时间">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ISpassed" HeaderText="处理状态">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ISpassed" HeaderText="处理过的操作">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="8%" />
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
