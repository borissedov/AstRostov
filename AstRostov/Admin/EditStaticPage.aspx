<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditStaticPage.aspx.cs" Inherits="AstRostov.Admin.EditStaticPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>
        <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>

    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Заглавие страницы</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbStaticPageTitle"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Ключ</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbStaticPageKey"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Тело страницы</span>
            <div class="controls">
                <ucc:RichTextBox runat="server" ID="tbStaticPageContent"></ucc:RichTextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <asp:HyperLink runat="server" NavigateUrl="~/Admin/StaticPageList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                <asp:Button runat="server" ID="SaveStaticPageButton" Text="Сохранить" CssClass="btn" OnClick="SaveStaticPage" />
            </div>
        </div>
    </div>
</asp:Content>
