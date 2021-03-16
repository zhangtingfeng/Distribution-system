function adjustShopNavIcon() {
    var allength = document.getElementById("ShopNavIcon").getElementsByTagName("li").length
    //var al = document.getElementById("ShopNavIcon").length;
    //debugger;

    var varSetWidth = "0%";
    if (allength == 1) {
        varSetWidth = "100%";
    }
    else if (allength == 2) {
        varSetWidth = "50%";
    }
    else if (allength == 3) {
        varSetWidth = "33%";
    }
    else if (allength == 4) {
        varSetWidth = "25%";
    }
    else if (allength == 5) {
        varSetWidth = "33%";
    }
    else if (allength == 6) {
        varSetWidth = "33%";
    }
    else if (allength >= 7) {
        varSetWidth = "25%";
    }

    var aShopNavIconLi = document.getElementById("ShopNavIcon").getElementsByTagName("li");
    for (var i = 0; i < allength; i++) {
        aShopNavIconLi[i].style.width = varSetWidth;   //对齐方式
    }
    //debugger;
    //0 1 2    4 5 6      8 9 10  12 13 14
    for (var i = 0; i < allength; i++) {
        if (allength == 1) {

        }
        else if (allength - 1 == i) {

        }
        else if (allength > 1 && (allength == 2) && (i == 0)) {
            InsertbeforeEnd(aShopNavIconLi, i);
        }
        else if (allength > 1 && (allength == 3) && (i == 0 || i == 1)) {
            InsertbeforeEnd(aShopNavIconLi, i);
        }
        else if (allength > 1 && (allength == 4) && (i == 0 || i == 1 || i == 2)) {
            InsertbeforeEnd(aShopNavIconLi, i);
        }
        else if (allength > 1 && (allength == 5) && (i == 0 || i == 1 || i == 3)) {
            InsertbeforeEnd(aShopNavIconLi, i);
        }
        else if (allength > 1 && (allength == 6) && (i == 0 || i == 1 || i == 3 || i == 4)) {
            InsertbeforeEnd(aShopNavIconLi, i);
        }
        else if (allength > 1 && (allength >= 7) && (i % 4 == 0 || (i - 1) % 4 == 0 || (i - 2) % 4 == 0)) {
            InsertbeforeEnd(aShopNavIconLi, i);
        }
    }

    //for (var i = 0; i < allength; i++) {
    //    if (allength == 1) {

    //    }
    //    else if (allength == 2 && i == 0) {
    //        InsertbeforeEnd(aShopNavIconLi, i);
    //    }
    //    else if (allength == 3 && (i == 0 || i == 1)) {
    //        InsertbeforeEnd(aShopNavIconLi, i);
    //    }
    //    else if (allength == 4 && (i == 0 || i == 1 || i == 2)) {
    //        InsertbeforeEnd(aShopNavIconLi, i);
    //    }
    //    else if (allength == 5 && (i == 0 || i == 1 || i == 2)) {
    //        InsertbeforeEnd(aShopNavIconLi, i);
    //    }
    //    //aShopNavIconLi[i].getElementsByClassName("ShowNav")[0].style.color = "#00f";
    //}
}

function InsertbeforeEnd(aShopNavIconLi, vari) {
    aShopNavIconLi[vari].getElementsByClassName("ShowNav")[0].insertAdjacentHTML("beforeEnd", "<div class='ShowNavLine'> </div>");
}

//$(document).ready(function () {
//    adjustShopNavIcon();
//});