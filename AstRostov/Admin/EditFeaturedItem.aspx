<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="EditFeaturedItem.aspx.cs" Inherits="AstRostov.Admin.EditFeaturedItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>
        Добавление продукта в список специальных предложений</h2>

    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Название производителя</span>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlProducts"/>
            </div>
        </div>
        
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/FeaturedItemList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                <asp:Button runat="server" ID="btnSaveFeaturedItem" Text="Сохранить" CssClass="btn" OnClick="SaveFeaturedItem"/>
            </div>
        </div>
    </div>

   
</asp:Content>
