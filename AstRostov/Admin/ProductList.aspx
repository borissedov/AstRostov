<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="ProductList.aspx.cs" Inherits="AstRostov.Admin.ProductList" %>

<%@ Import Namespace="AstCore.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список продуктов</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridProducts" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ProductId" />
            <asp:TemplateField>
                <ItemTemplate>
                    <img alt='<%#Eval("Name")%>' src='<%# ResolveUrl(AstImage.GetImageHttpPathMedium(Eval("MainImage").ToString()))%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Название" DataField="Name" />
            <asp:BoundField HeaderText="Категория" DataField="Category" />
            <asp:BoundField HeaderText="Производитель" DataField="Brand" />
            <asp:BoundField HeaderText="Цена" DataField="RetailPrice" />
            <asp:BoundField HeaderText="Цена со скидкой" DataField="SalePrice" />
            <asp:BoundField HeaderText="В наличии" DataField="Inventory" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ProductId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" CommandArgument='<%#Eval("ProductId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/Admin/EditProduct.aspx" Text="Добавить"></asp:HyperLink>
</asp:Content>
