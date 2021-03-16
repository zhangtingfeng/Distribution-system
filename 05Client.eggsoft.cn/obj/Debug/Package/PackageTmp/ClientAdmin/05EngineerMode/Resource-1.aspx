<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="Resource-1.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._05EngineerMode.Resource_1" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="Control/WUC_btnIntroduction.ascx" tagname="WUC_btnIntroduction" tagprefix="WUC_2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Resource-1</title>
<script src="../../Scripts/jquery-1.8.3.js?version=js201709121928" type="text/javascript"></script> 
<script src="Resource-0.js?version=js201709121928" type="text/javascript"></script>
<link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" /> 
<link href="../skin/Resource-0.css?version=css201709121928" rel="stylesheet" type="text/css" />
 

 <script type="text/javascript"> 
	   function deleteThis(intResource) {
		   var fullStr1 = "ResourceSavePost.aspx";

		   jsPost(fullStr1, {
			   'type': 'Delete',
			   'ResourceID': intResource,
			   'ReturnURL': window.location.href
		   });
	   }



	 function saveThis(intResource) {
		 var varMarker = "#NewmyTextBox" + intResource+"";
		 var varContent = htmlEncode($(varMarker).val());
		 var fullStr="ResourceSave.aspx?type=Modify&ResourceID="+intResource + "&TextContent=" + varContent;
		 //$.get(fullStr);




		 var fullStr1 = "ResourceSavePost.aspx";

		 jsPost(fullStr1, {
			 'TextContent': varContent,
			 'type': 'Modify',
			 'ResourceID': intResource,
			 'ReturnURL': window.location.href
		 });

//                 postwith(fullStr1, varContent);

//                 $.post(fullStr, function (data) {
//                     if (data == "1")
//                        alert("保存成功！");
//                 });

		 //return false; 
		// window.location.href = window.location.href;
	 }       
</script> 


</head>
<body> 
<form id="form1" runat="server"> 
<div class="ajax_tab_menu">
<div class="tab_menu_tit"><WUC_2:WUC_btnIntroduction ID="WUC_btnIntroduction1" runat="server" /></div>
		   
<asp:Table ID="Table_Show" runat="server">
						<asp:TableRow ID="TableRow1" runat="server">
							<asp:TableCell ID="TableCell1" CssClass="Css400" runat="server" HorizontalAlign="Center" VerticalAlign="Middle">
								<asp:TextBox ID="TextBox_ADD" CssClass="picCss100Percent" runat="server" Height="200px" TextMode="MultiLine" ToolTip="这里添加文本消息！"></asp:TextBox></asp:TableCell>
							<asp:TableCell ID="TableCell2" runat="server" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="mymiddle">
								<asp:ImageButton ID="ImageButton_ADD" runat="server" OnClick="ImageButton_ADD_Click" ImageUrl="../skin/Images/save.png" ToolTip="这里保存文本消息！"/>
							   </asp:TableCell>
						</asp:TableRow>
				   </asp:Table>
			  <div class="listPage">
				<div length="0" class="prolistpager_3">
				 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
						CurrentPageButtonClass="page-num-Current" EnableUrlRewriting="true" 
						MoreButtonsClass="page-num" NumericButtonCount="6" 
						NumericButtonTextFormatString="{0}" OnPageChanged="AspNetPager1_PageChanged" 
						PageSize="6" PagingButtonsClass="page-num" ShowFirstLast="False" 
						ShowPageIndexBox="Never" ShowPrevNext="False" UrlRewritePattern="" 
						width="100%" AlwaysShow="True">
					</webdiyer:AspNetPager>
					</div>   
				 </div>  
</div>
</form>
</body>
</html>
