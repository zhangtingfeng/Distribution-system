function show_hidden_Money() {
    var b = document.getElementById("Span1ClickButton_Shoping_Money").checked;
    if (b == true) {
        disp_prompt_Money()
    } else {
        if (b == false) {
            MakeFalseItRollBack_Money()
        }
    }
    ShowPriceAuto();
}
function disp_prompt_Money() {
    var bMoney = document.getElementById("CanCheckMoney1").innerHTML;///现金
    //MakeFalseItRollBack_Other(1);
    document.getElementById("Span1ClickButton_Shoping_Money").checked = true;
    document.getElementById("NowCanUseMoneyShowText").style.display = "inline";
    document.getElementById("NowCanUseMoney1").innerHTML = bMoney;
    document.getElementById("SpanOnleyOneShow_Number_Money").innerHTML = "1"
}
function MakeFalseItRollBack_Money() {
    document.getElementById("NowCanUseMoneyShowText").style.display = "none";
    document.getElementById("Span1ClickButton_Shoping_Money").checked = false;
    document.getElementById("SpanOnleyOneShow_Number_Money").innerHTML = "0"
}
function ShenMaDongliClickButtonClick_1() {
    var c = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (c) {
        document.getElementById("iosCheck_Shoping_Money").click()
    } else {
        var d = document.createEvent("MouseEvents");
        d.initEvent("click", true, true);
        document.getElementById("iosCheck_Shoping_Money").dispatchEvent(d)
    }
}


function show_hidden_Money_Vouchers() {
    var b = document.getElementById("Span2ClickButton_WeiBaiQuan").checked;
    if (b == true) {
        disp_prompt_Money_Vouchers()
    } else {
        if (b == false) {
            MakeFalseItRollBack_Money_Vouchers();///购物积分
        }
    }
    ShowPriceAuto();
}



function disp_prompt_Money_Vouchers() {
    //debugger;
    var b = document.getElementById("CanCheckMoney2_Vouchers").innerHTML;//可用购物券
    MakeFalseItRollBack_Other(2);
    document.getElementById("Span2ClickButton_WeiBaiQuan").checked = true;
    if (document.getElementById("NowCanUseMoney_Vouchers_ShowText") != null) {
        document.getElementById("NowCanUseMoney_Vouchers_ShowText").style.display = "inline";

    }
    if (document.getElementById("NowCanUseMoney2_Vouchers") != null) {
        document.getElementById("NowCanUseMoney2_Vouchers").innerHTML = b;
    }
    if (document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean") != null) {
        document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML = "2"
    }
}
function MakeFalseItRollBack_Money_Vouchers() {
    if (document.getElementById("NowCanUseMoney_Vouchers_ShowText") != null) {
        document.getElementById("NowCanUseMoney_Vouchers_ShowText").style.display = "none";
    }
    if (document.getElementById("Span2ClickButton_WeiBaiQuan") != null) {
        document.getElementById("Span2ClickButton_WeiBaiQuan").checked = false;
    }
    if (document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean") != null) {
        document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML = "0"
    }
}

function show_hidden_Money_Wealth() {
    var b = document.getElementById("Span2ClickButton_Wealth").checked;
    if (b == true) {
        disp_prompt_Money_Wealth()
    } else {
        if (b == false) {
            MakeFalseItRollBack_Money_Wealth()
        }
    }
    ShowPriceAuto();
}

function disp_prompt_Money_Wealth() {
    //debugger;
    var b = document.getElementById("CanCheckMoney2_Wealth").innerHTML;///可用财富积分
    document.getElementById("Span2ClickButton_Wealth").checked = true;
    if (document.getElementById("NowCanUseMoney_Wealth_ShowText") != null) {
        document.getElementById("NowCanUseMoney_Wealth_ShowText").style.display = "inline";

    }
    if (document.getElementById("NowCanUseMoney2_Wealth") != null) {
        document.getElementById("NowCanUseMoney2_Wealth").innerHTML = b;
    }
    if (document.getElementById("SpanOnleyOneShow_Number_Wealth") != null) {
        document.getElementById("SpanOnleyOneShow_Number_Wealth").innerHTML = "1"////财富积分
    }
}


function MakeFalseItRollBack_Money_Wealth() {///可用财富积分
    if (document.getElementById("NowCanUseMoney_Wealth_ShowText") != null) {
        document.getElementById("NowCanUseMoney_Wealth_ShowText").style.display = "none";
    }
    if (document.getElementById("Span2ClickButton_Wealth") != null) {
        document.getElementById("Span2ClickButton_Wealth").checked = false;
    }
    if (document.getElementById("SpanOnleyOneShow_Number_Wealth") != null) {
        document.getElementById("SpanOnleyOneShow_Number_Wealth").innerHTML = "0"
    }
}


