<%@ Page Title="About" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="StaticPage.aspx.cs" Inherits="AstRostov.StaticPagePage" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <title>АСТ-Ростов. <%=StaticPage.Title %></title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1> <%=StaticPage.Title %></h1>
    </hgroup>
    <asp:HiddenField runat="server" ID="hdnPageKey" Value="AboutUs"/>
    <article>
        <%=StaticPage.Content %>
    </article>
    <br/>
    <aside>
        <ul>
            <li><a runat="server" href="~/">Главная</a></li>
            <li><a runat="server" href="~/Contact.aspx">Контакты</a></li>
        </ul>
    </aside>
</asp:Content>