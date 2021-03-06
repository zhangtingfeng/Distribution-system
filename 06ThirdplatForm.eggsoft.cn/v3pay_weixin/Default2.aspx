<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="_06ThirdplatForm.eggsoft.cn.v3pay_weixin.Default2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="~/Scripts/jquery-1.10.2.js"></script>
    <script src="~/Scripts/components/core-min.js"></script>
    <script src="~/Scripts/components/sha1.js"></script>

    <script>
        function getappid() {
            return "wxb97da79b8bad5e74"; //换成自已的appid
        }
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        function getTimeStamp() {
            var timestamp = new Date().getTime();
            var timestampstring = timestamp.toString();//一定要转换字符串
            oldTimeStamp = timestampstring;
            return timestampstring;
        }

        //得到随机字符串
        function getNonceStr() {
            var $chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
            var maxPos = $chars.length;
            var noceStr = "";
            for (i = 0; i < 32; i++) {
                noceStr += $chars.charAt(Math.floor(Math.random() * maxPos));
            }
            oldNonceStr = noceStr;
            return noceStr;
        }
        //获取CODE
        var getcodeobj = {
            appid: getappid(),
            redirect_uri: "",
            response_type: "code",
            scope: "snsapi_base",
            state: "1"
        };

        //拼接url传参字符串
        function perapara(objvalues, isencode) {
            var parastring = "";
            for (var key in objvalues) {
                isencode = isencode || false;
                if (isencode) {
                    parastring += (key + "=" + encodeURIComponent(objvalues[key]) + "&");
                }
                else {
                    parastring += (key + "=" + objvalues[key] + "&");
                }
            }
            parastring = parastring.substr(0, parastring.length - 1);
            return parastring;
        }
        //得到用户code
        function getcode() {
            var code = getQueryString("code");
            if (!code) {
                var getcodeparas = $.extend(getcodeobj, {
                    redirect_uri: window.location.href
                });
                window.location.href = "https://open.weixin.qq.com/connect/oauth2/authorize?" + perapara(getcodeparas) + "#wechat_redirect";
            }
            else {
                return code;
            }
        }
        //得到用户accesstoken
        function getaccesstoken(code) {
            var url = "/wechat.oauth/GetAccessToken";
            $.ajax({
                type: "POST",  //默认是GET
                dataType: "text",
                url: url,
                data: "code=" + code,
                async: false,  //异步
                cache: false, //不加载缓存
                success: function (obj) {
                    access_tokenstring = obj;
                    isaccget = true;
                },
                error: function (req, msg, ex) {
                    $("#showerror").val(req.responseText.toString());
                }
            });

        }
        function getSign(beforesingstring) {
            sign = CryptoJS.SHA1(beforesingstring).toString();
            return sign;
        }

        var signparasobj = {
            "accesstoken": "",
            "appid": getappid(),
            "noncestr": "",
            "timestamp": "",
            "url": ""
        };
    </script>
</head>
<body>
    <div>
        <label>showerror</label>
        <textarea id="showerror"></textarea>
        <!--<div id="showerror"></div>-->
        <label>code</label><input type="text" id="txtcode" /><br />

        <textarea id="txtinfo"></textarea>
        <label>accesstoken</label><input type="text" id="txtaccesstoken" />
    </div>

    <div id="showtestresult"></div>
    <label for="redhref">href测试</label>
    <input type="text" id="redhref" /><br />
    <label for="redhref">加密前参数</label>
    <input name="44" id="signpre" type="text" /><br />

    <input name="44" id="thisurl" type="text" /><br />
    <input name="33" id="thisurl2" type="text" /><br />
    <input type="button" id="getaddress2" onclick="editAddress()" value="得到地址方式2" /><br />
    <input name="address1" id="address1" type="text" /><br />
    <input name="address2" id="address2" type="text" /><br />
    <input name="address3" id="address3" type="text" /><br />

    <div id="divinfo"></div>
    <div id="resvalues">aaaaaaaaS</div>
    <script>
        var codestring = "";
        var access_tokenstring = "";
        var oldTimeStamp;//保存timestamp，提交用
        var oldNonceStr; //保存nonceStr,提交用
        var sign;
        var isaccget = false;

        $(document).ready(
            function () {
                codestring = getcode();
                $("#thisurl").val(window.location.href);
                $("#txtcode").val(codestring);
                getaccesstoken(codestring);
                $("#txtaccesstoken").val(access_tokenstring);

            });
        function editAddress() {
            var showobj2 = txtinfo != null ? txtinfo : document.getElementById("txtinfo");
            showobj2.value = '进入微信事件';
            $("#thisurl2").val(window.location.href);
            //签名
            var signparas = $.extend(signparasobj, {
                "accesstoken": access_tokenstring,
                "noncestr": getNonceStr(),
                "timestamp": getTimeStamp(),
                "url": window.location.href
            });
            $("#signpre").val(perapara(signparas));
            //签名
            var signstring = getSign(perapara(signparas));
            if (isaccget) {
                WeixinJSBridge.invoke('editAddress',
                    {
                        "appId": getappid(),
                        "scope": "jsapi_address",
                        "signType": "sha1",
                        "addrSign": signstring,
                        "timeStamp": oldTimeStamp,
                        "nonceStr": oldNonceStr
                    }
                    ,
                    function (res) {
                        var ff = ''; var obj = resvalues != null ? resvalues : document.getElementById('resvalues'); if (res == null) { obj.innerText = '测试返回为空'; }
                        else {
                            for (var key in res)
                            { var js = 'res.' + key + ' = ' + res[key].toString(); ff = ff + js; }
                            obj.innerText = ff;
                            document.form1.address1.value = res.proviceFirstStageName;
                            document.form1.address2.value = res.addressCitySecondStageName;
                        }
                    });
            }
        }
    </script>
</body>

</html>