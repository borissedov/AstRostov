<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs"  MasterPageFile="~/Catalog.master" Inherits="AstRostov.Catalog1" %>
<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Каталог</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="pull-left">
                <a href='<%=HttpContext.Current.Request.Url.PathAndQuery%>'>
                    <h2>
                        Каталог
                    </h2>
                </a>
            </div>
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
    </div>
    <div class="pagination pagination-centered pagination-inverse" runat="server" id="divPagination">
        <ul>
            <li class='<% = 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/Catalog.aspx?page={0}", CurrentPageNo - 1)) %>'>« Предыдущая
                </a>
            </li>

            <asp:Repeater runat="server" ID="rptPaging">
                <ItemTemplate>
                    <li class='<%#(int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                        <a class="btn-inverse" href='<%#(int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/Catalog.aspx?page={0}", Eval("PageNo"))) %>'>
                            <%#Eval("PageNo") %>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            <li class='<% = PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/Catalog.aspx?page={0}", CurrentPageNo + 1)) %>'>Следующая »
                </a>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
