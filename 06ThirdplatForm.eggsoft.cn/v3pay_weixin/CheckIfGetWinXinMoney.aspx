<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckIfGetWinXinMoney.aspx.cs" Inherits="_06ThirdplatForm.eggsoft.cn.v3pay_weixin.CheckIfGetWinXinMoney" %>


 <script type="text/javascript">


     t = setTimeout("timedCount()", 2000);//等待2秒重新调用
     function timedCount() {
         self.location = '/v3pay_weixin/CheckIfGetWinXinMoney.aspx?OrderNum=<%=strPubOrderNum%>';
                              
         timedCount();
//         alert("2000");
//         Clickpay();
         //                      var lnk = document.getElementById("getBrandWCPayRequest");
         //                      alert(lnk);
         //                     
         //                          lnk.click();
     }
 </script>