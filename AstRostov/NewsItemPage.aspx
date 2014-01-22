<%@ Page Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="NewsItemPage.aspx.cs" Inherits="AstRostov.NewsItemPage" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <title>АСТ-Ростов. Новости. <%=CurrentNewsItem.Title %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <hgroup class="title">
        <h1><%=CurrentNewsItem.Title %></h1>
    </hgroup>
    <article>
        <%=CurrentNewsItem.Content %>
    </article>
    <br />
    <div style="clear: both"></div>
    <div>
        <span class="badge badge-success">Размещено <%=CurrentNewsItem.Created.ToString("g") %></span>
        <span class="badge badge-success"><%= CurrentNewsItem.Author %></span>
        <div class="pull-right">
            <span class="label">новости</span>
        </div>
    </div>
    <div class="social-row">
            <div id="vk_like"></div>
            <script type="text/javascript">
                VK.Widgets.Like("vk_like", { type: "button" });
            </script>
        </div>
    <hr />
    <asp:Repeater runat="server" ID="rptComments">
        <HeaderTemplate>
            <hgroup>
                <h3>Комментарии</h3>
            </hgroup>
        </HeaderTemplate>
        <ItemTemplate>
            <article>
                <header class="post-header">
                    <div class="pull-left">
                        <img class="profile-pic" src='<%=ResolveUrl("~/img/default_user.png") %>' alt="">
                    </div>
                    <div>
                        <strong><%#Eval("Author") %></strong>
                        <br />
                        <p class="muted">
                            <small>
                                <%# ((DateTime)Eval("Created")).ToString("G") %>
                            </small>
                        </p>
                    </div>
                    <div class="clearfix"></div>
                </header>
                <div class="post-content">
                    <p><%#Eval("Content") %> </p>
                </div>
            </article>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Panel ID="pnlAddComment" runat="server">
        <hgroup>
            <h3>Добавить комментарий</h3>
        </hgroup>
        <div class="row-fluid">
            <div class="span7">
                <asp:TextBox runat="server" ID="tbCommentBody" TextMode="MultiLine" Style="resize: none; width: 100%;" Rows="4"></asp:TextBox>
                <asp:Button runat="server" ID="btnAddComment" CssClass="btn btn-inverse pull-right" Text="Отправить" OnClick="AddComment" CausesValidation="True" ValidationGroup="NewsComment" />
                <asp:Label runat="server" ID="lblError" CssClass="label label-important" Text="Комментарий должен быть от 1 до 1000 символов в длину." Visible="False"></asp:Label>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pleaseLogin">
        <h4>Только зарегистрированные пользователи могут просматривать и оставлять комментарии.</h4>
        <p><a href='<%=ResolveUrl("~/Account/Login.aspx") %>'>Войдите</a> или <a href='<%=ResolveUrl("~/Account/Register.aspx") %>'>Зарегистрируйтесь</a>.</p>
    </asp:Panel>
</asp:Content>
