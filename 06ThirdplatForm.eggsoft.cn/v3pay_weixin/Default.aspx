<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_06ThirdplatForm.eggsoft.cn.v3pay_weixin.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../JS/jquery-1.8.3.js"></script>
    <script src="https://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script type="text/javascript">
        var varpayModel_AppId = document.getElementById("payModel_AppId").value;
        var varpayModel_Timestamp = document.getElementById("payModel_Timestamp").value;
        var varpayModel_Noncestr = document.getElementById("payModel_Noncestr").value;
        var varpayModel_Package = document.getElementById("payModel_Package").value;
        var varpayModel_PaySign = document.getElementById("payModel_PaySign").value;



        var data = {
            "appId": "" + varpayModel_AppId + "", //公众号名称，由商户传入
            "timeStamp": "" + varpayModel_Timestamp + "", //时间戳
            "nonceStr": "" + varpayModel_Noncestr + "", //随机串
            "package": "" + varpayModel_Package + "",//扩展包   
            "signType": "MD5", //微信签名算法：MD5
            "paySign": "" + varpayModel_PaySign + "" //微信签名
        };

        // 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            //公众号支付

            var iID = setTimeout(function () {
                PayAction();
            }, 10);

        }, false);

        function PayAction() {
            WeixinJSBridge.invoke('getBrandWCPayRequest', data, function (res) {
                //alert(res.err_msg);
                //WeixinJSBridge.log("res.err_msg");
                /**/
                //WeixinJSBridge.log(res.err_msg);
                //alert(res.err_code+res.err_desc+res.err_msg); 
                if (res.err_msg == "get_brand_wcpay_request:ok") {
                    var varpayout_trade_no = document.getElementById("Inputpayout_trade_no").value;

                    var out_trade_no = '' + varpayout_trade_no + ''; //订单号，商户需要保证该字段对于本商户的唯一性

                    self.location = '/v3pay_weixin/CheckIfGetWinXinMoney.aspx?OrderNum=' + out_trade_no;

                    //dosomething
                } else if (res.err_msg == "get_brand_wcpay_request:cancel") {

                    alert("取消支付");
                    window.location.replace('javascript:history.go(-3);')
                } else {
                    alert("支付失败" + res.err_msg);
                    window.location.replace('javascript:history.go(-3);')
                }
            });
        }

        function isWeiXin5() {
            var ua = window.navigator.userAgent.toLowerCase();
            var reg = /MicroMessenger\/[5-9]/i;
            return reg.test(ua);
        }
        window.onload = function () {
            if (isWeiXin5() == false) {
                alert("您的微信版本低于5.0，无法使用微信支付功能，请先升级！");
                //跳转页面
                //window.location.replace('javascript:history.go(-1);')
            }
        };
    </script>
</head>
<body>
    <input id="payModel_AppId" type="hidden" runat="server" />
    <input id="payModel_Timestamp" type="hidden" runat="server" />
    <input id="payModel_Noncestr" type="hidden" runat="server" />
    <input id="payModel_Package" type="hidden" runat="server" />
    <input id="payModel_PaySign" type="hidden" runat="server" />
    <input id="Inputpayout_trade_no" type="hidden" runat="server" />

    <br />
    <br />
    <br />
    <br />
    <a id="getBrandWCPayRequest" class="btn btn-blue mt30" href="javascript:void(0);" style="display: none; font-size: 72px;">确认支付</a>
</body>
</html>

