function returnSubmitData() {
    var BuyOrderShopUserID = document.getElementById("BuyOrderShopUserID").value;
    var BuyOrderUserRealName = document.getElementById("BuyOrderUserRealName").value;
    var BuyParentShopUserID = document.getElementById("BuyParentShopUserID").value;
    var BuyGrandParentShopUserID = document.getElementById("BuyGrandParentShopUserID").value;
    var UserEmail = document.getElementById("UserEmail").value;
    var UserExtraMemo = document.getElementById("UserExtraMemo").value;
    var NetUserSafeCode = document.getElementById("NetUserSafeCode").value;

    t2 = hex_md5_8(BuyOrderShopUserID + BuyOrderUserRealName + BuyParentShopUserID + BuyGrandParentShopUserID + UserEmail + UserExtraMemo + NetUserSafeCode);
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
    var Usertel = document.forms['testForm'].UserTel;
    var UserEmail = document.forms['testForm'].UserEmail;


    var BuyOrderShopUserID = BuyOrderShopUserID.value;
    var Usertel = Usertel.value;
    var UserEmail = UserEmail.value;

    if (BuyOrderShopUserID == "") {
        alert("请输入7天内下过单的用户ID！");
        return false;
    }
    if (validatemobile(Usertel) == false) {
        alert("手机号码非法！");
        return false;
    }
    if (UserEmail == "") {
        return true;
    }
    else if (!(validateEmail(UserEmail))) {
        alert("邮件地址非法！");
        return false;
    }
    else {

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


function inputCheckInfo() {
    // debugger;
    var BuyOrderShopUserID = document.getElementById("BuyOrderShopUserID").value;
    var NetUserSafeCode = document.getElementById("NetUserSafeCode").value;
    var t2 = hex_md5_8(BuyOrderShopUserID + varShopClientID + varUserID + NetUserSafeCode);



    var url = varServiceURL + "/Order/DoOperationCenter.asmx/_Service_Check_ParnetID_OperationCenter?";
    url += "BuyOrderUserID=" + BuyOrderShopUserID;
    url += "&JSUserSign=" + t2;
    url += "&ShopClientID=" + varShopClientID;
    url += "&UserID=" + varUserID;

    //loading带文字
    layer.open({
        type: 2
      , content: '自动检测数据中', time: 2
    });

    var result = -1;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "jsonp",
        jsonp: "jsonp", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonp201704170623Callback", //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
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
            } else if (result == -68) {
                //提示
                layer.open({
                    content: '只能申请首次下单的客户，当前用户已经下过单'
                 , skin: 'msg'
                 , time: 5 //2秒后自动关闭
                });
            } else if (result == -58) {
                //提示
                layer.open({
                    content: '未找到有效订单，建议用户下过单后，再申请调整上下级关系'
                  , skin: 'msg'
                  , time: 5 //2秒后自动关闭
                });
            } else if (result == 5) {
                layer.open({
                    content: '验证通过,可继续申请'
                 , type: 2
                 , time: 2 //2秒后自动关闭
                });
            }
            else if (result == 1) {

                document.getElementById("BuyOrderUserRealName").value = decodeURIComponent(json.ShowInfo.UserIDRealname);
                document.getElementById("BuyParentShopUserID").value = json.ShowInfo.UserParentIDShopUserID;
                document.getElementById("BuyGrandParentShopUserID").value = json.ShowInfo.UserGrandParentIDShopUserID;

                var varShowInfo = "";
                varShowInfo += "请检查自动带出的数据是否正确。";
                varShowInfo += " <br />上级信息（ID为" + json.ShowInfo.UserParentIDShopUserID + " 姓名为" + decodeURIComponent(json.ShowInfo.UserParentIDRealname) + "）";
                varShowInfo += " <br />上上级信息（ID为" + json.ShowInfo.UserGrandParentIDShopUserID + " 姓名为" + decodeURIComponent(json.ShowInfo.UserGrandParentIDRealname) + "）";

                layer.open({
                    content: varShowInfo
                 , btn: '好'
                });
            }

            return;
        },
        error: function () {
        }
    });
    return result;
}