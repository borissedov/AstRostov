<%@ Page Title="Register" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AstRostov.Account.Register" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Регистрация</title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>Регистрация</h1>
        <h2>Зарегистрируйтесь, используя эту форму.</h2>
    </hgroup>
    <div class="row-fluid">
        <div class="span6">
            <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatedUser="RegisterUser_CreatedUser">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
                    <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
                </LayoutTemplate>
                <WizardSteps>
                    <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                        <ContentTemplate>
                            <p class="message-info">
                                Пароль должен состоять минимум из <%: Membership.MinRequiredPasswordLength %> символов.
                            </p>

                            <p class="validation-summary-errors">
                                <asp:Literal runat="server" ID="ErrorMessage" />
                            </p>

                            <div class="form-horizontal">
                                <legend>Регистрация Пользователя</legend>
                                <div class="control-group">
                                    <asp:Label runat="server" AssociatedControlID="UserName" CssClass="control-label">Логин</asp:Label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="UserName" placeholder="Логин" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                            CssClass="field-validation-error" ErrorMessage="Введите логин." />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Label runat="server" AssociatedControlID="Email" CssClass="control-label">Email</asp:Label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="Email" TextMode="Email" placeholder="Email" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                            CssClass="field-validation-error" ErrorMessage="Введите адрес электронной почты." />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Пароль</asp:Label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Пароль" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                            CssClass="field-validation-error" ErrorMessage="Введите пароль." />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="control-label">Подтверждение пароля</asp:Label>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" placeholder="Подтверждение пароля" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Введите подтверждение пароля." />
                                        <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Пароли не совпадают." />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <div class="controls">
                                        <asp:Button runat="server" CssClass="btn btn-inverse" CommandName="MoveNext" Text="Зарегистрироваться" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <CustomNavigationTemplate />
                    </asp:CreateUserWizardStep>
                </WizardSteps>
            </asp:CreateUserWizard>
        </div>
        <div class="span6">
            <section id="socialLoginForm">
                <legend>Или войдите в один клик через одну из социальных сетей.</legend>
                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
            </section>
        </div>
    </div>
</asp:Content>
