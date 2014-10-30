<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditNewsComment.aspx.cs" Inherits="AstRostov.Admin.EditNewsComment" %>

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
            <span class="control-label">Содержание комментария</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbContent" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:Button runat="server" CssClass="btn" Text="Назад к списку" OnClick="BackToNewsList"></asp:Button>
                <asp:Button runat="server" ID="SaveMainSliderItemButton" Text="Сохранить" CssClass="btn" OnClick="SaveNewsComment" />
            </div>
        </div>
    </div>
</asp:Content>
