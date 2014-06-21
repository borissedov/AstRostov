<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditBlog.aspx.cs" Inherits="AstRostov.Admin.EditBlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId"/>
    <h2>Редактирование бортжурнала <%=Blog.BlogId %></h2>
    <div class="form-horizontal">
        
        <div class="control-group">
            <span class="control-label">Название бортжурнала</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbTitle"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="help-inline" ControlToValidate="tbTitle" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Автор</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbAuthor" Enabled="False"></asp:TextBox>
            </div>
        </div>
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
                <asp:TextBox runat="server" ID="tbManufacturedYear" ></asp:TextBox>
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
                <asp:Button runat="server" ID="btnSaveBlog" Text="Сохранить" CssClass="btn btn-success" OnClick="SaveBlog" />
            </div>
        </div>
    </div>
    <hr/>
    <h3>Записи</h3>
    <asp:GridView runat="server" ID="gridPosts" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="PostId" />
            <asp:BoundField HeaderText="Заглавие" DataField="Title" />
            <asp:TemplateField HeaderText="Содержание">
                <ItemTemplate>
                    <%# ((string)Eval("Content")).Length > 500 ? string.Format("{0}...", ((string)Eval("Content")).Substring(0, 500)) : Eval("Content") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Создана" DataField="Created" />
            <asp:BoundField HeaderText="Редактирована" DataField="Updated" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%--<asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("PostId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                    </asp:LinkButton>--%>
                    <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("PostId") %>' ToolTip="Удалить">
                                <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
