
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


var check = function () {
    returnSubmitData();

    var UserMoney = document.forms['testForm'].UserMoney;
    var UserPhone = document.forms['testForm'].UserPhone;
    var UserRealName = document.forms['testForm'].UserRealName;


    var UserRealNameTxt = UserRealName.value;
    var UserMoneyTxt = UserMoney.value;
    var UserPhoneTxt = UserPhone.value;

    if (UserRealNameTxt == "") {
        alert("请输入真实姓名！");
        return false;
    }

    if (UserMoneyTxt == "") {
        alert("请输入提款金额！");
        return false;
    }
    else if (UserMoneyTxt != "") {
        var oooo = document.getElementById("#myCanDrawMoneyVar");
        var varCanDrawMoney = $("#myCanDrawMoneyVar").html();
        if (parseFloat(UserMoneyTxt) > parseFloat(varCanDrawMoney)) {
            alert("金额超过可提额度！");
            return false;
        }
        else {

            return true;
        }
    }

    else {

        return true;
    }
}

//分享之后的继续调用
function AfterShareContinuesAskDrawMoney() {
    ShareShopFunction();///每日分享商铺的奖励事件 异步回调
    weChat();
    document.getElementById("testForm").submit();///继续提交

    layer.open({
        type: 2,
        content: "分享朋友圈,正在申请提现",
        time: 2
    });
}


function weChat() {
    $("#mcoverDrawMoney").css("display", "none");  // 分享给好友圈按钮触动函数
}


function btn_weChatShared() {
    // debugger;
    var varCheck = check();
    if (varCheck == true) {

        //var varAgreeConsumerWealthAgreement = localStorage.getItem('ConsumerWealthAgreement_0609');


        if (varboolShowConsumerWealthAgreement == "1") {///表示没有同意过
            showLawDraw();
            return;
        }
        else {
            doDrawMoneyAction();
        }
    }
    return false;///阻止它的form继续提交
}

function doDrawMoneyAction() {
    if (varUserDrawMoneyShareFriend == '0') {////表示 不要分享朋友圈
        document.getElementById("testForm").submit();///继续提交
    }
    else {
        layer.open({
            type: 2,
            content: "最后一步,分享朋友圈,完成提现申请",
            time: 2,
            end: function (layer) {
                $("#mcoverDrawMoney").css("display", "block");  // 分享给好友圈按钮触动函数
            }
        });
    }
}


function showLawDraw() {
    $("#c_backgroundShow_notice").css("display", "block");
    $("#c_noticeShow").css("display", "block");


}
function HideLawDraw() {
    $("#c_backgroundShow_notice").css("display", "none");
    $("#c_noticeShow").css("display", "none");

}
function Askc_notice() {
    //localStorage.setItem("ConsumerWealthAgreement_0609", '12');//////11表示没有同意过   12表示同意过

    HideLawDraw();
    doDrawMoneyAction();
}
