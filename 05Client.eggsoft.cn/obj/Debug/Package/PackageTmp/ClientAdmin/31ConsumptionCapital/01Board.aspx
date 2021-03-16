<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01Board.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital._01Board" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>消费财富系统</title>
    <script type="text/javascript" src="../../Scripts/Times.js?version=js201709121928"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.js?version=js201709121928"></script>
    <link href="../Image/jquery-calendar.css?version=css201709121928" rel="stylesheet" />
    <script type="text/javascript" src="../Image/jquery-calendar.js?version=js201709121928"></script>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js?version=js201709121928" type="text/javascript"></script>
    <style type="text/css">
        .numberShowInFo_Board {
            width: 20px;
            height: 20px;
            background-color: #F00;
            border-radius: 25px;
            z-index: 1;
            position: absolute;
        }

        .numberShowInFo {
            height: 20px;
            line-height: 20px;
            display: block;
            color: #FFF;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="mhead">
            <h1>消费财富系统
            </h1>
            <div class="mselct">
                <br />
                &nbsp;&nbsp;<asp:Button ID="btnOperationCenter" runat="server" Text="运营中心管理" OnClick="btnbtnOperationCenter_Click" Height="50px" Width="300px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnOperationCenterGoodAndReturn" runat="server" Text="消费商品及财富返还管理" Height="50px" Width="300px" OnClick="btnOperationCenterGoodAndReturn_Click" />
                <br />
                <br />
                &nbsp;&nbsp;<asp:Button ID="Button1FullEveryDay" runat="server" Text="每日运营统计" OnClick="btn08FullEveryDay_Click" Height="50px" Width="300px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2BalanceofPaymentStatistics" runat="server" Text="运营中心收支统计" Height="50px" Width="300px" OnClick="Button2BalanceofPaymentStatistics_Click" />
                <br />
                <br />
                &nbsp;&nbsp;<asp:Button ID="ButtonOperationsCenterMembershipStatistics" runat="server" Text="运营中心会员统计" OnClick="btnbtnButtonOperationsCenterMembershipStatistics_Click" Height="50px" Width="300px" />
                &nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button112OrderDetailEveryDay" runat="server" Text="运营中心订单统计" Height="50px" Width="300px" OnClick="Button112OrderDetailEveryDay_Click" />
                <br />
                <br />
                &nbsp;&nbsp;<asp:Button ID="Button14WealthMoneyControlOperationCenter" runat="server" Text="积分管理" OnClick="Button14WealthMoneyControlOperationCenter_Click" Height="50px" Width="300px" />
                &nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button16_16CheckModifyParent" runat="server" Text="运营中心申请调整上下级关系" Height="50px" Width="300px" OnClick="ButtonButton16_16CheckModifyParent_Click" />
                <div class="numberShowInFo_Board" id="Button16_16CheckModifyParento_Board">
                    <asp:Label ID="Label1intInfoAlertMessageExistsCount" runat="server" Text="4" CssClass="numberShowInFo"></asp:Label>
                </div>

                <%-- <span style="border-radius: 50%; height: 20px; width: 20px; display: inline-block; background: #238ff9; vertical-align: top;">
                    <span style="display: block; color: #FFFFFF; height: 20px; line-height: 20px; text-align: center">12</span>
                </span>--%>
            </div>
        </div>



        <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr>
                <td width="100%" valign="top" align="center">&nbsp;</td>
            </tr>
        </table>
    </form>


    <script type="text/javascript">
        ///1.获取相对与document的偏移量
        function getOffsetSum(ele) {
            var top = 0, left = 0;
            while (ele) {
                top += ele.offsetTop;
                left += ele.offsetLeft;
                ele = ele.offsetParent;
            }
            return {
                top: top,
                left: left
            }
        }

        ///2.获取相对与视口的偏移量(viewpoint)加上页面的滚动量(scroll)
        function getOffsetRect(ele) {
            var box = ele.getBoundingClientRect();
            var body = document.body,
              docElem = document.documentElement;
            //获取页面的scrollTop,scrollLeft(兼容性写法)
            var scrollTop = window.pageYOffset || docElem.scrollTop || body.scrollTop,
              scrollLeft = window.pageXOffset || docElem.scrollLeft || body.scrollLeft;
            var clientTop = docElem.clientTop || body.clientTop,
              clientLeft = docElem.clientLeft || body.clientLeft;
            var top = box.top + scrollTop - clientTop,
              left = box.left + scrollLeft - clientLeft;
            return {
                //Math.round 兼容火狐浏览器bug
                top: Math.round(top),
                left: Math.round(left)
            }
        }

        //兼容性写法

        //获取元素相对于页面的偏移
        function getOffset(ele) {
            if (ele.getBoundingClientRect) {
                return getOffsetRect(ele);
            } else {
                return getOffsetSum(ele);
            }
        }



        $(document).ready(function () {
            setLocation("Button16_16CheckModifyParento_Board", "Button16_16CheckModifyParent");
        })
        function setLocation(varBoard, varChlid) {

            var varButton16_16CheckModifyParent = document.getElementById(varChlid);
            var varPos = getOffset(varButton16_16CheckModifyParent);

            var varButton16_16CheckModifyParento_Board = document.getElementById(varBoard);

            if (document.getElementById("Label1intInfoAlertMessageExistsCount").innerText == "0") {
                varButton16_16CheckModifyParento_Board.style.left = -100;
                varButton16_16CheckModifyParento_Board.style.top = varPos.top;
            }
            else {
                varButton16_16CheckModifyParento_Board.style.left = varPos.left;
                varButton16_16CheckModifyParento_Board.style.top = varPos.top;
            }
          
        }
    </script>
</body>
</html>
