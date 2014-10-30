<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="AstRostov.PaymentSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" ID="litPaymrntSuccess"></asp:Literal>
    </h2>

    <p>
        Вам отправлено письмо с информацией об опаченном заказе. 
        В ближайшее время ваш заказ будет направлен к пункту доставки.
    </p>

    <asp:Label runat="server" ID="lblError" CssClass="text-error"></asp:Label>

    <div class="row-fluid">
        <div class="span12">
            <asp:Repeater runat="server" ID="gridLineItems" OnItemDataBound="GridItemDataBount">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <td>Наименование</td>
                            <td>Цена за единицу</td>
                            <td>Количество</td>
                            <td>Итог по позиции</td>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ProductName") %></td>
                        <td><%#Eval("SalePrice", "{0:c}") %></td>
                        <td><%#Eval("Count") %></td>
                        <td><%#Eval("Subtotal", "{0:c}") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="3">Итог по позициям</td>
                        <td>
                            <asp:Literal runat="server" ID="litSubtotal"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="3">Доставка (<asp:Literal runat="server" ID="litShippingName"></asp:Literal>)</td>
                        <td>
                            <asp:Literal runat="server" ID="litShippingPrice"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="3">Комиссия</td>
                        <td>
                            <asp:Literal runat="server" ID="litCommission"></asp:Literal></td>
                    </tr>
                    </tbody>
                        <footer>
                            <td colspan="3"><strong>Итого</strong></td>
                            <td><strong>
                                <asp:Literal runat="server" ID="litTotal"></asp:Literal></strong></td>
                        </footer>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="pull-left">
                <asp:HyperLink runat="server" CssClass="btn btn-inverse" Text="На главную" NavigateUrl="~/Default.aspx"></asp:HyperLink>
                <asp:HyperLink runat="server" CssClass="btn btn-inverse" Text="Мои заказы" NavigateUrl="~/Account/OrderList.aspx"></asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
