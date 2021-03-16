<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02QueryList.aspx.cs" Inherits="_01WA_WebDestop._01LogisticalPrice._02QueryList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <table width="90%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tbody>
            <tr>
                <td align="left" valign="top">




                    <div class="kindc">报价查询结果 &gt;&gt;<asp:Literal ID="Literal2SendInfo" runat="server"></asp:Literal> </div>
                    <br>
                    <table border="0" cellspacing="0" class="priceListTable">
                        <tbody>
                            <tr>
                                <td align="left"><strong>报价查询结果: </strong></td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" height="80" border="1" align="center" cellpadding="1" cellspacing="0" bordercolorlight="#CCCCCC" bordercolordark="#FFFFFF" bgcolor="#FFFFFF">
                                        <tbody>
                                            
                                            <tr>
                                                <td width="130" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_0"><strong>网络类别</strong></td>
                                                <td width="80" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_1"><strong>标准价</strong></td>
                                                <td width="60" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_2"><strong>折扣</strong></td>
                                                <td width="60" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_3"><strong>燃油费</strong></td>
                                                <td width="80" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_4"><strong>总费用</strong></td>
                                                <td width="60" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_5"><strong>时效</strong></td>
                                                <td width="50" align="center" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_6"><strong>按体积</strong></td>
                                                <td align="left" bgcolor="#E4E4E4" class="priceListTitle" id="oHeader_7"><strong>备注</strong></td>
                                            </tr>
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                    <br>
                </td>
            </tr>

        </tbody>
    </table>
</body>
</html>
