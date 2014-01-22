<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="PaymentFailed.aspx.cs" Inherits="AstRostov.PaymentFailed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Оплата не прошла</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Попытка оплаты завершилась неудачей</h2>
    <p>Платежная система отклонила Ваш запрос на оплату. Вы можете повторить попытку оплаты со страницы <a href='<%=ResolveUrl("~/Account/OrderList.aspx") %>' title="Перейти">списка ваших заказов</a></p>
</asp:Content>
