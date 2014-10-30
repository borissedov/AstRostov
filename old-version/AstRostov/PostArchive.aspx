<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="PostArchive.aspx.cs" Inherits="AstRostov.PostArchive" %>

<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <title>АСТ-Ростов. Записи в бортжурналах</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <h1>Записи в бортжурналах</h1>
                <asp:Repeater runat="server" ID="rptPosts" OnItemDataBound="rptPosts_OnItemDataBound" OnItemCommand="ItemCommand">
                    <ItemTemplate>
                        <h3>
                            <a href='<%#ResolveUrl(String.Format("~/Post.aspx?id={0}", Eval("PostId"))) %>'>
                                <%#Eval("Title") %>
                            </a>
                        </h3>
                        <asp:HyperLink class="btn btn-inverse btn-small" runat="server" ID="hlinkEdit" ToolTip="Редактировать"><i class="icon-white icon-edit"></i></asp:HyperLink>
                        <asp:LinkButton class="btn btn-inverse btn-small" runat="server" ID="lbDelete" ToolTip="Удалить" CommandName="Delete" CommandArgument='<%#Eval("PostId") %>'><i class="icon-white icon-trash"></i></asp:LinkButton>
                        <p>
                            <%#Eval("ShortContent") %>
                        </p>
                        <div>
                            <span class="badge badge-success">Размещено <%# ((DateTime)Eval("Created")).ToString("g") %></span>
                            <span class="badge badge-success"><%# Eval("Author") %></span>
<%--                            <a href='<%#ResolveUrl(String.Format("~/Blog.aspx?id={0}", ((Blog)Eval("Blog")).BlogId)) %>' title="К бортжурналу"><span class="badge badge-success"><%# Eval("Author") %></span></a>--%>
                            <div class="pull-right">
                                <span class="label">Блог</span>
                            </div>
                        </div>
                        <hr>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="pagination pagination-centered pagination-inverse">
                    <ul>
                        <li class='<%= 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                            <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/PostArchive.aspx?page={0}", CurrentPageNo - 1)) %>'>« Предыдущая
                            </a>
                        </li>

                        <asp:Repeater runat="server" ID="rptPaging">
                            <ItemTemplate>
                                <li class='<%# (int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                                    <a class="btn-inverse" href='<%# (int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/PostArchive.aspx?page={0}", Eval("PageNo"))) %>'>
                                        <%#Eval("PageNo") %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                        <li class='<%= PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                            <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/PostArchive.aspx?page={0}", CurrentPageNo + 1)) %>'>Следующая »
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
