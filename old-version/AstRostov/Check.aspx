<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="AstRostov.Check" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Квитанция СБ РФ (ПД-4)</title>
</head>
<body>
    <style>
        body {
            background-color: white;
        }

        .text14 li {
            list-style-type: none;
            padding-bottom: 5px;
            padding: 6px 0px 0px 5px;
        }

        .text14 {
            font-family: "Times New Roman", Times, serif;
            font-size: 14px;
        }

            .text14 strong {
                font-family: "Times New Roman", Times, serif;
                font-size: 11px;
            }
    </style>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hdnItemId" />
        <div class="text14">
            <table width="720" bordercolor="#000000" style="border: #000000 1px solid;" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="220" valign="top" height="250" align="center" style="border-bottom: #000000 1px solid; border-right: #000000 1px solid;">&nbsp;<strong>Извещение</strong></td>
                    <td valign="top" style="border-bottom: #000000 1px solid; border-right: #000000 1px solid;">
                        <li><strong>Получатель: </strong><font style="font-size: 90%">ИП Алехина Вита Александровна</font>&nbsp;&nbsp;&nbsp;<br />
                            <li><strong>КПП:</strong> __________&nbsp;&nbsp;&nbsp;&nbsp; <strong>ИНН:</strong> 616700770626&nbsp;&nbsp;<font style="font-size: 12px"> &nbsp;</font>
                                &nbsp;    
                            <li><strong>Код ОКАТО:</strong>___________&nbsp;&nbsp;&nbsp;&nbsp;<strong>P/сч.:</strong> 40802810800020000768<li><strong>в:</strong> Ростовский филиал ОАО «ФОНДСЕРВИСБАНК»<br />
                                <li><strong>БИК:</strong> 046027998&nbsp; <strong>К/сч.: 30101810100000000998</strong><br />
                                    <li><strong>Код бюджетной классификации (КБК):</strong> ____________________ 
                                              <li><strong>Платеж:</strong> <font style="font-size: 90%"><asp:Literal runat="server" ID="OrderName2"></asp:Literal></font>
                                                  <br />
                                                  <li><strong>Плательщик:</strong>  _________________________________________________<br />
                                                      <li><strong>Адрес плательщика:</strong> <font style="font-size: 90%">____________________________________________</font>
                                                          <br />
                                                          <li><strong>ИНН плательщика:</strong> ____________&nbsp;&nbsp;&nbsp;&nbsp; <strong>№ л/сч. плательщика:</strong> ______________      
                                                              <li><strong>Сумма:</strong> <strong><asp:Literal runat="server" ID="SumRub2"></asp:Literal></strong> руб. <strong><asp:Literal runat="server" ID="SumKop2"/></strong> коп. &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                                                    &nbsp;<br />
                                                                  &nbsp;<br />
                                                                  <br />
                                                                  Подпись:________________________        Дата: &quot;  &quot;&nbsp;&nbsp;&nbsp;2013 г.
                                                                  <br />
                                                                  <br />
                    </td>
                </tr>
                <tr>
                    <td width="220" valign="top" height="250" align="center" style="border-bottom: #000000 1px solid; border-right: #000000 1px solid;">&nbsp;<strong>Квитанция</strong></td>
                    <td valign="top" style="border-bottom: #000000 1px solid; border-right: #000000 1px solid;">
                        <li><strong>Получатель: </strong><font style="font-size: 90%">ИП Алехина Вита Александровна</font>&nbsp;&nbsp;<br />
                            <li><strong>КПП:</strong> __________&nbsp;&nbsp;&nbsp;&nbsp; <strong>ИНН:</strong> 616700770626&nbsp;&nbsp;<font style="font-size: 12px"> &nbsp;</font>
                                &nbsp;    
                            <li><strong>Код ОКАТО:</strong>___________&nbsp;&nbsp;&nbsp;&nbsp;<strong>P/сч.:</strong> 40802810800020000768<li><strong>в:</strong> Ростовский филиал ОАО «ФОНДСЕРВИСБАНК»<br />
                                <li><strong>БИК:</strong> 046027998&nbsp; <strong>К/сч.: 30101810100000000998</strong><br />
                                    <li><strong>Код бюджетной классификации (КБК):</strong> ____________________ 
                                              <li><strong>Платеж:</strong> <font style="font-size: 90%"><asp:Literal runat="server" ID="OrderName1"></asp:Literal></font>
                                                  <br />
                                                  <li><strong>Плательщик:</strong>  _________________________________________________<br />
                                                      <li><strong>Адрес плательщика:</strong> <font style="font-size: 90%">____________________________________________</font>
                                                          <br />
                                                          <li><strong>ИНН плательщика:</strong> ____________&nbsp;&nbsp;&nbsp;&nbsp; <strong>№ л/сч. плательщика:</strong> ______________      
                                                          <li><strong>Сумма:</strong> <strong><asp:Literal runat="server" ID="SumRub1"></asp:Literal></strong> руб. <strong><asp:Literal runat="server" ID="SumKop1"/></strong> коп. &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                                                    &nbsp;<br />
                                                                  &nbsp;<br />
                                                                  <br />
                                                                  Подпись:________________________        Дата: &quot;  &quot;&nbsp;&nbsp;&nbsp;2013 г.
                                                                  <br />
                                                                  <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
