<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBlog.aspx.cs" Inherits="AstRostov.CreateBlogPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Создание блога</h2>
    <article>
        У вас еще нет бортжурнал.
        Вы можете создать его для того чтобы поведать миру об истории тюнинга своего автомобиля.
        Для этого заполните информацию о вашем авто.
    </article>
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Марка авто</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbBrand"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="help-inline" ControlToValidate="tbBrand" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Модель</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbModel"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="help-inline" ControlToValidate="tbModel" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Год выпуска</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbManufacturedYear"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="help-inline" ControlToValidate="tbManufacturedYear" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Год покупки</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbBuyYear"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="help-inline" ControlToValidate="tbBuyYear" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Цвет</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbColor"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="help-inline" ControlToValidate="tbColor" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Комментарий к модели</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbModelComment"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Я езжу на этой машине</span>
            <div class="controls">
                <asp:RadioButtonList runat="server" ID="rblDriving">
                    <Items>
                        <asp:ListItem Text="Да" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Нет" Value="0"></asp:ListItem>
                    </Items>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Я продаю эту машину</span>
            <div class="controls">
                <asp:RadioButtonList runat="server" ID="rblSell">
                    <Items>
                        <asp:ListItem Text="Да" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Нет" Value="0" Selected="True"></asp:ListItem>
                    </Items>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Мощность</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbPower"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Объем двигателя</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbEngineVolume"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Госномер</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbGosNumber"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">VIN-код</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbVin"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:Button runat="server" ID="SaveMainSliderItemButton" Text="Создать бортжурнал" CssClass="btn" OnClick="CreateBlog" />
            </div>
        </div>
    </div>
</asp:Content>
