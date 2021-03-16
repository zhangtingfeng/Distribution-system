<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IS_UserFinance_check_DrawMoney.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._12UserAskMoney.IS_UserFinance_check_DrawMoney" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>用 户 提 现 管 理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <style type="text/css">
        input {
            height: auto;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">


        <script type="text/javascript">

            function IS_Admin_check_Async_ShowFuction_DoWeiXinHoneBao(ID, strNeedMoney, IS_Admin_check) {
                IS_Admin_check_Async_DoWeiXinHoneBao(ID, strNeedMoney, IS_Admin_check);
            }
            function IS_Admin_check_Async_DoWeiXinHoneBao(ID, strNeedMoney, IS_Admin_check) {
                //定义一个全局变量来接受$post的返回值
                if (IS_Admin_check == "1") {

                    if (confirm('财务人员，这是用户(代理商)提现管理，您的微信支付商户余额是否充足？请确认您要通过直接发放现金进行转账？')) {
                    }
                    else {
                        return;
                    }
                }
                var myurl = "IS_UserFinance_check_DrawMoney_AsncDoWeiXinHoneBao.aspx?ID=" + ID + "&NeedMoney=" + strNeedMoney + "&IS_Admin_check=" + IS_Admin_check;
                var result;

                //用ajax的“同步方式”调用一般处理程序 
                $.ajax({
                    url: myurl,
                    async: true, //改为同步方式
                    type: "POST",
                    data: { Sqls: "sql4" },
                    success: function (courseDT4) {
                        var varIS_Admin_check_AsyncStatus = "IS_Admin_check_AsyncStatus" + ID;
                        var vardocumentElement = document.getElementById(varIS_Admin_check_AsyncStatus);

                        var varIS_Admin_check_AsyncStatus_WeiXinRed = "IS_Admin_check_AsyncStatus_WeiXinRed" + ID;
                        var vardocumentElement_WeiXinRed = document.getElementById(varIS_Admin_check_AsyncStatus_WeiXinRed);

                        if (courseDT4 == "1") {
                            var strinnerHTML = "已转";
                            vardocumentElement.innerHTML = strinnerHTML;
                            vardocumentElement_WeiXinRed.innerHTML = strinnerHTML;

                        }
                        else {
                            alert("可能是微信支付账户余额不足，转账失败" + courseDT4);
                        }
                    }
                });
                return result;
            }
        </script>
        <script type="text/javascript">

            function IS_Admin_check_Async_ShowFuction(ID, IS_Admin_check) {
                IS_Admin_check_Async(ID, IS_Admin_check);
            }
            function IS_Admin_check_Async(ID, IS_Admin_check) {
                //定义一个全局变量来接受$post的返回值
                if (IS_Admin_check == "1") {

                    if (confirm('财务人员，这是用户(代理商)提现管理，您好，请确认您已经进行了财务转账处理\\n（转账是指你已通过第三方平台，支付宝或者银行转账）?')) {
                    }
                    else {
                        return;
                    }
                }

                var myurl = "IS_UserFinance_check_DrawMoney_Asnc.aspx?ID=" + ID + "&IS_Admin_check=" + IS_Admin_check;
                var result;

                //用ajax的“同步方式”调用一般处理程序 
                $.ajax({
                    url: myurl,
                    async: true, //改为同步方式
                    type: "POST",
                    data: { Sqls: "sql4" },
                    success: function (courseDT4) {
                        var varIS_Admin_check_AsyncStatus = "IS_Admin_check_AsyncStatus" + ID;
                        var vardocumentElement = document.getElementById(varIS_Admin_check_AsyncStatus);

                        var varIS_Admin_check_AsyncStatus_WeiXinRed = "IS_Admin_check_AsyncStatus_WeiXinRed" + ID;
                        var vardocumentElement_WeiXinRed = document.getElementById(varIS_Admin_check_AsyncStatus_WeiXinRed);

                        if (courseDT4 == "1") {
                            var strinnerHTML = "已转";
                            vardocumentElement.innerHTML = strinnerHTML;
                            vardocumentElement_WeiXinRed.innerHTML = strinnerHTML;

                        }
                        else {
                            var strinnerHTML = "<a href=\"#\" style=\"color:blue\" onclick=\"IS_Admin_check_Async(" + ID + ",1);\">转过账了</a>";
                            vardocumentElement.innerHTML = strinnerHTML;
                            vardocumentElement_WeiXinRed.innerHTML = strinnerHTML;

                        }
                    }
                });
                return result;
            }
        </script>
        <div class="mhead">
            <h1>用 户 提 现 管 理</h1>
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
                        Width="100%" Font-Size="12px" OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="申请编号">
                                <HeaderStyle Width="7%" CssClass="divVisibleNone" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="divVisibleNone" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ShopUserID" HeaderText="微店号">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                              <asp:BoundField DataField="UserRealName" HeaderText="用户名字">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserID" HeaderText="昵称">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CardName" HeaderText="手机号(微信号?)">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AskMoney" HeaderText="申请金额">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserID" HeaderText="用户现金余额">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdateTime" HeaderText="申请时间">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AskMemo" HeaderText="申请信息">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="IFSendMoney" HeaderText="财务转账状态">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>

                            <asp:BoundField DataField="IFSendMoney" HeaderText="线下转账操作">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                           <%--  <asp:BoundField DataField="payment_noResultCode" HeaderText="微信企业付款信息">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="微信实时转账">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="删除">
                                <HeaderStyle Width="7%" />
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
