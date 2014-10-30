<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="AstRostov.CategoryPage" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Категория
        <asp:Literal runat="server" ID="litCategoryNameHead"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <ucn:CategoryBreadCrumbs runat="server" ID="ucCategoryBreadCrumbs"></ucn:CategoryBreadCrumbs>
    <div class="row-fluid">
        <div class="span12">
            <div class="pull-left">
                <a href='<%=HttpContext.Current.Request.Url.PathAndQuery%>'>
                    <h2>
                        <asp:Literal runat="server" ID="litCategoryName"></asp:Literal>
                    </h2>
                </a>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <p>
                <asp:Literal runat="server" ID="litCategoryDescription"></asp:Literal>
            </p>
        </div>
    </div>
    <div class="shop-items">
        <asp:Repeater runat="server" ID="rptChildCategoriesRows" OnItemDataBound="ChildCategoryRowDataBound" Visible="False">
            <ItemTemplate>
                <div class="row-fluid">
                    <asp:Repeater runat="server" ID="rptChildCategoriesItems">
                        <ItemTemplate>
                            <div class="span4">
                                <div class="product-item">

                                    <!-- Item image -->
                                    <div class="item-image">
                                        <a href='<%#ResolveUrl(String.Format("~/Category.aspx?id={0}", Eval("CategoryId"))) %>'>
                                            <img src='<%#ResolveUrl(AstImage.GetImageHttpPathMedium(Eval("Image") as CategoryImage != null ? (Eval("Image") as CategoryImage).ToString() : "noimage.gif")) %>' alt="" class="img-responsive thumbnail">
                                        </a>
                                        &nbsp;&nbsp;&nbsp;
                                    </div>
                                    <!-- Item details -->
                                    <div class="item-details">
                                        <!-- Name -->
                                        <h5>
                                            <a href='<%#ResolveUrl(String.Format("~/Category.aspx?id={0}", Eval("CategoryId"))) %>'><%#Eval("Name") %></a>
                                        </h5>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater runat="server" ID="rptProductListRows" OnItemDataBound="ProductRowDataBound" Visible="False">
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
                                    <i runat="server" visible='<%#(bool)Eval("IsFeatured") %>' class="icon icon-white icon-star item-icon-right" style="top: 10px;"></i>
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
                                            <%# ((Product)Container.DataItem).FormattedPrice() %>
                                        </div>
                                        <!-- Add to cart -->
                                        <div class="pull-right">
                                            <asp:LinkButton runat="server" ID="lbAddToCart" CssClass="btn btn-primary btn-small" CommandName="AddToCart" CommandArgument='<%#Eval("ProductId") %>' Visible='<%#(int)Eval("TotalInventory") > 0 && !(bool)Eval("CallForPricing") %>'><i class="icon-white icon-shopping-cart"></i>&nbsp;В корзину</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbReserve" CssClass="btn btn-warning btn-small" CommandName="Reserve" CommandArgument='<%#Eval("ProductId") %>' Visible='<%#(int)Eval("TotalInventory") == 0 && !(bool)Eval("CallForPricing") %>'><i class="icon-white icon-time"></i>&nbsp;Предзаказ</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbCallForPricing" CssClass="btn btn-warning btn-small" CommandName="Reserve" CommandArgument='<%#Eval("ProductId") %>' Visible='<%#(bool)Eval("CallForPricing")%>'><i class="icon-white icon-time"></i>&nbsp;Запросить цену</asp:LinkButton>
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
        <asp:HyperLink runat="server" ID="hlAddProduct" CssClass="btn btn-success" Text="Добавить продукт" Visible="False" />
    </div>
    <div class="pagination pagination-centered pagination-inverse" runat="server" id="divPagination">
        <ul>
            <li class='<% = 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/Category.aspx?id={1}&page={0}&brand={2}", CurrentPageNo - 1, ItemId, ddlBrands.SelectedValue)) %>'>« Предыдущая
                </a>
            </li>

            <asp:Repeater runat="server" ID="rptPaging">
                <ItemTemplate>
                    <li class='<%#(int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                        <a class="btn-inverse" href='<%#(int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/Category.aspx?id={1}&page={0}&brand={2}", Eval("PageNo"), ItemId, ddlBrands.SelectedValue)) %>'>
                            <%#Eval("PageNo") %>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            <li class='<% = PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/Category.aspx?id={1}&page={0}&brand={2}", CurrentPageNo + 1, ItemId, ddlBrands.SelectedValue)) %>'>Следующая »
                </a>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="LeftContent" runat="server">
    <asp:PlaceHolder runat="server" ID="phLeftContent" Visible="False">
        <h4>Фильтр</h4>
        <dl>
            <dt>Производитель</dt>
            <dd>
                <asp:DropDownList runat="server" ID="ddlBrands" OnSelectedIndexChanged="Filter" AutoPostBack="True" />
            </dd>
            <asp:Repeater runat="server" ID="rptAttributes" OnItemDataBound="BindAttrValuesToRepeaterItem">
                <ItemTemplate>
                    <dt><%#Eval("Name") %></dt>
                    <dd>
                        <asp:DropDownList runat="server" ID="ddlAttributeValues" OnSelectedIndexChanged="Filter" AutoPostBack="True" />
                    </dd>
                </ItemTemplate>
            </asp:Repeater>
        </dl>
    </asp:PlaceHolder>
</asp:Content>
