<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="StaticPageList.aspx.cs" Inherits="AstRostov.Admin.StaticPageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список новостей</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridStaticPages" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="Id" />
            <asp:BoundField HeaderText="Ключ" DataField="Key" />
            <asp:BoundField HeaderText="Заглавие" DataField="Title" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("Id") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("Id") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/Admin/EditStaticPage.aspx" Text="Добавить"></asp:HyperLink>

</asp:Content>
