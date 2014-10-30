<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="BlogList.aspx.cs" Inherits="AstRostov.Admin.BlogList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список бортжурналов</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridBlogs" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="BlogId" />
            <asp:BoundField HeaderText="Название" DataField="Title" />
            <asp:TemplateField HeaderText="Автор">
                <ItemTemplate>
                    <%# Eval("Author") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("BlogId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("BlogId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
