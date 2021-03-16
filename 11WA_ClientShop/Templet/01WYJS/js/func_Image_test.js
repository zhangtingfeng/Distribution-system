function AutoResizeThisImage(d) {
    var h = [],
    j = document.getElementsByTagName("div");
    for (var g = 0; g < j.length; g++) {
        if (j[g].className == "box") {
            h[h.length] = j[g]
        }
    }
    for (var g = 0; g < h.length; g++) {
        var i = h[g].getElementsByTagName("img");
        AutoResizeImage(i[0])
    }
    return h
}

function getImgNaturalDimensions(img, callback) {
    var nWidth, nHeight
    if (img.naturalWidth) { // 现代浏览器
        nWidth = img.naturalWidth
        nHeight = img.naturalHeight
    } else { // IE6/7/8
        var imgae = new Image()
        image.src = img.src
        image.onload = function () {
            callback(image.width, image.height)
        }
    }
    return [nWidth, nHeight]
}


function AutoResizeImage(s) {

    var p = s.parentNode;
    var b = p.offsetWidth;
    var m = p.offsetHeight;

    //getImgNaturalDimensions(s);
    //var varWidth = getImgNaturalDimensions(img)[0];
    //alert(varWidth);

    var n = new Image();
    //n.onload = function () { alert("img is loaded") };
    //n.onerror = function () { alert("img is onerror") };
    n.src = s.src;
    n.onload;

    var o;
    var t;
    var h = 1;
    var q = n.width;
    var r = n.height;
    //HTML5提供了一个新属性naturalWidth/naturalHeight可以直接获取图片的原始宽高。这两个属性在Firefox/Chrome/Safari/Opera及IE9里已经实现。改造下获取图片尺寸的方法。
//华为的手机 一直是0；
    if (q == 0) q = s.naturalWidth;
    if (r == 0) r = s.naturalHeight;

    t = b / q;
    o = m / r;
    if (b == 0 && m == 0) {
        h = 1
    } else {
        if (b == 0) {
            if (o < 1) {
                h = o
            }
        } else {
            if (m == 0) {
                if (t < 1) {
                    h = t
                }
            } else {
                if (t < 1 || o < 1) {
                    h = (t <= o ? t : o)
                }
            }
        }
    }
    if (h < 1) {
        q = q * h;
        r = r * h
    }
    s.style.height = Math.round(r) + "px";
    s.style.width = Math.round(q) + "px";
    if (r < m) {
        s.style.marginTop = Math.round((m - r) / 2) + "px"
    }
    if (q < b) {
        s.style.marginLeft = Math.round((b - q) / 2) + "px"
    }
    if (!! !s.src) {
        s.src = imgSrc
    }
};