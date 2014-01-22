<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="AstRostov.Account.EditOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Просмотр деталей заказа</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>Заказ #<%=Order.OrderId %> от <%=Order.CreateDate.ToString("dd MMMM yyyy") %></h2>

    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />

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

    <hr />
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Способ оплаты</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbPaymentMethod" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Доставка</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbShippingType" Enabled="False"></asp:TextBox>
            </div>
        </div>
        
        <hr />
        
        <div class="control-group">
            <span class="control-label">ФИО заказчика</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbFullName" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Email</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbEmail" TextMode="Email" Enabled="False"></asp:TextBox>

            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Контактный телефон</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbPhone" TextMode="Phone" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Страна</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCountry" TextMode="Phone" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Регион</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbRegion" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Населенный пункт</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCity" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Адрес 1</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbAddress1" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Адрес 2</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbAddress2" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Почтовый индекс</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbZipCode" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Документ, удостоверяющий личность</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbDocumentType" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Серия и номер документа</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbDocumentNumber" Enabled="False"></asp:TextBox>
            </div>
        </div>

        <hr />
        
        <div class="control-group">
            <span class="control-label">Комментарий администратора</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbAdminComment" TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Комментарий заказчика</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCustomerComment" TextMode="MultiLine" placeholder="Введите данные об оплате (дата, номер чека и т.д.)"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:Button runat="server" ID="btnSaveComment" Text="Сохранить сведения об оплате" CssClass="btn" OnClick="SaveOrderComment" />
            </div>
        </div>
        <hr />
        <div class="control-group">
            
            <span class="control-label">Статус заказа</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbOrderState" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            
            <span class="control-label">Действия</span>
            <div class="controls">
                <asp:Button runat="server" ID="btnPrintCheck" Text="Распечатать квитанцию" CssClass="btn" OnClick="PrintCheck" />
                <asp:Button runat="server" ID="btnRepay" Text="Повторить оплату" CssClass="btn" OnClick="Repay" />
                <asp:Button runat="server" ID="btnMarkAsRecieved" Text="Отметить как полученный" CssClass="btn" OnClick="MarkAsRecieved" />
            </div>
        </div>
        <hr />
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/OrderList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
            </div>
        </div>
    </div>

</asp:Content>
