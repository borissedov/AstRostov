<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditPost.aspx.cs" Inherits="AstRostov.EditPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Редактирование записи в бортжурнале</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnBlogId" />
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>
        <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Название записи</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbTitle"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label">Содержание</span>
            <div class="controls">
                <ucc:RichTextBox runat="server" ID="tbContent"></ucc:RichTextBox>
                <span class="help-inline pull-right">
                    При написании статьи вы можете отделить область, которая будет выводиться как краткое содержание в списке постов вашего блога. 
                    Для этого в конце краткого содержания используйте двойной перевод на новую строку и продолжите писать статью дальше. 
                    В противном случае, краткое содержание поста будет сформировано автоматически. 
                    Максимальная длина краткого содержания 1000 символов.
                </span>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-inverse" Text="Отмена" OnClick="BackToBlog"></asp:Button>
                <asp:Button runat="server" ID="SaveMainSliderItemButton" Text="Сохранить" CssClass="btn btn-inverse" OnClick="SavePost" />
            </div>
        </div>
    </div>


</asp:Content>
