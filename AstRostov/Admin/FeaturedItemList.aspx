<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="FeaturedItemList.aspx.cs" Inherits="AstRostov.Admin.FeaturedItemList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список популярных товаров</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridFeaturedItems" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="FeaturedItemId" />
            <asp:BoundField HeaderText="Заглавие" DataField="ProductName" />
            <asp:BoundField HeaderText="Содержание" DataField="Description" />
            <asp:BoundField HeaderText="Цена" DataField="Price" />
            <asp:TemplateField HeaderText="Изображение">
                <ItemTemplate>
                    <img src='<%#ResolveUrl(String.Format("~/img/features/{0}",Eval("ImageFilePath"))) %>' alt='<%#Eval("ProductName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("FeaturedItemId") %>'>
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" CommandArgument='<%#Eval("FeaturedItemId") %>'>
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/Admin/EditItem.aspx?mode=FeaturedItem">Добавить</asp:HyperLink>
</asp:Content>
