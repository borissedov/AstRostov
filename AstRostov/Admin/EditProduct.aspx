<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="AstRostov.Admin.EditProduct" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Register TagName="AdminImageUploader" TagPrefix="ast" Src="~/Admin/Controls/ImageUploader.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>
        <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>

    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Название Продукта</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbProductName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbProductName" Display="Dynamic" ErrorMessage="*" CssClass="text-error"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Артикул Продукта</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbProductNum"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Описание</span>
            <div class="controls">
                <ucc:RichTextBox runat="server" ID="tbDescription"></ucc:RichTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbDescription" Display="Dynamic" ErrorMessage="*" CssClass="text-error"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Цена</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbRetailPrice"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbRetailPrice" Display="Dynamic" ErrorMessage="*" CssClass="text-error"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Цена со скидкой</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbSalePrice"></asp:TextBox>
            </div>
        </div>
        <%--<div class="control-group">
            <span class="control-label">Количество на складе</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbInventory" TextMode="Number"></asp:TextBox>
            </div>
        </div>--%>
        <div class="control-group">
            <span class="control-label">Категория</span>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlCategories"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Производитель</span>
            <div class="controls">
                <asp:TextBox ID="tbBrandName" runat="server" placeholder="Введите название нового производителя или выберете из списка" Visible="False"></asp:TextBox>
                <asp:DropDownList runat="server" ID="ddlBrands"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Показывать в специальных предложениях</span>
            <div class="controls">
                <input type="checkbox" runat="server" id="chbIsFeatured" />
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/ProductList.aspx" CssClass="btn" Text="Назад к списку" ></asp:HyperLink>
                <asp:Button runat="server" ID="btnSaveProduct" Text="Сохранить" CssClass="btn" OnClick="SaveProduct" CausesValidation="True" />
            </div>
        </div>
    </div>

    <asp:PlaceHolder runat="server" ID="phImages">
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
                        <asp:LinkButton runat="server" CommandName="MakeMain" CommandArgument='<%#Eval("Id") %>' ToolTip="Сделать главным">
                                <i class="icon-white icon-star"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("Id") %>' ToolTip="Удалить">
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

    </asp:PlaceHolder>


</asp:Content>
