<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AstRostov.Account.Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Вход на сайт</title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>Вход на сайт.</h1>
    </hgroup>

    <div class="row-fluid">
        <div class="span6">
            <section id="loginForm">
                <legend>Используйте аккаунт, зарегистрированный на нашем сайте.</legend>
                <asp:Login runat="server" ID="cLogin" ViewStateMode="Disabled" RenderOuterTable="false" FailureText="Неправильный логин или пароль." OnLoginError="LoginError" OnLoggedIn="LoggedIn" >
                    <LayoutTemplate>
                        <div class="form-horizontal">
                            <div class="control-group">
                                <span class="control-label">Логин</span>
                                <div class="controls">
                                    <asp:TextBox runat="server" ID="UserName" placeholder="Логин" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" ErrorMessage="*Введите логин"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label">Пароль</span>
                                <div class="controls">
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Пароль" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" ErrorMessage="*Введите пароль"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <label class="checkbox">
                                        <asp:CheckBox runat="server" ID="RememberMe" Text="Запомнить?" />
                                    </label>
                                    <asp:Button runat="server" CommandName="Login" CssClass="btn btn-inverse" Text="Войти" />
                                </div>
                            </div>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
                
                <p>
                    <asp:Literal runat="server" ID="litError"></asp:Literal>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Зарегистрируйтесь,</asp:HyperLink>
                    если у Вас нет аккаунта.
                </p>
            </section>
        </div>

        <div class="span6">
            <section id="socialLoginForm">
                <legend>Или войдите в один клик через одну из социальных сетей.</legend>
                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
            </section>
        </div>
    </div>
</asp:Content>
