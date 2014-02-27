<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSku.aspx.cs" MasterPageFile="~/Simple.Master" Inherits="AstRostov.Admin.EditSku" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Register TagName="AdminImageUploader" TagPrefix="ast" Src="~/Admin/Controls/ImageUploader.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnSkuId" />
    <h2>Редактирование конфигурации продукта</h2>
    <br />
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" CssClass="text-warning" />

    <h3>Атрибуты и значения</h3>
    <div class="form-horizontal">
        <asp:GridView runat="server" ID="gridAttributes" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Атрибут">
                    <ItemTemplate>
                        <%# ((AstCore.Models.Attribute)Eval("Attribute")).Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Значение" DataField="Value" />
            </Columns>
        </asp:GridView>
    </div>
    <hr />
    <div class="form-horizontal">
        <div class="control-group attr-config">
            <span class="control-label">Количество на складе</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbInventory" TextMode="Number" CssClass="product-qty"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="control-group attr-config">
            <span class="control-label">Переопределенная цена</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbRetailPrice"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="control-group attr-config">
            <span class="control-label">Переопределенная цена со скидкой</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbSalePrice"></asp:TextBox>
            </div>
        </div>
    </div>

    <asp:Label runat="server" CssClass="text-error" ID="lblError" ClientIDMode="Static"></asp:Label>
    <br />
    <asp:HyperLink runat="server" ID="hlBack" CssClass="btn" Text="Назад к продукту"></asp:HyperLink>
    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Сохранить" OnClick="SaveSku" />
    <hr />
    <h3>Изображения</h3>
    <asp:GridView runat="server" ID="gridImages" OnRowCommand="ImageGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:TemplateField HeaderText="Изображение">
                <ItemTemplate>
                    <img alt='<%#Eval("FileName")%>' src='<%# ResolveUrl(AstImage.GetImageHttpPathMedium(Eval("FileName").ToString()))%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Является основным" DataField="IsMain" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="MakeMain" CommandArgument='<%#Eval("Id") %>' ToolTip="Сделать главным">
                                <i class="icon-white icon-star"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" CommandArgument='<%#Eval("Id") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            У этого продукта пока нет изображений
        </EmptyDataTemplate>
    </asp:GridView>
    <h3>Загрузка изображения</h3>

    <ast:AdminImageUploader runat="server" ID="imageUploader"></ast:AdminImageUploader>
    <br />
    <asp:Button runat="server" ID="btnUploadImage" Text="Сохранить" CssClass="btn" OnClick="UploadImage" CausesValidation="False" />
    <script>
        $(document).ready(function () {
            $('.product-qty').change(function () {
                if ($(this).val() > 100) {
                    $(this).val(100);
                }
                if ($(this).val() < 0) {
                    $(this).val(0);
                }
            });
        });
    </script>
</asp:Content>
