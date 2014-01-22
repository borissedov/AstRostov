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
        <div class="text14">
            <table width="720" bordercolor="#000000" style="border: #000000 1px solid;" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="220" valign="top" height="250" align="center" style="border-bottom: #000000 1px solid; border-right: #000000 1px solid;">&nbsp;<strong>Извещение</strong></td>
                    <td valign="top" style="border-bottom: #000000 1px solid; border-right: #000000 1px solid;">
                        <li><strong>Получатель: </strong><font style="font-size: 90%">____________________________________________________</font>&nbsp;&nbsp;&nbsp;<br />
                            <li><strong>КПП:</strong> __________&nbsp;&nbsp;&nbsp;&nbsp; <strong>ИНН:</strong> ____________&nbsp;&nbsp;<font style="font-size: 12px"> &nbsp;</font>
                                &nbsp;    
                            <li><strong>Код ОКАТО:</strong>___________&nbsp;&nbsp;&nbsp;&nbsp;<strong>P/сч.:</strong> ____________________&nbsp;&nbsp;
                                   &nbsp;    
                                <li><strong>в:</strong> <font style="font-size: 90%">______________________________________________________________</font>
                                    <br />
                                    <li><strong>БИК:</strong> _________&nbsp; <strong>К/сч.:</strong>____________________<br />
                                        <li><strong>Код бюджетной классификации (КБК):</strong> ____________________ 
                                              <li><strong>Платеж:</strong> <font style="font-size: 90%">_____________________________________________________</font>
                                                  <br />
                                                  <li><strong>Плательщик:</strong>  _________________________________________________<br />
                                                      <li><strong>Адрес плательщика:</strong> <font style="font-size: 90%">____________________________________________</font>
                                                          <br />
                                                          <li><strong>ИНН плательщика:</strong> ____________&nbsp;&nbsp;&nbsp;&nbsp; <strong>№ л/сч. плательщика:</strong> ______________      
                                                              <li><strong>Сумма:</strong> <strong>______</strong> руб. <strong>__</strong> коп. &nbsp;&nbsp;&nbsp;&nbsp;
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
                        <li><strong>Получатель: </strong><font style="font-size: 90%">____________________________________________________</font>&nbsp;&nbsp;&nbsp;<br />
                            <li><strong>КПП:</strong> __________&nbsp;&nbsp;&nbsp;&nbsp; <strong>ИНН:</strong> ____________&nbsp;&nbsp;<font style="font-size: 12px"> &nbsp;</font>
                                &nbsp;    
                            <li><strong>Код ОКАТО:</strong>___________&nbsp;&nbsp;&nbsp;&nbsp;<strong>P/сч.:</strong> ____________________&nbsp;&nbsp;
                                   &nbsp;    
                                <li><strong>в:</strong> <font style="font-size: 90%">______________________________________________________________</font>
                                    <br />
                                    <li><strong>БИК:</strong> _________&nbsp; <strong>К/сч.:</strong>____________________<br />
                                        <li><strong>Код бюджетной классификации (КБК):</strong> ____________________ 
                                              <li><strong>Платеж:</strong> <font style="font-size: 90%">_____________________________________________________</font>
                                                  <br />
                                                  <li><strong>Плательщик:</strong>  _________________________________________________<br />
                                                      <li><strong>Адрес плательщика:</strong> <font style="font-size: 90%">____________________________________________</font>
                                                          <br />
                                                          <li><strong>ИНН плательщика:</strong> ____________&nbsp;&nbsp;&nbsp;&nbsp; <strong>№ л/сч. плательщика:</strong> ______________      
                                                              <li><strong>Сумма:</strong> <strong>______</strong> руб. <strong>__</strong> коп. &nbsp;&nbsp;&nbsp;&nbsp;
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
