<%@ Page Title="About" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="AstRostov.About" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <title>АСТ-Ростов. О компании</title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>О нас</h1>
    </hgroup>

    <article>
        <p>        
            Use this area to provide additional information.
        </p>

        <p>        
            Use this area to provide additional information.
        </p>

        <p>        
            Use this area to provide additional information.
        </p>
    </article>

    <aside>
        <h3>Дополнительная информация</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a runat="server" href="~/">Главная</a></li>
            <li><a runat="server" href="~/About.aspx">О нас</a></li>
            <li><a runat="server" href="~/Contact.aspx">Контакты</a></li>
        </ul>
    </aside>
</asp:Content>