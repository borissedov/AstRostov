<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="AstRostov.ShoppingCartPage" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Корзина</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('.product-qty').change(function () {
                if ($(this).val() > 10) {
                    $(this).val(10);
                }
                if ($(this).val() < 1) {
                    $(this).val(1);
                }
            });
        });
    </script>

    <h1>Корзина</h1>

    <%--<ucc:Alert runat="server" ID="ucAlert" Text="123"></ucc:Alert>--%>
    <asp:Label runat="server" ID="lblError"></asp:Label>
    <asp:UpdateProgress runat="server" ID="UpdateProgress1">
        <ProgressTemplate>
            <div class="wait-progress">
                <img alt="Пожалуйста, ждите" title="Пожалуйста, ждите" src='<%=ResolveUrl("~/img/loading.gif") %>' />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="updCart">

        <ContentTemplate>


            <div class="row-fluid">
                <div class="span12">
                    <asp:GridView runat="server" ID="gridShoppingCartItems" CssClass="table-black" AutoGenerateColumns="False" OnRowDataBound="ShoppingCartItemRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" OnClick="RemoveItem" ItemId='<%#Eval("ProductId") %>'><i class="icon-white icon-remove"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Изображение">
                                <ItemTemplate>
                                    <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'>
                                        <img alt="" src='<%#ResolveUrl(AstImage.GetImageHttpPathMedium(((Product)Eval("Product")).MainImage.ToString())) %>' />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Наименование">
                                <ItemTemplate>
                                    <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'>
                                        <%#((Product)Eval("Product")).Name %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Цена">
                                <ItemTemplate>
                                    <%# (((Product)Eval("Product")).FinalPrice).ToString("c") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Количество">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="tbQuantity" TextMode="Number" CssClass="product-qty" Width="37" Text='<%#Eval("Count") %>' OnTextChanged="ItemQuantityChange" ItemId='<%#Eval("ProductId") %>' AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Подитог">
                                <ItemTemplate>
                                    <%# ((decimal)Eval("Subtotal")).ToString("c") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Ваша корзина пуста.
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <hr />
            <div class="row-fluid" runat="server" id="pnlActionsAndTotals">
                <div class="span8">
                    <div class="pull-left">
                        <span class="text-warning" runat="server" id="spanNotAvailable">К сожалению, количество продукта «<asp:Literal runat="server" ID="litProductNotAvailableName"></asp:Literal>» на складе ограничено. Попробуйте уменьшить его количество или оформите
                            <asp:HyperLink runat="server" ID="hlPreorder" Text="предзаказ"></asp:HyperLink>. <br /><br />
                        </span>
                        
                        <asp:Button runat="server" ID="btnClearCart" CssClass="btn btn-inverse" Text="Очистить корзину" OnClick="ClearCart" />
                    </div>
                </div>
                <div class="span4">
                    <div class="pull-right">
                        <dl class="dl-horizontal">
                            <dt>Сумма без скидки:</dt>
                            <dd>
                                <asp:Literal runat="server" ID="litRetailSum"></asp:Literal>
                            </dd>
                            <dt>Скидка:</dt>
                            <dd>
                                <asp:Literal runat="server" ID="litDiscountSum"></asp:Literal>
                            </dd>
                            <dt>Итого:</dt>
                            <dd class="price-new">
                                <asp:Literal runat="server" ID="litTotal"></asp:Literal>
                            </dd>
                        </dl>
                        <asp:LinkButton runat="server" ID="btnChechout" CssClass="pull-right btn btn-large btn-primary" OnClick="Checkout">Оформить заказ&nbsp;<i class="icon-white icon-ok"></i></asp:LinkButton>
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
            $('.product-qty').change(function () {
                if ($(this).val() > 10) {
                    $(this).val(10);
                }
                if ($(this).val() < 1) {
                    $(this).val(1);
                }
            });
        }

    </script>
</asp:Content>
