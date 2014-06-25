<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AstRostov.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Административная панель. Статистика.
    </h2>
    <table class="table table-bordered">
        <thead>
            <tr>
                <td>Параметр</td>
                <td>Сегодня</td>
                <td>Последняя неделя</td>
                <td>Всего</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Зарегистрированно пользователей</td>
                <td><asp:Literal runat="server" ID="litUsersRegisteredToday"/></td>
                <td><asp:Literal runat="server" ID="litUsersRegisteredWeek"/></td>
                <td><asp:Literal runat="server" ID="litUsersRegisteredTotal"/></td>
            </tr>
            <tr>
                <td>Создано заказов</td>
                <td><asp:Literal runat="server" ID="litOrderCountToday"/></td>
                <td><asp:Literal runat="server" ID="litOrderCountWeek"/></td>
                <td><asp:Literal runat="server" ID="litOrderCountTotal"/></td>
            </tr>
            <tr>
                <td>Сумма заказов</td>
                <td><asp:Literal runat="server" ID="litOrderSumToday"/></td>
                <td><asp:Literal runat="server" ID="litOrderSumWeek"/></td>
                <td><asp:Literal runat="server" ID="litOrderSumTotal"/></td>
            </tr>
            <tr>
                <td>Товаров на складе</td>
                <td></td>
                <td></td>
                <td><asp:Literal runat="server" ID="litSkuCount"></asp:Literal></td>
            </tr>
            <tr>
                <td>Записей в бортжурналах</td>
                <td><asp:Literal runat="server" ID="litPostCountToday"/></td>
                <td><asp:Literal runat="server" ID="litPostCountWeek"/></td>
                <td><asp:Literal runat="server" ID="litPostCountTotal"/></td>
            </tr>
            <%--<tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>--%>
        </tbody>
    </table>
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Пользователей онлайн</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbUsersOnline" Enabled="False"></asp:TextBox>
            </div>
        </div>
    </div>
    <%=DateTime.Now.ToString("F") %>
</asp:Content>
