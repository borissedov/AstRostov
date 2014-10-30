<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="PreorderConvert.aspx.cs" Inherits="AstRostov.Admin.PreorderConvert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>Конвертация предзаказа</h2>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1">
        <ProgressTemplate>
            <div class="wait-progress">
                <img alt="Пожалуйста, ждите" title="Пожалуйста, ждите" src='<%=ResolveUrl("~/img/loading.gif") %>' />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="updCart">

        <ContentTemplate>
            <div class="form-horizontal">
                <div class="control-group">
                    <span class="control-label">Имя пользователя</span>
                    <div class="controls">
                        <ucc:AutocompleteTextbox runat="server" ID="txtUserName" data-provide="typeahead" data-source="" />
                        <asp:CustomValidator runat="server" ID="cvUserName" ControlToValidate="txtUserName" ValidateEmptyText="True" ErrorMessage="Пользователя с таким логином не существует" OnServerValidate="ValidateUserName" ValidationGroup="Adress"></asp:CustomValidator>
                    </div>
                </div>
                <hr />

                <div class="control-group">
                    <span class="control-label">Продукт</span>
                    <div class="controls">
                        <asp:Label runat="server" ID="lblProductName" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Артикул</span>
                    <div class="controls">
                        <asp:Label runat="server" ID="lblSkuNumber" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Конфигурация</span>
                    <div class="controls">
                        <asp:Label runat="server" ID="lblSkuAttrs" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Цена</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbProductPrice" OnTextChanged="PreorderChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Количество</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbCount" Text="1" TextMode="Number" CssClass="product-qty" OnTextChanged="PreorderChanged" AutoPostBack="true" />
                    </div>
                </div>
                <hr />
                <div class="control-group">
                    <span class="control-label">Способ оплаты</span>
                    <div class="controls">
                        <asp:RadioButtonList runat="server" ID="rblPaymentMethod" AutoPostBack="True" OnSelectedIndexChanged="PreorderChanged" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Способ доставки</span>
                    <div class="controls">
                        <asp:RadioButtonList runat="server" ID="rblShippingType" AutoPostBack="True" OnSelectedIndexChanged="PreorderChanged" />
                    </div>
                </div>
                <hr />
                <div class="control-group">
                    <span class="control-label">ФИО заказчика</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbFullName"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ControlToValidate="tbFullName" ValidationGroup="Address"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Email</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbEmail" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-error" Display="Dynamic" ValidationGroup="Address" ErrorMessage="*" ControlToValidate="tbEmail"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Контактный телефон</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbPhone" TextMode="Phone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Страна</span>
                    <div class="controls">
                        <asp:DropDownList runat="server" ID="ddlCountry">
                            <Items>
                                <asp:ListItem Text="Россия" Value="Россия" Selected="True"></asp:ListItem>
                            </Items>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Регион</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbRegion"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbRegion"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Населенный пункт</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbCity"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbCity"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Адрес 1</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbAddress1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbAddress1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Адрес 2</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbAddress2"></asp:TextBox>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Почтовый индекс</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbZipCode"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbZipCode"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Документ, удостоверяющий личность</span>
                    <div class="controls">
                        <asp:DropDownList runat="server" ID="ddlDocumentType">
                            <Items>
                                <asp:ListItem Text="Паспорт" Value="Паспорт" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Водистельское удостоверение" Value="Водистельское удостоверение"></asp:ListItem>
                            </Items>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label">Серия и номер документа</span>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbDocumentNumber"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbDocumentNumber"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <hr />
            <asp:Repeater runat="server" ID="gridLineItems" OnItemDataBound="GridItemDataBount">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <td>Наименование</td>
                            <td>Конфигурация</td>
                            <td>Цена за единицу</td>
                            <td>Количество</td>
                            <td>Итог по позиции</td>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ProductName") %></td>
                        <td><%#Eval("AttributeConfig") %></td>
                        <td><%#Eval("SalePrice", "{0:c}") %></td>
                        <td><%#Eval("Count") %></td>
                        <td><%#Eval("Subtotal", "{0:c}") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="4">Итог по позициям</td>
                        <td>
                            <asp:Literal runat="server" ID="litSubtotal"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="4">Доставка (<asp:Literal runat="server" ID="litShippingName"></asp:Literal>)</td>
                        <td>
                            <asp:Literal runat="server" ID="litShippingPrice"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td colspan="4">Комиссия</td>
                        <td>
                            <asp:Literal runat="server" ID="litCommission"></asp:Literal></td>
                    </tr>
                    </tbody>
                        <footer>
                            <td colspan="4"><strong>Итого</strong></td>
                            <td><strong>
                                <asp:Literal runat="server" ID="litTotal"></asp:Literal></strong></td>
                        </footer>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="form-horizontal">
                <div class="control-group">
                    <span class="control-label"></span>
                    <div class="controls">
                        <asp:Button runat="server" ID="btnCheckout" CausesValidation="True" Text="Сформировать заказ" CssClass="btn btn-success" ValidationGroup="Adress" OnClick="CreateOrder"></asp:Button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        with (Sys.WebForms.PageRequestManager.getInstance()) {
            add_beginRequest(onBeginRequest);
            add_endRequest(onEndRequest);
        }

        function onBeginRequest(sender, args) {
            //$.blockUI();
        }

        function onEndRequest(sender, args) {
            Page_Load();

            $('#tbProductAddCount').change(function () {
                if ($(this).val() > 99) {
                    $(this).val(99);
                }
                if ($(this).val() < 1) {
                    $(this).val(1);
                }
            });
        }
    </script>
</asp:Content>
