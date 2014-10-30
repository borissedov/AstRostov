<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentNews.ascx.cs" Inherits="AstRostov.Controls.News.RecentNews" %>

<h1>Новости</h1>

<asp:Repeater runat="server" ID="rptNews">
    <ItemTemplate>
        <a href='<%#ResolveUrl(ResolveUrl(String.Format("~/NewsItem.aspx?id={0}", Eval("NewsItemId")))) %>' style="text-decoration: none;">
            <h3>
                <a href='<%#ResolveUrl(String.Format("~/NewsItemPage.aspx?id={0}", Eval("NewsItemId"))) %>'>
                    <%#Eval("Title") %>
                </a>
            </h3>
        </a>
        <p>
            <%# Eval("ShortContent") %>
        </p>
        <div>
            <span class="badge badge-success">Размещено <%# ((DateTime)Eval("Created")).ToString("g") %></span>
            <span class="badge badge-success"><%# GetAuthorName(Eval("Author")) %></span>
            <div class="pull-right">
                <span class="label">новости</span>
            </div>
        </div>
        <hr>
    </ItemTemplate>
</asp:Repeater>
<div class="pull-right">
    <asp:HyperLink runat="server" NavigateUrl="~/NewsArchive.aspx" Text="Архив новостей"></asp:HyperLink>
</div>
