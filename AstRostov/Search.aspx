<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AstRostov.Search" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Результаты поиска</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="pull-left">
                <h1>Результаты поиска
                </h1>
            </div>
            <div class="pull-right" style="margin-top: 20px;">
                <asp:DropDownList runat="server" ID="ddlBrands" OnSelectedIndexChanged="FilterBrand" AutoPostBack="True" />
            </div>
        </div>
    </div>
    <div class="shop-items">
        <asp:Repeater runat="server" ID="rptProductListRows" OnItemDataBound="ProductRowDataBound">
            <ItemTemplate>
                <div class="row-fluid">
                    <asp:Repeater runat="server" ID="rptProductListItems" OnItemCommand="ProcessProductCommand">
                        <ItemTemplate>
                            <div class="span4">
                                <div class="product-item">
                                    <!-- Use the below link to put HOT icon -->
                                    <div class="item-icon badge badge-ast" runat="server" visible='<%#Eval("Brand") != null %>'>
                                        <span><%#Eval("Brand") ?? "" %></span>
                                    </div>
                                    <i id="I1" runat="server" visible='<%#(bool)Eval("IsFeatured") %>' class="icon-white icon-star item-icon-right" style="top: 10px;"></i>
                                    <!-- Item image -->
                                    <div class="item-image">
                                        <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'>
                                            <img src='<%#ResolveUrl(AstImage.GetImageHttpPathMedium(Eval("MainImage").ToString())) %>' alt="" class="img-responsive thumbnail">
                                        </a>
                                        &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <!-- Item details -->
                                    <div class="item-details">
                                        <!-- Name -->
                                        <h5>
                                            <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'><%#Eval("Name") %></a>
                                        </h5>
                                        <div class="clearfix"></div>
                                        <!-- Para. Note more than 2 lines. -->
                                        <p><%#Eval("ShortDescription") %></p>
                                        <hr>
                                        <!-- Price -->
                                        <div class="price pull-left">
                                            <%#((Product)Container.DataItem).FormattedPrice() %>
                                        </div>
                                        <!-- Add to cart -->
                                        <div class="pull-right">
                                            <asp:LinkButton runat="server" ID="lbAddToCart" CssClass="btn btn-primary btn-small" CommandName="AddToCart" CommandArgument='<%#Eval("ProductId") %>' Visible='<%#(int)Eval("TotalInventory") > 0 %>'><i class="icon-white icon-shopping-cart"></i>&nbsp;В корзину</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbReserve" CssClass="btn btn-warning btn-small" CommandName="Reserve" CommandArgument='<%#Eval("ProductId") %>' Visible='<%#(int)Eval("TotalInventory") == 0 %>'><i class="icon-white icon-time"></i>&nbsp;Предзаказ</asp:LinkButton>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

    <div class="pagination pagination-centered pagination-inverse">
        <ul>
            <li class='<% = 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/Search.aspx?keywords={1}&page={0}&brand={2}", CurrentPageNo - 1, Server.UrlEncode(String.Join("|", KeyWords)), ddlBrands.SelectedValue)) %>'>« Предыдущая
                </a>
            </li>

            <asp:Repeater runat="server" ID="rptPaging">
                <ItemTemplate>
                    <li class='<%#(int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                        <a class="btn-inverse" href='<%#(int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/Search.aspx?keywords={1}&page={0}&brand={2}", Eval("PageNo"), Server.UrlEncode(String.Join("|", KeyWords)), ddlBrands.SelectedValue)) %>'>
                            <%#Eval("PageNo") %>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            <li class='<% = PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/Search.aspx?keywords={1}&page={0}&brand={2}", CurrentPageNo + 1,Server.UrlEncode(String.Join("|", KeyWords)), ddlBrands.SelectedValue)) %>'>Следующая »
                </a>
            </li>
        </ul>
    </div>
</asp:Content>
