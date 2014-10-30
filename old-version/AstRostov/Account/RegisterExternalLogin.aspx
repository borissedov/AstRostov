<%@ Page Language="C#" Title="Register an external login" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="AstRostov.Account.RegisterExternalLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Связь аккаунтов</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Зарегистрируйтесь, используя ваш аккаунт <%: ProviderDisplayName %>,</h1>
        <h2><%: ProviderUserName %>.</h2>
    </hgroup>


    <asp:ModelErrorMessage runat="server" ModelStateKey="Provider" CssClass="field-validation-error" />


    <asp:PlaceHolder runat="server" ID="userNameForm">
        <fieldset>
            <legend>Связь аккаунтов</legend>
            <p>
                Вы аутентифицированы через <strong><%: ProviderDisplayName %></strong> как 
                <strong><%: ProviderUserName %></strong>. Пожалуйста, укажите <strong>логин</strong> для этого сайта и нажмите кнопку "Войти".
            </p>
            <asp:ModelErrorMessage runat="server" ModelStateKey="UserName" CssClass="field-validation-error" />
            <div class="form-horizontal">
                <div class="control-group">
                    <span class="control-label">Логин</span>
                    <div class="controls">
                        <div class="input-append">
                            <asp:TextBox runat="server" ID="userName" />
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-inverse" Text="Войти" ValidationGroup="NewUser" OnClick="logIn_Click" />
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="userName"
                            Display="Dynamic" ErrorMessage="Введите логин" ValidationGroup="NewUser" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label"></span>
                    <div class="controls">
                        <asp:Button runat="server" CssClass="btn btn-inverse" Text="Отмена" CausesValidation="false" OnClick="cancel_Click" />

                    </div>
                </div>
            </div>
        </fieldset>
    </asp:PlaceHolder>
</asp:Content>
