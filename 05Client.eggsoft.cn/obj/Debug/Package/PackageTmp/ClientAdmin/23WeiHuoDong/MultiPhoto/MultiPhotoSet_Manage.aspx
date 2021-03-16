<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiPhotoSet_Manage.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._23WeiHuoDong.MultiPhoto.MultiPhotoSet_Manage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>相册</title>
	           
	 <link href="../../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />
 
	    <style type="text/css">
            .style1
            {
                color: #CC0000;
            }
            .style_Percent
            {
                width: 20%;
            }
            .style5
            {
                color: #FF3300;
            }
            .style6
            {
                width: 80%;
            }
        </style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<table height="100%" cellSpacing="0" cellPadding="0" border="0" align="center" style="width:100%">
					<tr>
						<td vAlign="top" align="center" style="width:100%"><br>
							<!--创建一个JS调用button的click事件-->

<script type="text/javascript">
    function JsListChangeItem() {

        document.getElementById("Button_Choice_Click").click();
    }
</script>
<!--创建一个隐藏的button，创建双击事件--->
							<br>
							<br>
							<table class="border" style="WIDTH: 100%; HEIGHT: 107px" cellSpacing="2" cellPadding="0" align="center" border="0">
								<tr class="title" bgColor="#e3e3e3">
									<td align="center" colSpan="2" height="25" style=" text-align:center"><strong>相 册 管 理(本菜单地址:<span class="style5"><%=MenuLink%></span>)</strong></td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td bgColor="#e3e3e3" class="style_Percent">
										<div align="right"><strong>相册名称：</strong></div>
									</td>
									<td style="height : 36px; width:600px;" bgColor="#ecf5ff">
										<asp:textbox id="txtTitle" runat="server" Width="376px"></asp:textbox>
			                        </td>
								</tr>
								

                                <tr id="Tr1" class="tdbg" bgColor="#c0c0c0" runat="server">
									<td align="right" bgColor="#e3e3e3" height="22" class="style_Percent">
										<strong>图片管理：</strong></td>
									<td bgColor="#ecf5ff" height="22">
                                        <table class="style6">
                                            <tr>
                                                <td>
                                                    待选区</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    已选区</td>
                                                <td align="center">
                                                    图片名称标注</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="ListBox_WaitChoice" runat="server" Height="400px" 
                                                        Width="300px" 
                                                        onselectedindexchanged="ListBox_WaitChoice_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:ListBox>
                                                </td>
                                                <td>
			                                        <asp:Button ID="Button_Choice" runat="server" Text="选择-&gt;" 
                                                        onclick="Button_Choice_Click" />
		                                        </td>
                                                <td>
				                                    <asp:ListBox ID="ListBox_OK" runat="server" Height="360px" Width="300px" 
                                                        AutoPostBack="True" onselectedindexchanged="ListBox_OK_SelectedIndexChanged">
                                                    </asp:ListBox>
                                                    <asp:Button ID="MoveUp" runat="server" Text="向上移动" onclick="MoveUp_Click" />
                                                    <asp:Button ID="MoveDown" runat="server" Text="向下移动" onclick="MoveDown_Click" />

			<FONT face="宋体">
                                                    <asp:Button ID="MoveOut" runat="server" Text="移除" 
                                                        onclick="MoveOut_Click" />

			</FONT>

		                                       </td>
                                                <td align="center" style=" vertical-align:top;">
			                                        <asp:TextBox ID="TextBoxPicName" runat="server"></asp:TextBox>
		                                            <br />
                                                    <asp:Button ID="Button_SaveTitle" runat="server" Text="保存" 
                                                        onclick="Button_SaveTitle_Click" />
		                                            <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Image ID="ImageShow" runat="server" Width="100px" />
		                                            <br />
                                                    <asp:Literal ID="Literal_Image_Info" runat="server"></asp:Literal>
                                                    <br />
		                                            
		                                        </td>
                                            </tr>
                                        </table>
                                    </td>
								</tr>

                                <tr id="LinkShow" class="tdbg" bgColor="#c0c0c0" runat="server">
									<td align="right" bgColor="#e3e3e3" height="22" class="style_Percent">
										<strong>相册链接：</strong></td>
									<td bgColor="#ecf5ff" height="22">
                                        <asp:Label ID="Label1Link" runat="server" Text="Label1Link"></asp:Label></td>
								</tr>

								<tr class="tdbg"  style="display:none;" bgColor="#c0c0c0">
									<td align="center" bgColor="#e3e3e3" height="22" class="style_Percent">
										<div align="right"><strong>发布者：</strong></div>
									</td>
									<td align="center" bgColor="#ecf5ff" height="22">
										<div align="left">
											<asp:textbox id="txtWriter" runat="server" Width="376px">Admin</asp:textbox>
			<FONT face="宋体">
											<span class="style1"><strong>*</strong></span></FONT><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="发布者不能为空!" ControlToValidate="txtWriter"></asp:RequiredFieldValidator></div>
									</td>
								</tr>
								
								<tr class="tdbg" bgColor="#c0c0c0">
									<td align="center" bgColor="#e3e3e3" height="22" class="style3">&nbsp;
									</td>
									<td align="center" bgColor="#e3e3e3" height="22">
										<div align="left">&nbsp;
											<asp:button id="btnAdd" runat="server" Text=" 添  加 " Width="72px" OnClick="btnAdd_Click"></asp:button></div>
									</td>
								</tr>
							</table>
							<BR>
							<BR>
							<BR>
                            &nbsp;</td>
					</tr>
				</table>
			</FONT>
		</form>    
	</body>
</HTML>
