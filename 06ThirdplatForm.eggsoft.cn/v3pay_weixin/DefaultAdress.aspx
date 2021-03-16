<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultAdress.aspx.cs" Inherits="_06ThirdplatForm.eggsoft.cn.v3pay_weixin.DefaultAdress" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="width=640, user-scalable=no, target-densitydpi=device-dpi" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="../JS/jquery-1.8.3.js"></script>
    <script src="../JS/sha1.js"></script>
    <link href="../Css/Base.css" rel="stylesheet" />
    <script>

        function QueryStringstate() {
            var name, value, i;
            var varstate = "<%=pub_GetQueryString_state%>";
            //alert(str);
            var num = varstate.indexOf("?")
            str = varstate.substr(num + 1);
            var arrtmp = str.split("&");
            for (i = 0; i < arrtmp.length; i++) {
                num = arrtmp[i].indexOf("=");
                if (num > 0) {
                    name = arrtmp[i].substring(0, num);
                    value = arrtmp[i].substr(num + 1);
                    this[name] = value;
                }
            }
        }


        function getappid() {
            var Request_state = new QueryStringstate();
            var avarppid = Request_state["appid"];
            return avarppid;           
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

        }
        //得到用户accesstoken
        function getaccesstoken(code) {

            access_tokenstring = getQueryString("ScopeAccess_token");
            isaccget = true;

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
    <div style="background-color: rgb(255, 255, 255); display: block;" id="masklayer" class="wx_loading">
        <div class="wx_loading_inner">
            <div class="wx_loading_icon">
                <br />
            </div>
            正在加载...
        </div>
    </div>

    <div style="display: block;">
        <label>showerror</label>
        <textarea id="showerror"></textarea>
        <label>code</label><input type="text" id="txtcode" /><br />
        <textarea id="txtinfo"></textarea>
        <label>accesstoken</label><input type="text" id="txtaccesstoken" />
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

    </div>
    <div style="display: none;" id="resvalues">aaaaaaaaS</div>
    <script>
        var codestring = "";
        var access_tokenstring = "";
        var oldTimeStamp;//保存timestamp，提交用
        var oldNonceStr; //保存nonceStr,提交用
        var sign;
        var isaccget = false;

        $(document).ready(
            function () {
                   getaccesstoken();         
                delaySample();
            });

        function delaySample() {
            setTimeout(delay1, 4000)
            function delay1() {
              
                $("#getaddress2").click();
               

            }
        }

        function editAddress() {

          
            var signparas = $.extend(signparasobj, {
                "accesstoken": access_tokenstring,
                "noncestr": getNonceStr(),
                "timestamp": getTimeStamp(),
                "url": window.location.href
            });

            //$("#signpre").val(perapara(signparas));
            //签名
            //alert(1);
            var signstring = getSign(perapara(signparas));
            if (isaccget) {
                //alert(2);
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
                        //WeixinJSBridge.log(res.err_msg);
                        var varstate = "<%=pub_GetQueryString_state%>";

            

                        try {
                                                   
                            varstate += "&username=" + (encodeURI(res.userName));
                            varstate += "&telNumber=" + (encodeURI(res.telNumber));
                            varstate += "&addressPostalCode=" + (encodeURI(res.addressPostalCode));
                            varstate += "&proviceFirstStageName=" + (encodeURI(res.proviceFirstStageName));
                            varstate += "&addressCitySecondStageName=" + (encodeURI(res.addressCitySecondStageName));
                            varstate += "&addressCountiesThirdStageName=" + (encodeURI(res.addressCountiesThirdStageName));
                            varstate += "&addressDetailInfo=" + (encodeURI(res.addressDetailInfo));
                            varstate += "&nationalCode=" + (encodeURI(res.nationalCode));

                        } catch (err) {
                            //alert("获取失败" + err.name + err.message);
                            //    initBaidu();
                        }
                        finally {

                        }
                           window.location.href = varstate.toLowerCase();
                        /*
                        if (res == null) {

                        }
                        else {
                            for (var key in res) {
                                var js = 'res.' + key + ' = ' + res[key].toString();
                                ff = ff + js;
                            }
                            obj.innerText = ff;
                            document.form1.address1.value = res.proviceFirstStageName;
                            document.form1.address2.value = res.addressCitySecondStageName;
                        }

                        username
                        收货人姓名
                        telNumber
                        收货人电话
                        addressPostalCode
                        邮编
                        proviceFirstStageName
                        国标收货地址第一级地址
                        addressCitySecondStageName
                        国标收货地址第二级地址
                        addressCountiesThirdStageName
                        国标收货地址第三级地址
                        addressDetailInfo
                        详细收货地址信息
                        nationalCode
                        收货地址国家码


                        alert(res.proviceFirstStageName);
                        alert(res.proviceFirstStageName);
                        alert(res.addressCitySecondStageName);
                        alert(res.addressCountiesThirdStageName);
                        alert(res.addressDetailInfo);
                        alert(res.telNumber);
                        var ff = '';
                        var obj = resvalues != null ? resvalues : document.getElementById('resvalues');
                        if (res == null) {
                            obj.innerText = '测试返回为空';
                        }
                        else {
                            for (var key in res) {
                                var js = 'res.' + key + ' = ' + res[key].toString();
                                ff = ff + js;
                            }
                            obj.innerText = ff;
                            document.form1.address1.value = res.proviceFirstStageName;
                            document.form1.address2.value = res.addressCitySecondStageName;
                        }
                        */
                    });
                }
            }
    </script>
</body>
</html>