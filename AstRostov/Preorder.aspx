<%@ Page Title="" Language="C#" MasterPageFile="~/Catalog.master" AutoEventWireup="true" CodeBehind="Preorder.aspx.cs" Inherits="AstRostov.PreorderPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Предзаказ</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#tbCount').change(function () {
                if ($(this).val() > 10) {
                    $(this).val(10);
                }
                if ($(this).val() < 1) {
                    $(this).val(1);
                }
            });
        });
    </script>

    <h1>Оформление предзаказа</h1>

    <asp:Label runat="server" ID="lblError" CssClass="text-error"></asp:Label>

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">ФИО заказчика</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCustomer"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ControlToValidate="tbCustomer"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Контактный Email</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbEmail" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ControlToValidate="tbEmail"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Контактный телефон</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbPhone" TextMode="Phone"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Название Продукта</span>
            <div class="controls">
                <asp:HiddenField runat="server" ID="hdnProductId" />
                <asp:TextBox runat="server" ID="tbProductName" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Количество</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCount" TextMode="Number" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Комментарий к заказу</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbComment" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:LinkButton runat="server" ID="btnChechout" CssClass="btn btn-warning" OnClick="CreatePreorder">Оформить предзаказ</asp:LinkButton>
            </div>
        </div>
    </div>
     
    <asp:Label runat="server" CssClass="text-success" ID="lblSuccess" Visible="False" Text="Предзаказ оформлен. На ваш email отправлена дополнительная информация."></asp:Label>
</asp:Content>
