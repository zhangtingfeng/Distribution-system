<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shopping_Vouchers_Board.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._01Shopping_Vouchers.Shopping_Vouchers_Board" %>
<!doctype html>
<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
    layer弹出层不居中解决方案

代码头中加入以下代码即可

<!doctype html>
    --%>
<html>
<head>
    <title>优 惠 券 管  理</title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />


    <style type="text/css">
        input {
            height: auto;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <div class="mhead">
            <h1>优 惠 券 管  理 </h1>

            <div class="mselct">
                管理选项：<asp:Button
                    ID="btnAdd" runat="server" Text="添加优惠券" OnClick="btnAdd_Click" />


            </div>
        </div>
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <tr>
                <td valign="top" align="center">
                    <asp:GridView ID="gvAnnounce" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" GridLines="Horizontal" BorderWidth="0px"
                        BorderColor="#EFEFEF" CellPadding="0" Width="100%" Font-Size="12px"
                        OnRowDataBound="gvAnnounce_RowDataBound" class="mtab">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Vouchers_Title" HeaderText="名称">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Money" HeaderText="金额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AllCount" HeaderText="总数">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LimitHowMany" HeaderText="每人张数">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValidateStartTime" HeaderText="有效期开始日期">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValidateEndTime" HeaderText="有效期结束日期">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>


                            <asp:BoundField DataField="HowToGet" HeaderText="发放方式">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>




                            <asp:BoundField HeaderText="修改">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="使失效">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="使用详情" ItemStyle-CssClass="centerAuto" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <a href="<%# string.Format("javascript:openNewWindow('/ClientAdmin/01Shopping_Vouchers/Shopping_Vouchers_BoardDetail.aspx?Scheme_ID={0}');", Eval("ID")) %>">查看</a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="使用详情" ItemStyle-CssClass="centerAuto" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnMate_No" runat="server" Text='查看' OnClientClick='openNewWindow("/ClientAdmin/01Shopping_Vouchers/Shopping_Vouchers_BoardDetail.aspx?Scheme_ID=<%#Eval("ID")%>")'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <%--    <asp:HyperLinkField HeaderText="使用详情" DataNavigateUrlFields="ID" Target="_blank" DataNavigateUrlFormatString="openNewWindow('/ClientAdmin/01Shopping_Vouchers/Shopping_Vouchers_BoardDetail.aspx?Scheme_ID={0}')" Text="查看">
                                <itemtemplate>
                                 <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="centerAuto" />
                             </itemtemplate>
                            </asp:HyperLinkField>--%>
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
                    </asp:DropDownList>页 </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript" src="../../Scripts/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="../../Scripts/layer.js"></script>

    <script type="text/javascript">
        function openNewWindow(contentURL) {
            layer.open({
                type: 2,
                title: '优惠券使用详情',
                shadeClose: true,
                maxmin: true, //开启最大化最小化按钮
                shade: 0.8,
                //area: ['1280px', '800px'], //宽高
                area: ['80%', '80%'],
                offset: [ '4px', ''],
                //iframe: { src: contentURL }
                content: contentURL //iframe的url  http://test.baoxianduoduo.com/qybpc
            });
        }
    </script>
</body>
</html>
