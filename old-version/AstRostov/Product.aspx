﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="AstRostov.ProductPage" %>

<%@ Import Namespace="AstCore.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Продукт <%=Product.Name %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="pnlProduct">
        <ProgressTemplate>
            <div class="wait-progress">
                <img alt="Пожалуйста, ждите" title="Пожалуйста, ждите" src='<%=ResolveUrl("~/img/loading.gif") %>' />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="pnlProduct">
        <ContentTemplate>
            <script>
                $(document).ready(function () {
                    $('.thumbnails .thumbnail').click(function () {
                        if (!$(this).parent().hasClass('active')) {
                            $('#imgMainImage').attr('src', $(this).children('input[type="hidden"]').val());
                            $('#imgMainImageHref').attr('href', $(this).children('input[type="hidden"]').val());
                            $(this).addClass('active');
                            $(this).siblings().removeClass('active');
                        }
                    });

                    $('#tbProductAddCount').change(function () {
                        if ($(this).val() > 99) {
                            $(this).val(99);
                        }
                        if ($(this).val() < 1) {
                            $(this).val(1);
                        }
                    });
                });

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

            <asp:HiddenField runat="server" ID="hdnItemId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnSkuId"></asp:HiddenField>
            <ucn:ProductBreadCrumbs runat="server" ID="ucProductBreadCrumbs"></ucn:ProductBreadCrumbs>
            <a href='<%=ResolveUrl(String.Format("~/Product.aspx?id={0}", Product.ProductId)) %>'>
                <h2>
                    <%=Product.Name %>
                </h2>
            </a>
            <br />

            <div class="row-fluid" id="product-main-info">
                <div class="span4">
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="item-image thumbnail">
                                <a id="imgMainImageHref" href='<%=ResolveUrl(AstImage.GetImageHttpPathLarge(SelectedSku == null || !SelectedSku.Images.Any() ? Product.MainImage.ToString() : SelectedSku.MainImage.ToString())) %>'>
                                    <img id="imgMainImage" src='<%=ResolveUrl(AstImage.GetImageHttpPathLarge(SelectedSku == null || !SelectedSku.Images.Any() ? Product.MainImage.ToString() : SelectedSku.MainImage.ToString())) %>' alt="" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="thumbnails">
                                <asp:Repeater runat="server" ID="rptThumbnails">
                                    <ItemTemplate>
                                        <div class='thumbnail <%#(bool)Eval("IsMain") ? "active" : "" %>'>
                                            <img alt="" src='<%#ResolveUrl(AstImage.GetImageHttpPathSmall((String)Eval("FileName"))) %>' />
                                            <input type="hidden" value='<%#ResolveUrl(AstImage.GetImageHttpPathLarge((String)Eval("FileName"))) %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span5">
                    <!-- Title -->
                    <h4><%=Product.Name %></h4>
                    <dl class="dl-horizontal">
                        <dt>Артикул:</dt>
                        <dd><%=SelectedSku != null ? SelectedSku.SkuNumber : String.Empty %>&nbsp;</dd>
                        <dt>Производитель:</dt>
                        <dd><%=Product.Brand %>&nbsp;</dd>

                        <asp:Repeater runat="server" ID="rptAttrs" OnItemDataBound="BindAttrValuesForRptItem">
                            <ItemTemplate>
                                <dt><%#Eval("Name") %></dt>
                                <dd>
                                    <asp:DropDownList runat="server" ID="ddlAttrValues" OnSelectedIndexChanged="BindAttributeSku" AutoPostBack="True" /></dd>
                                <asp:HiddenField runat="server" ID="hdnAttrId" Value='<%#Eval("AttributeId") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder runat="server" ID="phInventory">
                            <dt>Остаток на складе:</dt>
                            <dd>
                                <asp:Literal runat="server" ID="litInventory"></asp:Literal></dd>
                        </asp:PlaceHolder>
                    </dl>
                    <h4 id="priceHolder" runat="server"><strong>Цена: <%=SelectedSku != null ? SelectedSku.FormattedPrice(Count) : Product.FormattedPrice(Count)  %></strong></h4>

                    <asp:PlaceHolder runat="server" ID="phProductActions">
                        <div class="input-append">
                            <asp:TextBox runat="server" TextMode="Number" Text="1" Width="37" ID="tbProductAddCount" ClientIDMode="Static" OnTextChanged="CheckInventory" AutoPostBack="True"></asp:TextBox>
                            <asp:LinkButton CssClass="btn btn-primary" runat="server" ID="btnAddToCart" OnClick="AddToCart">
                    <i class="icon-white icon-shopping-cart"></i>&nbsp;Добавить в корзину
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnReserveProduct" OnClick="ReserveProduct" CssClass="btn btn-warning" Text="Оформить предзаказ"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnCallForPricing" OnClick="ReserveProduct" CssClass="btn btn-warning" Text="Запросить цену" Visible="False"></asp:LinkButton>
                        </div>
                    </asp:PlaceHolder>
                    <asp:HyperLink runat="server" ID="hlEdit" Visible="False" Text="Редактировать" CssClass="btn btn-success"></asp:HyperLink>
                    <asp:Button runat="server" ID="btnDelete" Visible="False" Text="Удалить" CssClass="btn btn-danger" OnClick="DeleteProduct"></asp:Button>
                    <div class="social-row">
                        <div id="vk_like"></div>
                        <script type="text/javascript">
                            VK.Widgets.Like("vk_like", { type: "button" });
                        </script>
                    </div>
                </div>
            </div>
            <br>
            <div class="row-fluid">
                <div class="span12">
                    <ul id="myTab" class="nav nav-tabs">
                        <!-- Use uniqe name for "href" in below anchor tags -->
                        <li class="active"><a href="#tab1" data-toggle="tab">Описание</a></li>
                        <%--<li class=""><a href="#tab2" data-toggle="tab">Отзывы</a></li>--%>
                    </ul>
                    <div id="myTabContent" class="tab-content">

                        <div class="tab-pane fade active in" id="tab1">
                            <h5><strong><%=Product.Name %></strong></h5>
                            <%=Product.Description %>
                            <br />
                            <%=SelectedSku != null ? SelectedSku.AdditionalDescription : ""  %>
                        </div>

                        <%-- <div class="tab-pane fade" id="tab2">
                    <h5><strong>Product Reviews :</strong></h5>
                    <hr>
                    <div class="item-review">
                        <h5>Ravi Kumar - <span class="color">4/5</span></h5>
                        <p class="rmeta">27/1/2012</p>
                        <p>Suspendisse potenti. Morbi ac felis nec mauris imperdiet fermentum. Aenean sodales augue ac lacus hendrerit sed rhoncus erat hendrerit. Vivamus vel ultricies elit. Curabitur lacinia nulla vel tellus elementum non mollis justo aliquam.</p>
                    </div>

                    <hr>
                    <h5><strong>Write a Review</strong></h5>
                    <hr>
                    <asp:Panel runat="server">
                        <div class="form-group">
                            <label for="name">Your Name</label>
                            <input type="text" class="form-control" id="name" placeholder="Enter Name">
                        </div>
                        <div class="form-group">
                            <label for="exampleInputEmail1">Email address</label>
                            <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">
                        </div>
                        <div class="form-group">
                            <label for="rating">Rating</label>
                            <!-- Dropdown menu -->
                            <select class="form-control">
                                <option>Rating</option>
                                <option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputEmail1">Review</label>
                            <textarea class="form-control" rows="3"></textarea>
                        </div>
                        <button type="submit" class="btn btn-info">Send</button>
                        <button type="reset" class="btn btn-default">Reset</button>
                    </asp:Panel>

                </div>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
