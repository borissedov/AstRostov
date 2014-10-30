<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="BrandList.aspx.cs" Inherits="AstRostov.Admin.BrandList" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список производителей</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridBrands" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="BrandId" />
            <asp:BoundField HeaderText="Название" DataField="Name" />
            <asp:TemplateField HeaderText="Количество продуктов">
                <ItemTemplate>
                    <%# ((ICollection<Product>)Eval("Products")).Count() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("BrandId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("BrandId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <a href='<%=ResolveUrl("~/Admin/EditBrand.aspx") %>' class="btn btn-success">Добавить</a>
</asp:Content>