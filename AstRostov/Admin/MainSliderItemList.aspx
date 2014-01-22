<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="MainSliderItemList.aspx.cs" Inherits="AstRostov.Admin.MainSliderItemList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список популярных товаров</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridMainSliderItems" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="MainSliderItemId" />
            <asp:BoundField HeaderText="Заглавие" DataField="Title" />
            <asp:BoundField HeaderText="Цена" DataField="Price"/>
            <asp:TemplateField HeaderText="Изображение">
                <ItemTemplate>
                    <img src='<%#ResolveUrl(String.Format("~/img/main-slider/{0}",Eval("ImageFile"))) %>' alt='<%#Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("MainSliderItemId") %>'>
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" CommandArgument='<%#Eval("MainSliderItemId") %>'>
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/Admin/EditMainSliderItem.aspx">Добавить</asp:HyperLink>
</asp:Content>
