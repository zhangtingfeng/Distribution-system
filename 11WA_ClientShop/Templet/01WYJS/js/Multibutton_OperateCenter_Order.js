function MultiLoad() {
    wol();///请输入7天内的支付时间
}

///请输入7天内的支付时间
function wol() {
    var mydateInput = document.getElementById("BuyOrderPayTime");
    var date = new Date();
    var dateString = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2);
    mydateInput.value = dateString;
}


function returnSubmitData() {
    var BuyOrderShopUserID = document.getElementById("BuyOrderShopUserID").value;
    var BuyOrderPaySerialNumber = document.getElementById("BuyOrderPaySerialNumber").value;
    //var BuyOrderUserRealName = document.getElementById("BuyOrderUserRealName").value;
    var BuyParentShopUserID = document.getElementById("BuyParentShopUserID").value;

    var UserTel = document.getElementById("UserTel").value;
    var UserEmail = document.getElementById("UserEmail").value;
    var UserExtraMemo = document.getElementById("UserExtraMemo").value;
    var ShopClientID = document.getElementById("ShopClientID").value;
    var UserID = document.getElementById("UserID").value;


    var NetUserSafeCode = document.getElementById("NetUserSafeCode").value;

    t2 = hex_md5_8(BuyOrderShopUserID + BuyOrderPaySerialNumber + BuyParentShopUserID + UserTel + UserEmail + UserExtraMemo + ShopClientID + UserID + NetUserSafeCode);
    document.getElementById("JSUserSign").value = t2
    return true;
}



function validatemobile(mobile) {
    var myreg = /^((1)+\d{10})$/;


    if (mobile.length == 0) {
        alert('请输入手机号码！');
        // document.testForm.UserPhone.focus();
        return false;
    } else if (mobile.length != 11) {
        alert('请输入有效的手机号码！');
        //  document.testForm.UserPhone.focus();
        return false;
    } else if (!myreg.test(mobile)) {
        alert('请输入有效的手机号码！');
        //  document.testForm.UserPhone.focus();
        return false;
    } else {
        return true;
    }

}

function validateEmail(eEmail) {

    var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    if (!myreg.test(eEmail)) {
        alert('提示\n\n请输入有效的E_mail！');

        return false;
    }
    return true;
}

var check = function () {
    returnSubmitData();

    var BuyOrderShopUserID = document.forms['testForm'].BuyOrderShopUserID;
    var BuyOrderPaySerialNumber = document.forms['testForm'].BuyOrderPaySerialNumber;
    var Usertel = document.forms['testForm'].UserTel;
    var UserEmail = document.forms['testForm'].UserEmail;
    var BuyOrderPayTime = document.forms['testForm'].BuyOrderPayTime;
    var BuyOrderCount = document.forms['testForm'].BuyOrderCount;


    var varBuyOrderShopUserID = BuyOrderShopUserID.value;
    var varBuyOrderPaySerialNumber = BuyOrderPaySerialNumber.value;
    var varUsertel = Usertel.value;
    var varUserEmail = UserEmail.value;
    var varBuyOrderPayTime = BuyOrderPayTime.value;
    var varBuyOrderCount = BuyOrderPayTime.BuyOrderCount;



    if (varBuyOrderShopUserID == "") {
        alert("请输入下单的用户ID！");
        return false;
    }

    var varBuyOrderShopUserIDIDCard = document.getElementById("BuyOrderShopUserIDIDCard").value;
    if (isNull(varBuyOrderShopUserIDIDCard)) {
        alert("下单人身份证号码必须录入！");
        return false;
    }
    if ((varBuyOrderShopUserIDIDCard.length != 15 && varBuyOrderShopUserIDIDCard.length != 18)) {
        alert("下单人身份证号码位数不是18位或者15位！");
        return false;
    }


    if (inputCheckInfo_BuyOrderShopUserIDIDCard() == false) {
        alert("请输入正确的身份证号码！");
        return false;
    }
    if (varBuyOrderPaySerialNumber == "") {
        alert("请输入支付流水号！");
        return false;
    }

    if (varBuyOrderCount > 100 || varBuyOrderCount < 1) {
        alert("录入的下单数量非法！");
        return false;
    }

    if (validatemobile(varUsertel) == false) {
        alert("手机号码非法！");
        return false;
    }
    if (varUserEmail == "") {
        return true;
    }
    else if (!(validateEmail(varUserEmail))) {
        alert("邮件地址非法！");
        return false;
    }
    else {

    }


    var arrstart = varBuyOrderPayTime.split("-");
    var arrstartTime = new Date(arrstart[0], arrstart[1] - 1, arrstart[2]);
    if (arrstartTime > new Date) {
        alert("支付时间非法！");
        return false;
    }

    return true;
}


