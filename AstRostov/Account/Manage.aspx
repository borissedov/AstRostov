<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="AstRostov.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Управление аккаунтом</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function validateFileSize(sender, args) {
            var uploadedFile = document.getElementById("<%=fuAvatar.ClientID %>");
            if (uploadedFile.files[0].size > 50000) // greater than 50KB
            {
                args.IsValid = false;
            }
        }
    </script>
    <hgroup class="title">
        <h1>Управление аккаунтом</h1>
    </hgroup>

    <section id="mainInfoForm">
        <h3>Вы вошли как <strong><%: User.Identity.Name %></strong>.</h3>
    </section>
    <hr />
    <section id="avatarForm">
        <h3>Аватар</h3>
        <div class="form-horizontal">
            <div class="control-group">
                <span class="control-label">
                    <asp:Image runat="server" ID="imgAvatar" />
                </span>
                <div class="controls">
                    <p>Загрузка нового аватара</p>
                    <asp:FileUpload runat="server" ID="fuAvatar" />
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Avatar" CssClass="text-error" ErrorMessage="Укажите изображение" ControlToValidate="fuAvatar"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fuAvatar"
                        CssClass="text-error" Display="Dynamic" ErrorMessage="Выберите корректное JPEG, JPG, PNG или GIF изображение"
                        ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])" ValidationGroup="Avatar"></asp:RegularExpressionValidator>
                    <asp:CustomValidator Display="Dynamic" CssClass="text-error" EnableClientScript="True" ID="sizeValidator" ClientIDMode="Static" ErrorMessage="Размер файла не должен привышать 50kB" runat="server" ValidationGroup="Avatar" ClientValidationFunction="validateFileSize"></asp:CustomValidator>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-inverse" ID="btnUploadAvatar" OnClick="UploadNewAvatar" ValidationGroup="Avatar" Text="Загрузить" />
                </div>
            </div>
        </div>
    </section>
    <hr />
    <section id="addressForm">
        <h3>Адрес</h3>
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
                </div>
            </div>
            <div class="control-group">
                <span class="control-label"></span>
                <div class="controls">
                    <asp:Button runat="server" ID="btnSaveAddress" CssClass="btn btn-inverse" OnClick="SaveAddress" Text="Сохранить адрес" ValidationGroup="Address" CausesValidation="True" />
                </div>
            </div>

        </div>
        <asp:Label runat="server" ID="lblAddressSavedSuccess" Text="Адрес успешно сохранен" CssClass="text-success" Visible="False"></asp:Label>
    </section>
    <hr />
    <section id="passwordForm">
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="message-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="setPassword" Visible="false">
            <p>
                У вас нет локального пароля для нашего сайта. Добавьте локальный пароль для того, чтобы заходить на сайт без использования внешнего аккаунта.
            </p>
            <h3>Форма установки пароля</h3>

            <div class="form-horizontal">
                <div class="control-group">
                    <asp:Label CssClass="control-label" runat="server" AssociatedControlID="password">Пароль</asp:Label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="password" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="password"
                            CssClass="text-error" ErrorMessage="Введите пароль."
                            Display="Dynamic" ValidationGroup="SetPassword" />

                        <asp:ModelErrorMessage ID="ModelErrorMessage1" runat="server" ModelStateKey="NewPassword" AssociatedControlID="password"
                            CssClass="text-error" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="control-group">
                    <asp:Label ID="Label1" CssClass="control-label" runat="server" AssociatedControlID="confirmPassword">Подтверждение пароля</asp:Label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="confirmPassword"
                            CssClass="text-error" Display="Dynamic" ErrorMessage="Введите подтверждение пароля."
                            ValidationGroup="SetPassword" />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="Password" ControlToValidate="confirmPassword"
                            CssClass="text-error" Display="Dynamic" ErrorMessage="Пароли не совпадают."
                            ValidationGroup="SetPassword" />
                    </div>
                </div>
                <div class="control-group">
                    <span class="control-label"></span>
                    <div class="controls">
                        <asp:Button ID="Button2" runat="server" Text="Установить пароль" ValidationGroup="SetPassword" CausesValidation="True" OnClick="setPassword_Click" CssClass="btn btn-inverse" />
                    </div>
                </div>
            </div>



        </asp:PlaceHolder>

        <asp:PlaceHolder runat="server" ID="changePassword" Visible="false">
            <asp:ChangePassword runat="server" CancelDestinationPageUrl="~/" ViewStateMode="Disabled" RenderOuterTable="false" SuccessPageUrl="Manage?m=ChangePwdSuccess">
                <ChangePasswordTemplate>
                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                    <fieldset class="changePassword">
                        <h4>Изменение пароля</h4>
                        <div class="form-horizontal">
                            <div class="control-group">
                                <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword" CssClass="control-label">Текущий пароль</asp:Label>
                                <div class="controls">
                                    <asp:TextBox runat="server" ID="CurrentPassword" CssClass="passwordEntry" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="CurrentPassword"
                                        CssClass="text-error" ErrorMessage="Введите текущий пароль."
                                        ValidationGroup="ChangePassword" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="control-group">
                                <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword" CssClass="control-label">Новый пароль</asp:Label>

                                <div class="controls">
                                    <asp:TextBox runat="server" ID="NewPassword" CssClass="passwordEntry" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="NewPassword"
                                        CssClass="text-error" ErrorMessage="Введите пароль."
                                        ValidationGroup="ChangePassword" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="control-group">
                                <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword" CssClass="control-label">Подтверждение</asp:Label>
                                <div class="controls">
                                    <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="passwordEntry" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ConfirmNewPassword"
                                        CssClass="text-error" Display="Dynamic" ErrorMessage="Введите подтверждение пароля."
                                        ValidationGroup="ChangePassword" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                        CssClass="text-error" Display="Dynamic" ErrorMessage="Пароли не совпадают."
                                        ValidationGroup="ChangePassword" />
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label"></span>
                                <div class="controls">
                                    <asp:Button ID="Button1" runat="server" CommandName="ChangePassword" Text="Изменить пароль" ValidationGroup="ChangePassword" CausesValidation="True" CssClass="btn btn-inverse" />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </ChangePasswordTemplate>
            </asp:ChangePassword>
        </asp:PlaceHolder>
    </section>
    <hr />
    <section id="externalLoginsForm">

        <asp:ListView runat="server"
            ItemType="Microsoft.AspNet.Membership.OpenAuth.OpenAuthAccountData"
            SelectMethod="GetExternalLogins" DeleteMethod="RemoveExternalLogin" DataKeyNames="ProviderName,ProviderUserId">

            <LayoutTemplate>
                <h3>Зарегистрированные внешние аккаунты</h3>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Сервис</th>
                            <th>Логин</th>
                            <th>Последняя активность</th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>

                    <td><%#: Item.ProviderDisplayName %></td>
                    <td><%#: Item.ProviderUserName %></td>
                    <td><%#: ConvertToDisplayDateTime(Item.LastUsedUtc) %></td>
                    <td>
                        <asp:Button runat="server" Text="Удалить" CommandName="Delete" CausesValidation="False"
                            ToolTip='<%# "Удалить привязку этого " + Item.ProviderDisplayName + " аккаунта" %>'
                            Visible="<%# CanRemoveExternalLogins %>" CssClass="btn btn-inverse" />
                    </td>

                </tr>
            </ItemTemplate>
        </asp:ListView>

        <h3>Привязка внешнего аккаунта.</h3>
        <uc:OpenAuthProviders runat="server" ReturnUrl="~/Account/Manage.aspx" />
    </section>
</asp:Content>
