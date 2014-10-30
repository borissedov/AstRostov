<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditBrand.aspx.cs" Inherits="AstRostov.Admin.EditBrand" %>
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
            <span class="control-label">Название производителя</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbBrandName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbBrandName" Display="Dynamic" ErrorMessage="*" CssClass="text-error"></asp:RequiredFieldValidator>
                <asp:CustomValidator runat="server" ID="cvValidateUniqueName" ControlToValidate="tbBrandName" OnServerValidate="ValidateUniqueName" CssClass="text-error" Display="Dynamic" ValidateEmptyText="True" ErrorMessage="Название производителя должно быть уникальным"></asp:CustomValidator>
            </div>
        </div>
        
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/BrandList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                <asp:Button runat="server" ID="btnSaveBrand" Text="Сохранить" CssClass="btn" OnClick="SaveBrand" CausesValidation="True"/>
            </div>
        </div>
    </div>

   
</asp:Content>
