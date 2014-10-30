<%@ Page Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="MyBlog.aspx.cs" Inherits="AstRostov.MyBlog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Мой бортжурнал</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <div>
        <h1>
            <asp:Literal runat="server" ID="litBlogTitle"></asp:Literal></h1>
    </div>

    <div class="container-fluid">
        <p runat="server" id="lblPostsEmpty" visible="False">В бортжурнале пока нет записей</p>

        <asp:Button runat="server" CssClass="btn btn-inverse" Text="Добавить запись" ID="btnAddPost" OnClick="AddPost" />

        <asp:Repeater runat="server" ID="rptPosts" OnItemDataBound="rptPosts_OnItemDataBound">
            <ItemTemplate>
                <div class="row-fluid">
                    <div class="span12">
                        <div>
                            <h2 id="title" itemprop="name headline">
                                <a href="#"><%#Eval("Title") %></a>
                            </h2>

                        </div>
                        <div>
                            <i class="icon-calendar icon-white"></i>
                            <time itemprop="datePublished"><%#((DateTime)Eval("Created")).ToString("F") %></time>
                        </div>
                        <div>
                            <i class="icon-tags icon-white"></i>
                            <span class="label label-info">Блог</span>
                        </div>
                    </div>
                </div>
                <div class="row-fluid" itemprop="articleBody">
                    <div class="span12">
                        <%#Eval("ShortContent") %>
                        <div class="pull-right">
                            <asp:HyperLink class="btn btn-inverse" runat="server" ID="hlinkEdit" ToolTip="Редактировать"><i class="icon-white icon-edit"></i></asp:HyperLink><a class="btn btn-inverse" href='<%#ResolveUrl(String.Format("~/Post.aspx?id={0}", Eval("PostId"))) %>'>Читать полностью »</a></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>



        <div class="pagination pagination-centered pagination-inverse">
            <ul>
                <li class='<%= 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                    <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/MyBlog.aspx?page={0}", CurrentPageNo - 1)) %>'>« Предыдущая
                    </a>
                </li>

                <asp:Repeater runat="server" ID="rptPaging">
                    <ItemTemplate>
                        <li class='<%# (int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                            <a class="btn-inverse" href='<%# (int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/MyBlog.aspx?page={0}", Eval("PageNo"))) %>'>
                                <%#Eval("PageNo") %>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>

                <li class='<%= PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                    <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/<MyBlog.aspx?page={0}", CurrentPageNo + 1)) %>'>Следующая »
                    </a>
                </li>
            </ul>
        </div>

    </div>

</asp:Content>
