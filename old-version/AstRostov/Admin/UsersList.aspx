<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="AstRostov.Admin.UsersList" %>
<%@ Import Namespace="AstCore.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список пользователей</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridUsers" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <%--<asp:BoundField HeaderText="UserName" DataField="BlogId" />--%>
            <asp:BoundField HeaderText="UserName" DataField="UserName" />
            <asp:BoundField HeaderText="Последняя активность" DataField="LastActivityDate" />
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Address") as Address != null ? ((Address)Eval("Address")).Email : Eval("Membership") as AstCore.Models.Membership != null ? ((AstCore.Models.Membership)Eval("Membership")).Email : "" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Телефон">
                <ItemTemplate>
                    <%# Eval("Address") as Address != null ? ((Address)Eval("Address")).Phone : "" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Адрес">
                <ItemTemplate>
                    <%# Eval("Address") as Address != null ? ((Address)Eval("Address")).ShortAddress : "" %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
