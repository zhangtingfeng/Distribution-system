<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient.Default1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/default.css?version=css201709121928" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function uploadComplete(sender, args) {
            alert("上传成功");
        }

        function uploadError(sender, args) {
            alert("上传错误");
        }


        function CheckClientValidate() {
            var TextboxUserPassword = $('#TextboxUserPassword').val();
            var TextboxRePassword = $('#TextboxRePassword').val();

            if (TextboxUserPassword != TextboxRePassword) {
                alert("两次的密码不相同！");
                return false;
            }



            var srcUp = $("#ImageLogo").attr("src");
            if (srcUp.toString().length == 0) {
                alert("相关图片必须选择！");
                return false;
            }
            return true;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div>
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%;">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                        <br>
                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">

                            <tr class="title">
                                <th align="left" bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25"><strong style="text-align: center">&nbsp;&nbsp; 会员资料</strong></th>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td style="width: 20%;" align="right">
                                    <strong>用户名：</strong>
                                </td>
                                <td style="height: 36px;" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="TextboxUserName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    <font face="宋体"></font>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator6" runat="server" ErrorMessage="用户名不能为空!"
                                        ControlToValidate="TextboxUserName"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>密码：</strong>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">


                                    <asp:TextBox ID="TextboxUserPassword" runat="server" TextMode="Password" CssClass="l_input"></asp:TextBox>

                                    <asp:Label ID="Label_ModifyTip" runat="server" Text="不修改请置空" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>重复密码：</strong>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="TextboxRePassword" runat="server" TextMode="Password" CssClass="l_input"></asp:TextBox>



                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22"
                                    style="font-weight: 700;">Email：</td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="Textbox_Email" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    <font face="宋体">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                            ErrorMessage="Email不能为空!" ControlToValidate="Textbox_Email"></asp:RequiredFieldValidator>
                                    </font>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="Email格式不对！"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="Textbox_Email"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>联系人姓名：</strong>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:TextBox ID="Textbox_RealName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>性别：</strong>
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        <asp:RadioButtonList ID="RadioButtonList_Sex" runat="server"
                                            RepeatDirection="Horizontal" Width="193px">
                                            <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                                            <asp:ListItem Value="0">女</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>备注信息：</strong>
                                </td>

                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="Textbox_BeiZhu" runat="server" Width="436px" Height="70px"
                                        TextMode="MultiLine" CssClass="l_input"></asp:TextBox>



                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" height="22" class="style3">&nbsp;
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        &nbsp;
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="width: 100%">
                <tr>
                    <td valign="top" align="center" style="width: 100%">
                        <table class="border" style="width: 100%; height: 107px" cellspacing="2" cellpadding="0" align="center" border="0">
                            <tr class="title">
                                <th align="left" bgcolor="#ecf5ff" class="centerAuto" colspan="2" height="25"><strong>&nbsp;&nbsp; 公司资料</strong></th>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td style="width: 20%;" align="right">
                                    <strong>公司名称：</strong>
                                </td>
                                <td style="height: 36px;" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="txtINCName" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>
                                    <font face="宋体">
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator_INCName" runat="server" ErrorMessage="公司名称不能为空!"
                                            ControlToValidate="txtINCName"></asp:RequiredFieldValidator>
                                    </font>



                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>公司类型：</strong>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:DropDownList ID="DropDownList_INC" runat="server" Height="20px"
                                        Width="157px">
                                        <asp:ListItem>公司类型</asp:ListItem>
                                        <asp:ListItem>事业单位或社会团体</asp:ListItem>
                                        <asp:ListItem>个体经营</asp:ListItem>
                                        <asp:ListItem>其他</asp:ListItem>
                                    </asp:DropDownList>



                                </td>
                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>按钮图片：<br />
                                        (建议大小：130*130)</strong>
                                </td>

                            </tr>
                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22">
                                    <strong>主营行业：</strong>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <font face="宋体">
                                                <asp:DropDownList ID="DropDownList_Class1" runat="server" AutoPostBack="True"
                                                    Height="20px" OnSelectedIndexChanged="DropDownList_Class1_SelectedIndexChanged"
                                                    Width="101px">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DropDownList_Class2" runat="server" AutoPostBack="True"
                                                    Height="20px" OnSelectedIndexChanged="DropDownList_Class2_SelectedIndexChanged"
                                                    Width="101px">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DropDownList_Class3" runat="server" Height="20px"
                                                    Width="101px">
                                                </asp:DropDownList>
                                            </font>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22"
                                    style="font-weight: 700;">
                                    <strong>公司电话：</strong>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="TextboxINCPhone" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>



                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="right" height="22"
                                    style="font-weight: 700;">
                                    <strong>公司地址：</strong>
                                </td>
                                <td bgcolor="#ecf5ff" height="22">
                                    <asp:TextBox ID="TextboxAddress" runat="server" Width="376px" CssClass="l_input"></asp:TextBox>



                                </td>
                            </tr>

                            <tr class="tdbg" bgcolor="#e3e3e3">
                                <td align="center" height="22" class="style3">&nbsp;
                                </td>
                                <td align="center" bgcolor="#ecf5ff" height="22">
                                    <div align="left">
                                        &nbsp;
											
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError" />
        </div>
    </form>

    <%--<form id="Form1" method="post" runat="server">
          
			<FONT face="宋体">
				<table cellSpacing="0" cellPadding="0" border="0" align="center" style="width:100%;">
					<tr>
						<td vAlign="top" align="center" style="width:100%"><br>
							<br>
							<br>
							<table class="border" style="WIDTH: 100%; HEIGHT: 107px" cellSpacing="2" cellPadding="0" align="center" border="0">
							
								<tr class="title" >
									<td align="left"  bgColor="#ecf5ff" class="centerAuto" colSpan="2" height="25"><strong style="text-align: center">&nbsp;&nbsp; 会员资料</strong></td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td  style="width:20%;">
										<strong>用户名：</strong>
									</td>
									<td style="height : 36px;" bgColor="#ecf5ff">
										<asp:textbox id="TextboxUserName" runat="server" Width="376px"></asp:textbox>
			<FONT face="宋体"></FONT><asp:RequiredFieldValidator 
                                            id="RequiredFieldValidator6" runat="server" ErrorMessage="用户名不能为空!" 
                                            ControlToValidate="TextboxUserName"></asp:RequiredFieldValidator></td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22">
                                        <strong>密码：</strong>
										</td>
									<td bgColor="#ecf5ff" height="22">
  
                                    
                                    <asp:textbox id="TextboxUserPassword" runat="server" TextMode="Password"></asp:textbox>
                                   
            		    <asp:Label ID="Label_ModifyTip" runat="server" Text="不修改请置空" Visible="False"></asp:Label>
			</td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22">
                                        <strong>重复密码：</strong>
										</td>
									<td bgColor="#ecf5ff" height="22">
			<asp:textbox id="TextboxRePassword" runat="server" TextMode="Password" ></asp:textbox>

             
        
									</td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22" 
                                        style="font-weight: 700;">
										Email：</td>
									<td bgColor="#ecf5ff" height="22">
                                    <asp:textbox id="Textbox_Email" runat="server" Width="376px"></asp:textbox>
			<FONT face="宋体"><asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" 
                                            ErrorMessage="Email不能为空!" ControlToValidate="Textbox_Email"></asp:RequiredFieldValidator>
			</FONT>
					    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ErrorMessage="Email格式不对！" 
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                            ControlToValidate="Textbox_Email"></asp:RegularExpressionValidator>
									</td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="center"  height="22">
										<strong>联系人姓名：</strong>
									</td>
									<td align="center" bgColor="#ecf5ff" height="22">
										<div align="left">
											<asp:textbox id="Textbox_RealName" runat="server" Width="376px"></asp:textbox>
			                            </div>
									</td>
								</tr>
								
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="center"  height="22">
										<strong>性别：</strong>
									</td>
									<td align="center" bgColor="#ecf5ff" height="22">
										<div align="left">
											<asp:RadioButtonList ID="RadioButtonList_Sex" runat="server" 
                                                RepeatDirection="Horizontal" Width="193px">
                                                <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                                                <asp:ListItem Value="0">女</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
									</td>
								</tr>
								
                                <tr class="tdbg" bgColor="#e3e3e3">
                                    <td align="center"  height="22">
										<strong>备注信息：</strong>
									</td>
									
									<td bgColor="#ecf5ff" height="22">
                                    <asp:textbox id="Textbox_BeiZhu" runat="server" Width="436px" Height="70px" 
                                            TextMode="MultiLine"></asp:textbox>

             
        
									</td>
								</tr>

								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="center"  height="22" class="style3">&nbsp;
									</td>
									<td align="center" bgColor="#ecf5ff" height="22">
										<div align="left">&nbsp;
											</div>
									</td>
								</tr>
							</table>
							</td>
					</tr>
				</table>

				<table cellSpacing="0" cellPadding="0" border="0" align="center" style="width:100%">
					<tr>
						<td vAlign="top" align="center" style="width:100%">							
							<table class="border" style="WIDTH: 100%; HEIGHT: 107px" cellSpacing="2" cellPadding="0" align="center" border="0">
								<tr class="title" >
									<td align="left" bgColor="#ecf5ff" class="centerAuto" colSpan="2" height="25"><strong>&nbsp;&nbsp; 公司资料</strong></td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td  style="width:20%;">
										<strong>公司名称：</strong>
									</td>
									<td style="height : 36px;" bgColor="#ecf5ff">
										<asp:textbox id="txtINCName" runat="server" Width="376px"></asp:textbox>
			<FONT face="宋体">
				                        <asp:RequiredFieldValidator 
                                            id="RequiredFieldValidator_INCName" runat="server" ErrorMessage="公司名称不能为空!" 
                                            ControlToValidate="txtINCName"></asp:RequiredFieldValidator>
			</FONT>

             
        
		                            </td>
								</tr>
                                <tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22">
                                        <strong>公司类型：</strong>
										</td>
									<td bgColor="#ecf5ff" height="22">
                                        <asp:DropDownList ID="DropDownList_INC" runat="server" Height="20px" 
                                            Width="157px">
                                            <asp:ListItem>公司类型</asp:ListItem>
                                            <asp:ListItem>事业单位或社会团体</asp:ListItem>
                                            <asp:ListItem>个体经营</asp:ListItem>
                                            <asp:ListItem>其他</asp:ListItem>
                                        </asp:DropDownList>

             
        
									</td>
								</tr>
                                <tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22">
                                        <strong>按钮图片：<br />
                                        (建议大小：130*130)</strong>
										</td>
									<td bgColor="#ecf5ff" height="22">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>                                              
		                    
		                                <asp:AsyncFileUpload ID="FileUploadLogo" runat="server" 
                                            onclientuploadcomplete="uploadComplete()" onclientuploaderror="uploadError()" 
                                            onuploadedcomplete="FileUploadLogo_UploadedComplete" />
		                    
		                               <asp:Image ID="ImageUploadLogo" runat="server" Height="50px" Width="50px" />

            
                                        <asp:Table ID="clientSide" runat="server" Visible="False">
                                        </asp:Table>
                                      
        
									</td>
								</tr>
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22">
                                           <strong>主营行业：</strong>
										</td>
									<td bgColor="#ecf5ff" height="22">
			                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <font face="宋体">
                                                    <asp:DropDownList ID="DropDownList_Class1" runat="server" AutoPostBack="True" 
                                                        Height="20px" onselectedindexchanged="DropDownList_Class1_SelectedIndexChanged" 
                                                        Width="101px">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownList_Class2" runat="server" AutoPostBack="True" 
                                                        Height="20px" onselectedindexchanged="DropDownList_Class2_SelectedIndexChanged" 
                                                        Width="101px">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DropDownList_Class3" runat="server" Height="20px" 
                                                        Width="101px">
                                                    </asp:DropDownList>
                                                </font>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
									</td>
								</tr>

                                <tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22" 
                                        style="font-weight: 700;">
                                        <strong>公司电话：</strong>
										</td>
									<td bgColor="#ecf5ff" height="22">
                                    <asp:textbox id="TextboxINCPhone" runat="server" Width="376px"></asp:textbox>

             
        
									</td>
								</tr>

								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="right"  height="22" 
                                        style="font-weight: 700;">
                                        <strong>公司地址</strong>
									    </td>
									<td bgColor="#ecf5ff" height="22">
                                    <asp:textbox id="TextboxAddress" runat="server" Width="376px"></asp:textbox>

             
        
									</td>
								</tr>
								
								<tr class="tdbg" bgColor="#e3e3e3">
									<td align="center"  height="22" class="style3">&nbsp;
									</td>
									<td align="center" bgColor="#ecf5ff" height="22">
										<div align="left">&nbsp;
											</div>
									</td>
								</tr>
							</table>
							</td>
					</tr>
				</table>
			</FONT>

             
        
		    <p style="text-align: center">
			<FONT face="宋体" >
				<asp:button id="btnAdd" runat="server" Text=" 保  存 " Width="72px" 
                    OnClick="btnAdd_Click" onclientclick="return CheckClientValidate();"></asp:button>
			</FONT>

             
        
		    </p>

             
        
		</form>--%>
</body>
</html>
