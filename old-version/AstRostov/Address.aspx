<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="AstRostov.AddressPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Адрес доставки</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Адрес</h2>
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">ФИО заказчика</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbFullName"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ControlToValidate="tbFullName" ValidationGroup="Address"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Email</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbEmail" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-error" Display="Dynamic" ValidationGroup="Address" ErrorMessage="*" ControlToValidate="tbEmail"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Контактный телефон</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbPhone" TextMode="Phone"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbPhone"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Страна</span>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlCountry">
                    <Items>
                        <asp:ListItem Text="Россия" Value="Россия" Selected="True"></asp:ListItem>
                    </Items>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Регион</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbRegion"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbRegion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Населенный пункт</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCity"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbCity"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Адрес 1</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbAddress1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbAddress1"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Адрес 2</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbAddress2"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Почтовый индекс</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbZipCode"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbZipCode"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Документ, удостоверяющий личность</span>
            <div class="controls">
                <asp:DropDownList runat="server" ID="ddlDocumentType">
                    <Items>
                        <asp:ListItem Text="Паспорт" Value="Паспорт" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Водистельское удостоверение" Value="Водистельское удостоверение"></asp:ListItem>
                    </Items>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Серия и номер документа</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbDocumentNumber"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-error" Display="Dynamic" ErrorMessage="*" ValidationGroup="Address" ControlToValidate="tbDocumentNumber"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:LinkButton runat="server" ID="btnSaveAddress" CssClass="btn btn-primary btn-large" OnClick="NextStepCheckout" ValidationGroup="Address" CausesValidation="True" >Далее &nbsp;<i class="icon-white icon-arrow-right"></i></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
