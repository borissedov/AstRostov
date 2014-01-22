<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="NewsArchive.aspx.cs" Inherits="AstRostov.NewsArchive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Новости</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <h1>Новости</h1>
                <asp:Repeater runat="server" ID="rptNews">
                    <ItemTemplate>
                        <h3>
                            <a href='<%#ResolveUrl(String.Format("~/NewsItemPage.aspx?id={0}", Eval("NewsItemId"))) %>'>
                                <%#Eval("Title") %>
                            </a>
                        </h3>
                        <p>
                            <%#Eval("ShortContent") %>
                        </p>
                        <div>
                            <span class="badge badge-success">Размещено <%# ((DateTime)Eval("Created")).ToString("g") %></span>
                            <span class="badge badge-success"><%# Eval("Author") %></span>
                            <div class="pull-right">
                                <span class="label">Новости</span>
                            </div>
                        </div>
                        <hr>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="pagination pagination-centered pagination-inverse">
                    <ul>
                        <li class='<% = 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                            <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/NewsArchive.aspx?year={1}&month={2}&page={0}", CurrentPageNo - 1, Year, Month)) %>'>« Предыдущая
                            </a>
                        </li>

                        <asp:Repeater runat="server" ID="rptPaging">
                            <ItemTemplate>
                                <li class='<%#(int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                                    <a class="btn-inverse" href='<%#(int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/NewsArchive.aspx?year={1}&month={2}&page={0}", Eval("PageNo"), Year, Month)) %>'>
                                        <%#Eval("PageNo") %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                        <li class='<% = PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                            <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/NewsArchive.aspx?year={1}&month={2}&page={0}", CurrentPageNo + 1, Year, Month)) %>'>Следующая »
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