function ShenMaDongliClickButtonClick_2() {
    var c = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (c) {
        document.getElementById("iosCheck_Shoping_Money_Vouchers").click()
    } else {
        var d = document.createEvent("MouseEvents");
        d.initEvent("click", true, true);
        document.getElementById("iosCheck_Shoping_Money_Vouchers").dispatchEvent(d)
    }
}
function show_hidden_Bean() {
    var b = document.getElementById("Span3ClickButton_Shoping_Bean").checked;
    if (b == true) {
        disp_prompt_Bean()
    } else {
        if (b == false) {
            MakeFalseItRollBack_Bean()
        }
    }
    ShowPriceAuto();
}
function disp_prompt_Bean() {
    var b = document.getElementById("CanCheckBean3").innerHTML;
    MakeFalseItRollBack_Other(3);
    if (document.getElementById("NowCanUseBean3_ShowText") != undefined) document.getElementById("NowCanUseBean3_ShowText").style.display = "inline";
    if (document.getElementById("NowCanUseBean3") != undefined) document.getElementById("NowCanUseBean3").innerHTML = b;
    if (document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean") != undefined) document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML = "3";
    if (document.getElementById("Span3ClickButton_Shoping_Bean") != undefined) document.getElementById("Span3ClickButton_Shoping_Bean").checked = true
}
function MakeFalseItRollBack_Bean() {
    if (document.getElementById("NowCanUseBean3_ShowText") != undefined) document.getElementById("NowCanUseBean3_ShowText").style.display = "none";
    if (document.getElementById("Span3ClickButton_Shoping_Bean") != undefined) document.getElementById("Span3ClickButton_Shoping_Bean").checked = false;
    if (document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean") != undefined) document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML = "0"
}
function ShenMaDongliClickButtonClick_3() {
    var c = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (c) {
        document.getElementById("iosCheck_Shoping_Bean").click()
    } else {
        var d = document.createEvent("MouseEvents");
        d.initEvent("click", true, true);
        document.getElementById("iosCheck_Shoping_Bean").dispatchEvent(d)
    }
}
function show4_hidden_Vouchers() {
    //var b = document.getElementById("Span4Status_Number_Vouchers_Bean_Money").checked;
    var b = document.getElementById("Span4ChangeYouHuiQuan").value != "-1";
    if (b == true) {
        disp_4_prompt_Vouchers()
    } else {
        if (b == false) {
            MakeFalseItRollBack_Vouchers();
            ShowPriceAuto();
        }
    }
}
function disp_4_prompt_Vouchers() {
    var b = prompt("请输入优惠券号码", "");
    select_GouWuQuan_check_pub(b)
}
function select_GouWuQuan_check_pub(argGouWuQuan_Number) {
    var Numname = argGouWuQuan_Number;
    if ((Numname == "") || (Numname == null)) {
        MakeFalseItRollBack_Vouchers();
        return false
    } else {
        if (Numname != "") {
            if (isNaN(Numname)) {
                alert("请输入正确的券号！");
                return false
            }
            //if (Numname.length > 9) {
            //    alert("这是正确的券号吗！");
            //    return false
            //}
            var varPromotePriceWillPay = document.getElementById("CanCheckMoney4").innerHTML;
            var varStrJson = "strNum=" + Numname + "&strUserID=" + getUserID_For_Shopping_Vouchers_js() + "&strvarPromotePriceWillPay=" + varPromotePriceWillPay + "&ToShopCilentID=" + getToShopCilentID_For_Shopping_Vouchers_js();
            $.ajax({
                url: "/Handler/Handler_CheckQuan.ashx",
                type: "POST",
                data: varStrJson,
                datatype: "json",
                success: function (msgJsonData) {
                    var str1 = '{ "name": "cxh", "sex": "man" }';
                    var dataObj = eval("(" + str1 + ")");
                    var msgReturnJsonData = eval("(" + msgJsonData + ")");
                    if ("1" == msgReturnJsonData.ReturnCode) {
                        MakeFalseItRollBack_Other(4);
                        document.getElementById("Quaninfo_4_ShowText_Vouchers").innerHTML = Numname + " ¥" + msgReturnJsonData.ReturnMoney;
                        document.getElementById("Quaninfo_4_ShowText_Vouchers").style.display = "inline";
                        document.getElementById("NowCanUse_Vouchers_Num4").innerHTML = Numname;
                        document.getElementById("NowCanUse_Vouchers_Money4").innerHTML = msgReturnJsonData.ReturnMoney;
                        //document.getElementById("Span4ChangeYouHuiQuan").value = document.getElementById("Quaninfo_4_ShowText_Vouchers").innerHTML;//   //document.getElementById("Span4Status_Number_Vouchers_Bean_Money").checked = true;
                        document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML = "4";
                        ShowPriceAuto();
                    } else {
                        var varError = "";
                        if ("-1" == msgReturnJsonData.ReturnCode) {
                            varError = "券不存在！"
                        } else {
                            if ("-2" == msgReturnJsonData.ReturnCode) {
                                varError = "券已使用！"
                            } else {
                                if ("-3" == msgReturnJsonData.ReturnCode) {
                                    varError = "券不合法！"
                                } else {
                                    if ("-6" == msgReturnJsonData.ReturnCode) {
                                        varError = "未到使用期！"
                                    } else {
                                        if ("-7" == msgReturnJsonData.ReturnCode) {
                                            varError = "已过有效期！"
                                        } else {
                                            if ("-8" == msgReturnJsonData.ReturnCode) {
                                                varError = "不属于该微店！"
                                            } else {
                                                if ("-9" == msgReturnJsonData.ReturnCode) {
                                                    varError = "无券功能！"
                                                } else {
                                                    if ("-5" == msgReturnJsonData.ReturnCode) {
                                                        varError = "非法使用人！"
                                                    } else {
                                                        varError = "意外的错误！"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        MakeFalseItRollBack_Vouchers();
                        alert(varError)
                    }
                }
            })
        }
    }
}
function MakeFalseItRollBack_Vouchers() {
    if (document.getElementById("Quaninfo_4_ShowText_Vouchers") != null) document.getElementById("Quaninfo_4_ShowText_Vouchers").style.display = "none";
    $('#Span4ChangeYouHuiQuan').val(-1).change();     // document.getElementById("Span4Status_Number_Vouchers_Bean_Money").checked = false;
    document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML = "0"
}
function ShenMaDongliClickButtonClick_4() {
    var c = navigator.appName == "Microsoft Internet Explorer" ? true : false;
    if (c) {
        document.getElementById("iosCheck_Shoping_Vouchers").click()
    } else {
        var d = document.createEvent("MouseEvents");
        d.initEvent("click", true, true);
        document.getElementById("iosCheck_Shoping_Vouchers").dispatchEvent(d)
    }
}
function MakeFalseItRollBack_Other(cCurrent) {
    var dNowSelect = document.getElementById("SpanOnleyOneShow_Number_Vouchers_Bean").innerHTML;
    //if (cCurrent != 1) {
    //    if (dNowSelect == "1") {
    //        MakeFalseItRollBack_Money()
    //    }
    //}
    if (cCurrent != 2) {
        if (dNowSelect == "2") {///购物积分
            MakeFalseItRollBack_Money_Vouchers()
        }
    }
    if (cCurrent != 3) {
        if (dNowSelect == "3") {
            MakeFalseItRollBack_Bean()
        }
    }
    if (cCurrent != 4) {
        if (dNowSelect == "4") {////优惠券
            MakeFalseItRollBack_Vouchers()
        }
    }
};

///如果有购物券 就替用户 直接选中
function do_AutoSelectGouWuQuan() {
    setTimeout('auto_do_AutoSelectGouWuQuan_Action()', 2500)///可能是加载的原因吧 必须使用Timer
}
function auto_do_AutoSelectGouWuQuan_Action() {
    var bCanCheckMoney2_Vouchers = document.getElementById("CanCheckMoney2_Vouchers");
    if (bCanCheckMoney2_Vouchers != undefined) {
        var varGouWuQuan = bCanCheckMoney2_Vouchers.innerHTML;
        if (varGouWuQuan > 0) {
            var biosCheck_Shoping_Money_Vouchers = document.getElementById("iosCheck_Shoping_Money_Vouchers");
            if (biosCheck_Shoping_Money_Vouchers != undefined) {
                var bCanCheckMoney2_Vouchers = document.getElementById("SpanOnleyOneShow_Number_Money");

           
                MakeFalseItRollBack_Money_Vouchers();
                biosCheck_Shoping_Money_Vouchers.click();
               
            }
        }
    }
}
////如果检测到负值 就直接纠正所有选择 不可能让出现负值的支付
function do_AutoSelectGiveAnswer() {
    var varPromotePriceWillPay = document.getElementById("PromotePriceWillPay").innerHTML;
    varPromotePriceWillPay = varPromotePriceWillPay.replace("¥", "")
    var varPromotePriceWillPayMoney = parseFloat(varPromotePriceWillPay); //returns 1234.0
    if (varPromotePriceWillPayMoney < 0) {
        MakeFalseItRollBack_Money();//'//'
        MakeFalseItRollBack_Money_Vouchers();//2
        MakeFalseItRollBack_Bean();//3
        MakeFalseItRollBack_Vouchers();//4
        MakeFalseItRollBack_Money_Wealth();//财富积分
        ShowPriceAuto();
    }
}


function Show_ReCountRedWalletAndMoneyAuto_Document(varShowminReturnCountMoney, varShowminCountMoney_Vouchers, varShowminCountMoney_Wealth, varShowminCountMoney_Vouchers_YouHuiQuan) {
    if (document.getElementById('CanCheckMoney1')) {////现金
        // 找到到对应元素
        document.getElementById("CanCheckMoney1").innerHTML = varShowminReturnCountMoney;
        document.getElementById("NowCanUseMoney1").innerHTML = varShowminReturnCountMoney;
        //show_hidden_Money();
    } else {
        // 没有找到找到到对应元素
    }

    if (document.getElementById('CanCheckMoney2_Wealth')) {////财富积分
        // 找到到对应元素
        document.getElementById("CanCheckMoney2_Wealth").innerHTML = varShowminCountMoney_Wealth;
        document.getElementById("NowCanUseMoney2_Wealth").innerHTML = varShowminCountMoney_Wealth;
        //show_hidden_Money();
    } else {
        // 没有找到找到到对应元素
    }


    if (document.getElementById('CanCheckMoney2_Vouchers')) {//微石币  沁加币   
        document.getElementById("CanCheckMoney2_Vouchers").innerHTML = varShowminCountMoney_Vouchers;
        document.getElementById("NowCanUseMoney2_Vouchers").innerHTML = varShowminCountMoney_Vouchers;

        
    } else {

    }

    if (document.getElementById('CanCheckMoney4')) {///购物券 线下发放              
        document.getElementById("CanCheckMoney4").innerHTML = varShowminCountMoney_Vouchers_YouHuiQuan;
        document.getElementById("NowCanUse_Vouchers_Money4").innerHTML = varShowminCountMoney_Vouchers_YouHuiQuan;
        //show4_hidden_Vouchers();
    } else {
        // 没有找到找到到对应元素
    }


    ShowPriceAuto();
    /*
    document.getElementById("CanCheckMoney2_Vouchers").innerHTML = varText;
    document.getElementById("CanCheckMoney4").innerHTML = varText;
    */
}



function onClickCanGetYouHuiQuan() {
    //loading带文字
    layer.open({
        type: 2
      , content: '好的，即将打开可领用页',
        time: 2,
        end: function () {
            window.location.href = "/addfunction/06coupons/indexlist.aspx";
        }
    });

    //var select = document.getElementById("Span4ChangeYouHuiQuan");  // 获取select对象
    ////select.value = val; // 设置选中项

    //if (select.value == "0") {
    //    $('#Span4ChangeYouHuiQuan').val(-100).change();///点击返回时会有默认选中状态 这句话是改变他的状态
    //    window.location.href = "/AddFunction/06coupons/IndexList.aspx";
    //}
    //console.log(select.value);
}



function ChangeCanChoiceYouHuiQuan() {
    var select = document.getElementById("Span4ChangeYouHuiQuan");  // 获取select对象
    //select.value = val; // 设置选中项

    if (select.value == "0") {
        $('#Span4ChangeYouHuiQuan').val(-1).change();
        layer.open({
            type: 2, time: 1,
            content: '好的，即将打开可领用页',
            end: function () {
                window.location.href = "/addfunction/06coupons/indexlist.aspx";
            }
        });
    }
    if (select.value == "-1") {///页面不能选 但是程序可以回选到这里
        document.getElementById("Quaninfo_4_ShowText_Vouchers").style.display = "none";
        select.style.backgroundColor = "#fff";// */
    }
    else {
        var varmingoodmoney = select.options[select.selectedIndex].getAttribute('mingoodmoney');
        var FlaotMingoodmoney = parseFloat(varmingoodmoney);

        var varHowMany = document.getElementById("values").value;
        var IntvarHowMany = parseInt(varHowMany);
        var varGoodMoney = document.getElementById("PromotePriceTotalNoneShow").innerHTML;
        var varFloatGoodMoney = parseFloat(varGoodMoney);
        if (FlaotMingoodmoney > 0)////说明有满足多少才能使用的条件
        {
            if (FlaotMingoodmoney > (IntvarHowMany * varFloatGoodMoney)) {
                layer.open({
                    content: '商品购买金额必须满足¥' + FlaotMingoodmoney + '才能使用，您可以增加购买数量后再试'
                  , skin: 'msg',
                    end: function () {
                        select.style.backgroundColor = "#fff";// */
                        $('#Span4ChangeYouHuiQuan').val(-1).change();
                    }
                  , time: 4 //2秒后自动关闭
                });

                //提示

            }
            else {
                select_GouWuQuan_check_pub(select.value);
                select.style.backgroundColor = "#00e970";// */
            }
        }
        else {///不限制面额的
            select_GouWuQuan_check_pub(select.value);
            select.style.backgroundColor = "#00e970";// */
        }

    };

}


