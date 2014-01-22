<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="CategoryList.aspx.cs" Inherits="AstRostov.Admin.CategoryList" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список категорий</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridCategories" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="CategoryId" />
            <asp:BoundField HeaderText="Название" DataField="Name" />
            <asp:BoundField HeaderText="Является корневой" DataField="IsRoot" />
            <asp:TemplateField HeaderText="Количество продуктов">
                <ItemTemplate>
                    <%# ((ICollection<Product>)Eval("Products")).Count() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("CategoryId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("CategoryId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/Admin/EditCategory.aspx" Text="Добавить"></asp:HyperLink>
</asp:Content>
