<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="D.aspx.cs" Inherits="_11WA_ClientShop.Huodong.LightAppCN.D" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <asp:Literal ID="Literal_ShareFriend" runat="server"></asp:Literal>
      <meta name="format-detection" content="telephone=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title><%=strTitleName%></title>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/offline.js"></script>
    <link rel="stylesheet" type="text/css" href="/Huodong/LightAppCN/JS_Css/default.css" />
    <link rel="stylesheet" type="text/css" href="/Huodong/LightAppCN/JS_Css/default.date.css" />
    <link rel="stylesheet" type="text/css" href="/Huodong/LightAppCN/JS_Css/default.time.css" />
    <link rel="stylesheet" type="text/css" href="/Huodong/LightAppCN/JS_Css/main.css" />

    <script type="text/javascript">
        var phoneWidth = parseInt(window.screen.width);
        var phoneScale = phoneWidth / 640;
        var ua = navigator.userAgent;
        if (/Android (\d+\.\d+)/.test(ua)) {
            var version = parseFloat(RegExp.$1);
            if (version > 2.3) {
                document.write('<meta name="viewport" content="width=640, minimum-scale = ' + phoneScale + ', maximum-scale = ' + phoneScale + ', target-densitydpi=device-dpi">');
            } else {
                document.write('<meta name="viewport" content="width=640, target-densitydpi=device-dpi">');
            }
        } else {
            document.write('<meta name="viewport" content="width=640, user-scalable=no, target-densitydpi=device-dpi">');
        }
    </script>
    <!--移动端版本兼容 end -->
</head>
<body>
    <input type="hidden" value='776' id='activeId'><!-- 活动id -->
    <section class="p-index">
		<!--fn-声音提示 -->
				<div class="audio_txt">
					<p class="txt">点击开启/关闭音效</p>
					<p></p>
				</div>
				<!--fn-声音提示 end-->
		
				<!--fn-声音显示 -->
				<section class="fn-audio">
					<div class="btn">
						<p class="btn_audio"><span class='css_sprite01 audio_open'></span><span class='css_sprite01 audio_close'></span></p>
						<audio id="car_audio" controls preload="preload">
							<source src="<%=strLightApp_ID_Mp3Path%>" type="audio/mpeg">
							您的浏览器不支持HTML5音频格式
						</audio>
					</div>
				</section>
				<!--fn-声音显示 end-->
	<%=strintContent%>
										
	<!--pageLoading-->
	<section class="pageLoading">
		<img src="/Huodong/LightAppCN/JS_Css/load.gif" alt="loading" />
	</section>
	<!--pageLoading end-->
	<!--微信分享 -->
	<section class='weixin-share' onclick='cancle_share_weixin(this);'>
		<img class='lazy-bk' data-bk='/Huodong/LightAppCN/JS_Css/guide.png'/>
	</section>
	<!--微信分享 end  -->

	<!-- 箭头指示引导 -->
	<section class='u-arrow2'><img src="/Huodong/LightAppCN/JS_Css/btn01_arrow.png" /></section>
	<!-- 箭头指示引导 end-->
</section>
    <!--脚本加载-->
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/html5.js?1383635738"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/jquery-1.8.2.min.js?1383635738"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/jquery.easing.1.3.js?1383810748"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/txt_scroll.js?1392192018"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/yl3d.js?1392964130"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/ylMap.js?1392961386"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/1_picker.js?1386650008"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/2_picker.date.js?1383816872"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/3_picker.time.js?1383816872"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/4_legacy.js?1383637492"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/9_slidepic.js?1393912181"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/99_main.js?1403249155"></script>
    <script type="text/javascript" src="/Huodong/LightAppCN/JS_Css/wxm-core176ed4.js?1397525361"></script>
      <!--脚本加载   <script type="text/javascript" src="JS_Css/wxshare.js?1402492708"></script>
end-->
</body>
</html>