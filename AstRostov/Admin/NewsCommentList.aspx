<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="NewsCommentList.aspx.cs" Inherits="AstRostov.Admin.NewsCommentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список комментариев</h2>
    <asp:HiddenField runat="server" ID="hdnNewsItemId" />
    <h3 id="subheaderFromUser" runat="server">к новости #<%=OwnerNewsItem.NewsItemId %></h3>
    <asp:HiddenField runat="server" ID="hdnAuthor" />
    <h3 id="subheaderOfNewsItem" runat="server">пользователя <%=Author %></h3>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridComments" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="NewsCommentId" />
            <asp:TemplateField HeaderText="Содержание">
                <ItemTemplate>
                    <%# ((string)Eval("Content")).Length > 500 ? string.Format("{0}...", ((string)Eval("Content")).Substring(0, 500)) : Eval("Content") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID Новости" DataField="NewsItemId" />
            <asp:TemplateField HeaderText="Автор">
                <ItemTemplate>
                    <%# Eval("Author") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Создана" DataField="Created" />
            <asp:BoundField HeaderText="Редактирована" DataField="Updated" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("NewsCommentId") %>'>
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("NewsCommentId") %>'>
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
