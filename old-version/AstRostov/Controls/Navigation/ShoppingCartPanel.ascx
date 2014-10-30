<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartPanel.ascx.cs" Inherits="AstRostov.Controls.Navigation.ShoppingCartPanel" %>
<div id="shopping-cart">
    <h4 style="float: left;">Корзина</h4>
    <div class="pull-right" style="margin: 10px 0;">
        <i class="icon-shopping-cart icon-white"></i>
        <a class="cart-contents" href='<%=ResolveUrl("~/ShoppingCart.aspx") %>' title="Перейти к корзине">
            <asp:Literal runat="server" ID="litCartItemsCount"></asp:Literal> - <asp:Label runat="server" CssClass="amount" ID="lblAmount"></asp:Label>
        </a>
    </div>
</div>
