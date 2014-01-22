<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturesVertical.ascx.cs" Inherits="AstRostov.Controls.Home.FeaturesVertical" %>
<%@ Import Namespace="AstCore.Helpers" %>


<div id="features-vertical" class="carousel slide vertical">
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
                <div class="item">
                    <asp:Repeater runat="server" ID="rptFeaturedItems" OnItemCommand="ProcessFeaturedCommand">
                        <ItemTemplate>
                            <div class="row-fluid">
                                <div class="span4">
                                    <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'>
                                        <img src='<%#ResolveUrl(AstImage.GetImageHttpPathSmall(Eval("MainImage").ToString())) %>' alt="" class="img-responsive thumbnail">
                                    </a>
                                </div>
                                <div class="span8">
                                    <div class="item-details">
                                        <!-- Name -->
                                        <h5>
                                            <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", Eval("ProductId"))) %>'><%#Eval("Name") %></a>
                                        </h5>
                                        <div class="clearfix"></div>
                                        <!-- Price -->
                                        <div class="price">
                                            <%#Eval("FormattedPrice") %>
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
</div>






