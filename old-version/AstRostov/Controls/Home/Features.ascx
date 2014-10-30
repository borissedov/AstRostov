<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Features.ascx.cs" Inherits="AstRostov.Controls.Home.Features" %>
<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>


<div id="features-horisontal" class="carousel slide">
    <ol class="carousel-indicators">
        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
        <li data-target="#myCarousel" data-slide-to="1"></li>
        <li data-target="#myCarousel" data-slide-to="2"></li>
    </ol>

    <a class="carousel-control left" href="#myCarousel" data-slide="prev">&lsaquo;</a>
    <a class="carousel-control right" href="#myCarousel" data-slide="next">&rsaquo;</a>


    <div class="carousel-inner shop-items" style="margin: 0;">
        <asp:Repeater runat="server" ID="rptFeaturedListRows" OnItemDataBound="ProductRowDataBound">
            <ItemTemplate>
                <div class="item row-fluid">
                    <asp:Repeater runat="server" ID="rptFeaturedItems" OnItemCommand="ProcessFeaturedCommand">
                        <ItemTemplate>
                            <div class="product-item span4">
                                <!-- Use the below link to put HOT icon -->
                                <div class="item-icon badge badge-ast" runat="server" visible='<%#Eval("Brand") != null %>'>
                                    <span><%#Eval("Brand") ?? "" %></span>
                                </div>
                                <i class="icon-white icon-star item-icon-right" style="margin-top: -8px;"></i>
                                <!-- Item image -->
                                <div class="item-image">
                                    <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'>
                                        <img src='<%#ResolveUrl(AstImage.GetImageHttpPathMedium(Eval("MainImage").ToString())) %>' alt="" class="img-responsive thumbnail" style="width: 150px;">
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
                                    <div class="price">
                                        <%# ((Product)Container.DataItem).FormattedPrice() %>
                                    </div>
                                    
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</div>