function btn_weAskRequest() {

    // debugger;
    var varCheck = check();
    if (varCheck == true) {
        document.getElementById("testForm").submit();///继续提交
    }
    return false;///阻止它的form继续提交
}

///下单的用户ID
function inputCheckInfo_BuyOrderShopUserID() {
    // debugger;
    var BuyOrderShopUserID = document.getElementById("BuyOrderShopUserID").value;
    var NetUserSafeCode = document.getElementById("NetUserSafeCode").value;
    var t2 = hex_md5_8(BuyOrderShopUserID + varShopClientID + varUserID + NetUserSafeCode);

    ///运营中心下单系统 。检查录入的ID是否存在  并是否是本运营中心的。如果 存在  就自动 带出上级。   
    if (BuyOrderShopUserID == '' || BuyOrderShopUserID == null) {
        layer.open({
            content: '请录入微店ID'
                 , skin: 'msg'
                 , time: 5 //2秒后自动关闭
        });


        return;
    }

    var url = varServiceURL + "/Order/DoOperationCenter.asmx/_Service_Check_ShopUserID_OperationCenter?";
    url += "BuyOrderUserID=" + BuyOrderShopUserID;
    url += "&JSUserSign=" + t2;
    url += "&ShopClientID=" + varShopClientID;
    url += "&UserID=" + varUserID;

    //loading带文字
    layer.open({
        type: 2
      , content: '自动检测下单的用户ID的数据中', time: 2
    });

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201704170907Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = (json.ErrorCode);
            if (result == -88) {
                //提示
                layer.open({
                    content: '签名失败，我们已记录你的IP等访问信息'
                  , skin: 'msg'
                  , time: 5 //2秒后自动关闭
                });
            }
            else if (result == -78) {
                //提示
                layer.open({
                    content: '用户微店ID不存在'
                  , skin: 'msg'
                  , time: 5 //2秒后自动关闭
                });
            } else if (result == 12 || result == 13) {
                layer.open({///不是本运营中心的 。  或者几张表信息不对 信息不吻合  也不auto 带出。
                    content: '验证通过,可继续申请'
                 , type: 2
                 , time: 2 //2秒后自动关闭
                });
            }
            else if (result == 1) {

                document.getElementById("BuyOrderShopUserIDRealName").value = decodeURIComponent(json.ShowInfo.BuyOrderShopUserIDRealName);
                document.getElementById("BuyParentShopUserID").value = json.ShowInfo.BuyParentShopUserID;

                layer.open({///信息不吻合  也不auto 带出。
                    content: '请检查自动带出的上级ID是否正确'
                , type: 2
                , time: 2 //2秒后自动关闭
                });

            }

            return;
        },
        error: function () {
        }
    });
    return result;
}




function inputCheckInfo_BuyOrderShopUserIDIDCard() {
    var varBuyOrderShopUserIDIDCard = document.getElementById("BuyOrderShopUserIDIDCard").value;

    if (isNull(varBuyOrderShopUserIDIDCard) == false && (varBuyOrderShopUserIDIDCard.length == 15 || varBuyOrderShopUserIDIDCard.length == 18)) {
        if (CheckIdCard(varBuyOrderShopUserIDIDCard) == "true") {
            return true;

        }
        else if (varBuyOrderShopUserIDIDCard.length == 18) {
            //提示
            layer.open({
                content: '身份证号码输入错误'
              , skin: 'msg'
              , time: 2 //2秒后自动关闭
            });
        }
    }
    return false;
}


///请输入上级用户ID（直推），对账需要 运营中心下单系统 。上级是否有资质   
function _Service_Check_ShopUserID_OperationCenter_BuyParentShopUserID() {
    // debugger;
    var BuyParentShopUserID = document.getElementById("BuyParentShopUserID").value;
    var NetUserSafeCode = document.getElementById("NetUserSafeCode").value;
    var t2 = hex_md5_8(BuyParentShopUserID + varShopClientID + varUserID + NetUserSafeCode);

    ///运营中心下单系统 。上级是否有资质   

    var url = varServiceURL + "/Order/DoOperationCenter.asmx/_Service_Check_ShopUserID_OperationCenter_BuyParentShopUserID?";
    url += "BuyParentShopUserID=" + BuyParentShopUserID;
    url += "&JSUserSign=" + t2;
    url += "&ShopClientID=" + varShopClientID;
    url += "&UserID=" + varUserID;

    //loading带文字
    layer.open({
        type: 2
      , content: '自动检测上级用户ID数据中', time: 2
    });

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201704170906Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = (json.ErrorCode);
            if (result == -88) {
                //提示
                layer.open({
                    content: '签名失败，我们已记录你的IP等访问信息'
                  , skin: 'msg'
                  , time: 5 //2秒后自动关闭
                });
            }
            else if (result == -1) {
                layer.open({
                    content: '上级录入错误'
                   , skin: 'msg'
                   , time: 5 //2秒后自动关闭
                });
            }

            return;
        },
        error: function () {
        }
    });
    return result;
}


