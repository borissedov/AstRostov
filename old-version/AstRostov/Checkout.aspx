<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="AstRostov.CheckoutPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Оплата заказа</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        with (Sys.WebForms.PageRequestManager.getInstance()) {
            add_beginRequest(onBeginRequest);
            add_endRequest(onEndRequest);
        }

        function onBeginRequest(sender, args) {
            //$.blockUI();
        }

        function onEndRequest(sender, args) {
            Cufon.replace("h4");
        }
    </script>
    <h2>Оплата заказа</h2>
    <asp:Label runat="server" ID="lblError" CssClass="text-error"></asp:Label>

    <div class="row-fluid">
        <div class="span12">
            <asp:Repeater runat="server" ID="gridLineItems">
                <HeaderTemplate>
                    <table class="table-black">
                        <tbody>
                            <tr>
                                <th>Наименование</th>
                                <th>Цена за единицу</th>
                                <th>Количество</th>
                                <th>Итог по позиции</th>
                            </tr>

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
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <br />
    <div class="row-fluid">
        <div class="span12">
            <div class="pull-right">
                <dl class="dl-horizontal">
                    <dt>Итог по позициям:</dt>
                    <dd class="price-new">
                        <asp:Literal runat="server" ID="litSubtotal"></asp:Literal>
                    </dd>
                    <dt>Скидка:</dt>
                    <dd>
                        <asp:Literal runat="server" ID="litDiscountSum"></asp:Literal>
                    </dd>
                </dl>
            </div>
        </div>
    </div>
    <hr />
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="updTotals">
                <ProgressTemplate>
                    <div class="wait-progress">
                        <img alt="Пожалуйста, ждите" title="Пожалуйста, ждите" src='<%=ResolveUrl("~/img/loading.gif") %>' />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" UpdateMode="Always" ID="updTotals">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span6">
                    <div class="form-horizontal">
                        <div class="control-group">
                            <h4 class="control-label">Способ доставки</h4>
                            <div class="controls">
                                <asp:RadioButtonList runat="server" ID="rblShippingMethod" OnSelectedIndexChanged="BindTotals" AutoPostBack="True" CssClass="radio" RepeatLayout="Flow" RepeatDirection="Horizontal" RepeatColumns="1"></asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span3">
                    <p runat="server" id="pShippingTK">
                        Внимание! В сумму заказа включется стоимость доставки заказа до терминала отправки. Услуги транспортировки груза до пункта назначения оплачиваются отдельно при получении.
                    </p>
                    <p runat="server" id="pShippingPickUp">
                        Внимание! Заказ вы сможете забрать на нашем складе, располагающемся по адресу, указанному на <a href='<%=ResolveUrl("~/Contact.aspx") %>'>странице контактов</a>.
                    </p>
                </div>
                <div class="span3">
                    <div class="pull-right">
                        <dl class="dl-horizontal">
                            <dt>Доставка:</dt>
                            <dd>
                                <asp:Literal runat="server" ID="litShipping"></asp:Literal>
                            </dd>

                        </dl>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row-fluid">
                <div class="span6">
                    <div class="form-horizontal">
                        <div class="control-group">
                            <h4 class="control-label">Способ оплаты</h4>
                            <div class="controls">
                                <asp:RadioButtonList runat="server" ID="rblPaymentMethod" OnSelectedIndexChanged="BindTotals" AutoPostBack="True" CssClass="radio" RepeatLayout="Flow" RepeatDirection="Horizontal" RepeatColumns="1"></asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="pull-right">
                        <dl class="dl-horizontal">
                            <dt>Комиссия:</dt>
                            <dd>
                                <asp:Literal runat="server" ID="litCommission"></asp:Literal>
                            </dd>
                        </dl>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row-fluid">
                <div class="span12">
                    <div class="pull-right">
                        <dl class="dl-horizontal">
                            <dt><strong>Итого:</strong></dt>
                            <dd class="price-new-checkout">
                                <asp:Literal runat="server" ID="litTotal"></asp:Literal>
                            </dd>
                        </dl>
                        <asp:Button runat="server" ID="btnCheckout" CssClass="btn btn-large btn-primary pull-right" OnClick="ProcessChechout" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
