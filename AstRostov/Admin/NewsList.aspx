<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="NewsList.aspx.cs" Inherits="AstRostov.Admin.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список новостей</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridNews" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="NewsItemId" />
            <asp:BoundField HeaderText="Заглавие" DataField="Title" />
            <asp:TemplateField HeaderText="Содержание">
                <ItemTemplate>
                    <%# ((string)Eval("Content")).Length > 500 ? string.Format("{0}...", ((string)Eval("Content")).Substring(0, 500)) : Eval("Content") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Автор">
                <ItemTemplate>
                    <%# Eval("Author") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Создана" DataField="Created" />
            <asp:BoundField HeaderText="Редактирована" DataField="Updated" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("NewsItemId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("NewsItemId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/Admin/EditNewsItem.aspx" Text="Добавить"></asp:HyperLink>
</asp:Content>
