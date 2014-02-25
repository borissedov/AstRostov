<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditProductConfiguration.aspx.cs" Inherits="AstRostov.Admin.EditProductConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnProductId" />
    <asp:HiddenField runat="server" ID="hdnAttributeDictionary"/>
    <h2>Редактирование схемы конфигураций продукта</h2>
    <br />
    <div class="form-horizontal">
        <asp:Repeater runat="server" ID="rptAttributes">
            <ItemTemplate>
                <div class="control-group">
                    <asp:TextBox runat="server" CssClass="control-label" ID="tbAttributeName" placeholder="Название атрибута" Text='<%#Eval("Name") %>'></asp:TextBox>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="tbAttributeDefaultValue" placeholder="Значение атрибута по умолчанию"></asp:TextBox>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Label runat="server" CssClass="text-error" ID="lblError" ClientIDMode="Static"></asp:Label>
    <br />
    <span class="warning">Внимание! Сохранение схемы конфигураций удалит все существующие конфигурации продукта и создаст конфигурацию "по умолчанию" с заданными значаниями атрибутов.</span>
    <br/>
    <asp:HyperLink runat="server" ID="hlBack" CssClass="btn" Text="Назад к продукту"></asp:HyperLink>
    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Сохранить" OnClick="SaveConfiguration" />
</asp:Content>