///检查输入的支付流水号
function inputCheckInfo_BuyOrderPaySerialNumber() {
    // debugger;
    var strBuyOrderPaySerialNumber = document.getElementById("BuyOrderPaySerialNumber").value;
    var NetUserSafeCode = document.getElementById("NetUserSafeCode").value;
    var t2 = hex_md5_8(strBuyOrderPaySerialNumber + varShopClientID + varUserID + NetUserSafeCode);

    ///运营中心下单系统 。上级是否有资质   

    if (BuyOrderPaySerialNumber.length < 6) {
        layer.open({
            content: ' 您输入的支付流水号不合法'
                 , skin: 'msg'
                 , time: 5 //2秒后自动关闭
        });
        return;
    }

    var url = varServiceURL + "/Order/DoOperationCenter.asmx/_Service_Check_BuyOrderPaySerialNumber?";
    url += "BuyOrderPaySerialNumber=" + strBuyOrderPaySerialNumber;
    url += "&JSUserSign=" + t2;
    url += "&ShopClientID=" + varShopClientID;
    url += "&UserID=" + varUserID;

    ////loading带文字
    //layer.open({
    //    type: 2
    //  , content: '自动检测支付流水号数据中', time: 2
    //});

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201704170903Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        contentType: "application/json; charset=utf-8",
        success: function (json) {
            result = (json.ErrorCode);
            if (result == -88) {
                //提示
                layer.open({
                    content: '流水号非法或者签名失败，我们已记录你的IP等访问信息'
                  , skin: 'msg'
                  , time: 5 //2秒后自动关闭
                });
            }
            else if (result == 2) {
                layer.open({
                    content: '本支付流水号已经下单成功,现在前往订单表查看吗？'
                    , btn: ['查看订单表', '不要']
                    , yes: function (index) {
                        window.location.href = "/multibutton_showyunyinzhongxinorderdata.aspx";
                        layer.close(index);
                    }
                });
            }

            return;
        },
        error: function () {
        }
    });
    return result;
}


//----------------------------------------------------------
//    功能：检查身份证号码
//  参数：
//    idcard 
//    返回值：
//----------------------------------------------------------
function CheckIdCard(idcard) {
    var Errors = new Array(
    "true",
    "身份证号码位数不对!",
    "身份证号码出生日期超出范围或含有非法字符!",
    "身份证号码校验错误!",
    "身份证地区非法!"
    );
    var area = {
        11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江",
        31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东",
        41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏",
        61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
    }

    var idcard, Y, JYM;
    var S, M;
    var idcard_array = new Array();
    idcard_array = idcard.split("");
    //地区检验 
    if (area[parseInt(idcard.substr(0, 2))] == null) return Errors[4];
    //身份号码位数及格式检验 
    switch (idcard.length) {
        case 15:
            if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 == 0 &&
            (parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/;//测试出生日期的合法性 
            } else {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/;//测试出生日期的合法性 
            }
            if (ereg.test(idcard)) return Errors[0];
            else return Errors[2];

            break;
        case 18:
            //18位身份号码检测 
            //出生日期的合法性检查 
            //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9])) 
            //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8])) 
            if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 &&
            parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/;//闰年出生日期的合法性正则表达式 
            } else {
                ereg = /^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/;//平年出生日期的合法性正则表达式 
            }
            if (ereg.test(idcard)) {//测试出生日期的合法性 
                //计算校验位 
                S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
                + (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
                + (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
                + (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
                + (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
                + (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
                + (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
                + parseInt(idcard_array[7]) * 1
                + parseInt(idcard_array[8]) * 6
                + parseInt(idcard_array[9]) * 3;
                Y = S % 11;
                M = "F";
                JYM = "10X98765432";
                M = JYM.substr(Y, 1);//判断校验位 
                if (M == idcard_array[17]) return Errors[0]; //检测ID的校验位 
                else return Errors[3];
            }
            else return Errors[2];
            break;
        default:
            return Errors[1];
            break;
    }

}