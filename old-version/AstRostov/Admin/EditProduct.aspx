﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="AstRostov.Admin.EditProduct" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Register TagName="AdminImageUploader" TagPrefix="ast" Src="~/Admin/Controls/ImageUploader.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>
        <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>

    <asp:Label runat="server" ID="ErrorLabel" Visible="False"  CssClass="text-warning"/>

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Название Продукта</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbProductName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbProductName" Display="Dynamic" ErrorMessage="*" CssClass="text-error"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Описание</span>
            <div class="controls">
                <ucc:RichTextBox runat="server" ID="tbDescription"></ucc:RichTextBox>
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
        <div class="control-group">
            <span class="control-label">Цена по запросу</span>
            <div class="controls">
                <asp:CheckBox runat="server" ID="chbCallForPricing"></asp:CheckBox>
            </div>
        </div>
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
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/ProductList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                <asp:Button runat="server" ID="btnSaveProduct" Text="Сохранить" CssClass="btn" OnClick="SaveProduct" CausesValidation="True" />
            </div>
        </div>
    </div>

    <asp:PlaceHolder runat="server" ID="phImages">
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
    <asp:PlaceHolder runat="server" ID="phSkuList">
        <hr />
        <h3>Конфигурации</h3>
        <asp:GridView runat="server" ID="gridSkus" OnRowCommand="OnAttributeRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="SkuId" />
                <asp:BoundField HeaderText="Артикул" DataField="SkuNumber" />
                <asp:BoundField HeaderText="Конфигурация атрибутов" DataField="AttributeConfig" />
                <asp:BoundField HeaderText="Количество на складе" DataField="Inventory" />
                <asp:BoundField HeaderText="Цена*" DataField="RetailPrice" />
                <asp:BoundField HeaderText="Цена со скидкой*" DataField="SalePrice" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("SkuId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="DeleteSku" CommandArgument='<%#Eval("SkuId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HyperLink runat="server" Text="Редактировать схему конфигураций" CssClass="btn btn-success" ID="hlEditAttrConfig"></asp:HyperLink>
        <asp:HyperLink runat="server" Text="Добавить новую конфигурацию" CssClass="btn btn-success" ID="hlAddSku"></asp:HyperLink>
    </asp:PlaceHolder>
    <br/>
    <asp:LinkButton runat="server" OnClick="ToCategory" CssClass="btn btn-danger" ID="lbtnToCategory" Text="К категории"></asp:LinkButton>
</asp:Content>
