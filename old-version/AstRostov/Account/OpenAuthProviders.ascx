<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenAuthProviders.ascx.cs" Inherits="AstRostov.Account.OpenAuthProviders" %>

<fieldset class="open-auth-providers">
    <asp:ListView runat="server" ID="providerDetails" ItemType="Microsoft.AspNet.Membership.OpenAuth.ProviderDetails"
        SelectMethod="GetProviderNames" ViewStateMode="Disabled">
        <ItemTemplate>
            <button type="submit" name="provider" value="<%#: Item.ProviderName %>" class="btn btn-inverse btn-<%#: Item.ProviderName %>"
                title="Войдите, используя свой <%#: Item.ProviderDisplayName %> аккаунт.">
                <%#: Item.ProviderDisplayName %>
            </button>
        </ItemTemplate>
    
        <EmptyDataTemplate>
            <div class="message-info">
                <p>Нет доступных внешних провайдеров.</p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</fieldset>