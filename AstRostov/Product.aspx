<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="AstRostov.ProductPage" %>

<%@ Import Namespace="AstCore.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Продукт <%=Product.Name %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('.thumbnails .thumbnail').click(function () {
                if (!$(this).parent().hasClass('active')) {
                    $('#imgMainImage').attr('src', $(this).children('input[type="hidden"]').val());
                    $(this).addClass('active');
                    $(this).siblings().removeClass('active');
                }
            });

            $('#tbProductAddCount').change(function () {
                if ($(this).val() > 10) {
                    $(this).val(10);
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
            $('#tbProductAddCount').change(function () {
                if ($(this).val() > 10) {
                    $(this).val(10);
                }
                if ($(this).val() < 1) {
                    $(this).val(1);
                }
            });
        }
    </script>

    <asp:HiddenField runat="server" ID="hdnItemId" />
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
                        <img id="imgMainImage" src='<%=ResolveUrl(AstImage.GetImageHttpPathLarge(Product.MainImage.ToString())) %>' alt="">
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
            <p><strong>Артикул:</strong> <%=Product.ProductNum %></p>
            <p><strong>Производитель:</strong> <%=Product.Brand %></p>
            <p><strong>Остаток на складе:</strong> <%=Product.DefaultSku.Inventory == 0 ? "нет на складе" : Product.DefaultSku.Inventory > 10 ? "более 10" : "менее 10"  %></p>
            <br>
            <h4><strong>Цена: <%=Product.FormattedPrice %></strong></h4>
            <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="pnlAddToCart">
                <ProgressTemplate>
                    <div class="wait-progress">
                        <img alt="Пожалуйста, ждите" title="Пожалуйста, ждите" src='<%=ResolveUrl("~/img/loading.gif") %>' />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="pnlAddToCart" class="input-append" UpdateMode="Always">
                <ContentTemplate>
                    <asp:TextBox runat="server" TextMode="Number" Text="1" Width="37" ID="tbProductAddCount" ClientIDMode="Static" OnTextChanged="CheckInventory" AutoPostBack="True"></asp:TextBox>
                    <asp:LinkButton CssClass="btn btn-primary" runat="server" ID="btnAddToCart" OnClick="AddToCart">
                    <i class="icon-white icon-shopping-cart"></i>&nbsp;Добавить в корзину
                    </asp:LinkButton>

                    <asp:Button runat="server" ID="btnReserveProduct" OnClick="ReserveProduct" CssClass="btn btn-warning" Text="Оформить предзаказ" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HyperLink runat="server" ID="hlEdit" Visible="False" Text="Редактировать" CssClass="btn btn-success"></asp:HyperLink>
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
</asp:Content>
