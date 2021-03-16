<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUC_DateTime.ascx.cs" Inherits="_12upload.eggsoft.cn.Control.WebUC_DateTime" %>

<asp:Panel ID="Panel1" runat="server" Width="235px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <asp:TextBox ID="TextBox_selectTime" runat="server"></asp:TextBox>

            &nbsp;<asp:Button ID="Button_selectTime" runat="server" Text="选择时间"
                OnClick="Button_selectTime_Click" />

            <asp:Table ID="Table_Show" runat="server" Width="816px" Visible="False"
                HorizontalAlign="Left">
                <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell1" runat="server" VerticalAlign="Top">
                        选择日期：<br />
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell2" runat="server" VerticalAlign="Top">
                        选择小时：<br />
                        <asp:DropDownList ID="DropDownList_Hour" runat="server" Height="23px"
                            Width="213px">
                        </asp:DropDownList>

                    </asp:TableCell>
                    <asp:TableCell ID="TableCell3" runat="server" VerticalAlign="Top">
                        选择分钟：<br />
                        <asp:DropDownList ID="DropDownList_Min" runat="server" Height="23px" Width="213px">
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>




        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Panel>
