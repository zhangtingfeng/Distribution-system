// JavaScript Document
//cateList_openSec=0:初始状态,允许展开分类; 1:运动状态,禁止所有鼠标操作; 2:展开状态,允许关闭和切换分类
var cateList_myScroll,cateList_myScroll2,cateList_openSec=0,
cate_mainCainter = $("#cate_mainCainter"),
cate_secCainter = $("#cate_secCainter"),
cate_mainCainter_li,cate_mainCainter_a;

function cateList_cateloadSuccess (d,showEmptyCate) {
	allData=d;
	//alert(JSON.stringify(d))
	for(var i=d.length-1;i>=0;i--){
		var cate = d[i];
		var pcate = getCateByID(d[i].parentID);
		if(pcate){
			pcate.itemCount +=cate.itemCount
		}
	}
	if(showEmptyCate){
		for(var i=d.length-1;i>=0;i--){
			var cate = d[i];
			if(cate.itemCount<=0){
				d.splice(i,1)
			}
		}
	}
	$.getTmpl('/tmpl/cate_1_tmpl.html?jsversion=_191312146',"cate_1_tmpl").done(function () {
		var roots = getChindrenByID("root");
		$("#cate_mainCainter_ul").append($("#cate_1_tmpl").tmpl(roots))
		cateList_loaded();
	})
}
function cateList_loaded () {
	cateList_myScroll = new IScroll('#cate_mainCainter', {useTransition: false,mouseWheel: true,tap: true  });
	cateList_myScroll2 = new IScroll('#cate_secCainter', {useTransition: false,mouseWheel: true,tap: true  });
	cate_mainCainter_li = $("#cate_mainCainter li")
	cate_mainCainter_a = $("#cate_mainCainter a");
	function cate_mainCainterswipeRightHandler(){
		if(cateList_openSec!=2)return;
		cateList_openSec=1;//转为运动状态
		cate_mainCainter.width("100%");
		cate_secCainter.transition({ x: cate_mainCainter.width()+'px' },function(){
			cate_mainCainter.width("100%").removeClass("insert").addClass("normal")
			cate_mainCainter_a.addClass("normal")
			cate_mainCainter_li.removeClass("pointerLi")
			cateList_openSec=0;//转为初始状态
		});
		cate_mainCainter.trigger("returnMainStatus")
	}
	$$("#cate_mainCainter").on("swipeRight",cate_mainCainterswipeRightHandler);
	cate_mainCainter.on("swipeRight",cate_mainCainterswipeRightHandler);
	cate_mainCainter_li.on("tap",cateList_show2Cate)
	setTimeout(function(){
		cateList_myScroll.refresh()
	},2000)
	cate_mainCainter.trigger("CateCreateSuccess")
}
function cateList_show2Cate(event){
	event.preventDefault();
	event.stopPropagation();
	var owner = $(this)
	if(cateList_openSec==1){
		return;
	}else {
		var pw = win.width() *.8;
		if(pw>500)pw=500;
		else if(pw<300)pw=300;
		var w = 0.382*pw;
		cate_secCainter.width(pw-w)
		var cateID = owner.attr("cateID");
		var cates = getChindrenByID(cateID)
		cate_mainCainter.trigger("mainCateItemClcik",getCateByID(cateID))
		$(".chooseCateItem_2").off("click");
		var cate_secCainter_ul = $("#cate_secCainter_ul").empty()
		if(cates && cates.length>0){
			$.getTmpl('/tmpl/cate_2_tmpl.html?jsversion=_191312146',"cate_2_tmpl").done(function () {
				cate_secCainter_ul.append($("#cate_2_tmpl").tmpl(cates));
				setTimeout(function () {
					cateList_myScroll2.refresh();
				}, 10);
			})
			if(cateList_openSec==2){
				 cateList_openScePanel(owner,w)
			}else if(cateList_openSec==0){
				cateList_openSec=1;//转为运动状态
				cate_secCainter.css({x: cate_mainCainter.width()+'px'}).show().transition({ x: w+'px' },function(){
					cateList_openScePanel(owner,w)
				});
			}
		}else{
			cate_mainCainter.trigger("swipeRight")
		}
		
	}
}

function cateList_openScePanel(owner,w){
	cate_mainCainter.width(w).removeClass("normal").addClass("insert");
	cate_mainCainter_a.removeClass("normal");
	cate_mainCainter_li.removeClass("pointerLi");
	owner.addClass("pointerLi").find("a").addClass("normal");
	cate_mainCainter.trigger("CateSecPanelOpenSuccess",owner.attr("cateID"))
	cateList_openSec=2;//转为展开状态
	$(".chooseCateItem_2").off("tap").on("tap",function(event){
		event.preventDefault();
		event.stopPropagation();
		cate_mainCainter.trigger("cateList_secLiClick",$(this).attr("cateID"))
	})
}