<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="AstRostov.Controls.Navigation.Search" %>

<div class="input-append input-prepend">
    <span class="add-on"><i class="icon-search"></i></span>
    <asp:TextBox runat="server" ID="tbSearch" placeholder="Поиск" Width="190" />
    <asp:Button runat="server" ID="btnSearch" class="btn btn-inverse" OnClick="InitiateSearch" Text="Найти" />
</div>
