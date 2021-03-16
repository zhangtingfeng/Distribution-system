<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IS_FinanceAdmin_check_ReturnMoney.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._13Order_Cancel.IS_FinanceAdmin_check_ReturnMoney" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title> 退 款 管 理</title>
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
            /*
            //这里用了JQUERY，
            function GetRadioValue(RadioName) {
                var obj = document.getElementsByName(RadioName);
                var returnValue = "";
                if (obj != null) {
                    for (var i = 0; i < obj.length; i++) {
                        if (obj[i].checked) {
                            returnValue = obj[i].value;
                        }
                    }
                }
                return returnValue;
            }




            function Show_ShenHE_Cancel() {
                document.getElementById("divModify_check_Goods").style.display = "none";
            }

            function Show_ShenHE_Yes() {
                var varRadioButtonList_DisTri = GetRadioValue("RadioButtonList_DisTri");
                var globalGoodID = document.getElementById("Goodid").value;

                document.getElementById("divModify_check_Goods").style.display = "none";
            }

            */
            function IS_Admin_check_Async_ShowFuction(OrderID, IS_Admin_check) {
                IS_Admin_check_Async(OrderID, IS_Admin_check);
            }
            function IS_Admin_check_Async(OrderID, IS_Admin_check) {
                //定义一个全局变量来接受$post的返回值
                /*var varValue = 0;
                if (typeof (varRadioButtonList_DisTri) != "undefined") {
                    varValue = varRadioButtonList_DisTri;
                }
                */
                if (IS_Admin_check == "1") {
                    if (confirm('财务人员你好，恭喜你选择了最麻烦的操作商户退款管理。\r1.你已选择了线下退款，如果订单已经转化，代理分销所得需要手动追回（例如使用积分管理）。\r2本操作不可撤销。\r3.请确认您已经进行了第三方支付的原路退回处理。\r4.本操作仅仅是删除订单,退回积分等，用户支付的现金不能退回 。\r5如果需要原路返还，请使用订单管理的待发货里面的取消订单 ')) {
                        //alert('选择了是');
                    }
                    else {
                        return;
                    }
                }


                var myurl = "IS_FinanceAdmin_check_ReturnMoney_Asnc.aspx?OrderID=" + OrderID + "&IS_Admin_check=" + IS_Admin_check;
                var result;

                //用ajax的“同步方式”调用一般处理程序 
                $.ajax({
                    url: myurl,
                    async: true, //改为同步方式
                    type: "POST",
                    data: { Sqls: "sql4" },
                    success: function (courseDT4) {
                        var varIS_Admin_check_AsyncStatus = "IS_Admin_check_AsyncStatus" + OrderID;
                        var vardocumentElement = document.getElementById(varIS_Admin_check_AsyncStatus);

                        // var IS_Admin_check_AsyncStatusShow = "IS_Admin_check_AsyncStatusShow" + OrderID;
                        // var vardocumentElement_StatusShow = document.getElementById(IS_Admin_check_AsyncStatusShow);

                        if (courseDT4 == "1") {


                            var strinnerHTML = "已通过";
                            vardocumentElement.innerHTML = strinnerHTML;

                            //var strinnerHTML_Status = "已审";
                            // vardocumentElement_StatusShow.innerHTML = strinnerHTML_Status;

                        }
                        else {
                            var strinnerHTML = "<a href=\"#\" onclick=\"IS_Admin_check_Async(" + OrderID + ",1);\">通过</a>";
                            vardocumentElement.innerHTML = strinnerHTML;
                            //var strinnerHTML_Status = "未审";
                            //vardocumentElement_StatusShow.innerHTML = strinnerHTML_Status;
                        }
                        //history.go(0); //Javascript刷新页面的几种方法：
                    }
                });
                //alert (result);
                return result;
            }
        </script>
        <div class="mhead">
            <h1>退 款 管 理
            </h1>
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
                            <asp:BoundField DataField="OrderID" HeaderText="订单编号">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ShopUserID" HeaderText="用户ID">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NickName" HeaderText="用户昵称">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="RefundMoney" HeaderText="申请退款金额">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="UpdateTime" HeaderText="申请时间">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReturnMoneyContent" HeaderText="附加信息">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                             <asp:BoundField DataField="OrderID" HeaderText="第三方退款链接">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="AdminCheck" HeaderText="系统审核状态" ReadOnly="True" Visible="false">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>
                           
                            <asp:CheckBoxField DataField="FinanceCheck" HeaderText="财务处理状态">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CheckBoxField>
                             <asp:BoundField DataField="FinanceCheck" HeaderText="审核操作">
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
