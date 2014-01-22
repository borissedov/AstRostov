<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditNewsItem.aspx.cs" Inherits="AstRostov.Admin.EditNewsItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="updEditNewsItem">
        <Triggers>
            <asp:PostBackTrigger ControlID="SaveNewsItemButton" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnItemId" />
            <h2>
                <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>

            <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
            <asp:Panel runat="server" ID="pnlNewsItem" Visible="False">

                <div class="form-horizontal">
                    <div class="control-group">
                        <span class="control-label">Заглавие новости</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbNewsItemTitle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label">Тело новости</span>
                        <div class="controls">
                            <ucc:RichTextBox runat="server" ID="tbNewsItemContent"></ucc:RichTextBox>
                            <span class="help-inline pull-right">При написании статьи вы можете отделить область, которая будет выводиться как краткое содержание в списках новостей. 
                                Для этого в конце краткого содержания используйте двойной перевод на новую строку и продолжите писать статью дальше. 
                                В противном случае, краткое содержание новости будет сформировано автоматически. 
                                Максимальная длина краткого содержания 1000 символов.
                            </span>
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                            <asp:Button runat="server" ID="btnGoToComments" Text="Список комментариев" CssClass="btn" OnClick="GoToCommentsList" />
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                            <asp:HyperLink runat="server" NavigateUrl="~/Admin/NewsList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                            <asp:Button runat="server" ID="SaveNewsItemButton" Text="Сохранить" CssClass="btn" OnClick="SaveNewsItem" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <%--<asp:Panel runat="server" ID="pnlFeaturedItem" Visible="False">
                <div class="form-horizontal">
                    <div class="control-group">
                        <span class="control-label">Название продукта</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbFeaturedItemName"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label">Цена</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbFeaturedItemPrice"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label">Ссылка</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbFeaturedItemUrl"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label">Описание</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbFeaturedItemDescription"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label"></span>
                        <div class="controls">
                            <asp:RadioButtonList runat="server" ID="rblImageMode" AutoPostBack="True" OnSelectedIndexChanged="rblImageMode_OnSelectedIndexChanged" CssClass="radio">
                                <Items>
                                    <asp:ListItem Text="Оставить текущее изображение"></asp:ListItem>
                                    <asp:ListItem Text="Загрузить новое изображние"></asp:ListItem>
                                    <asp:ListItem Text="Без изображения"></asp:ListItem>
                                </Items>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label"></span>
                        <div class="controls">
                            <asp:Image runat="server" ID="imgFeaturedItemImage" />
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label"></span>
                        <div class="controls">
                            <asp:FileUpload ID="ImageUploadControl" runat="server" CssClass="file-upload" />
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label"></span>
                        <div class="controls">
                            <asp:HyperLink runat="server" NavigateUrl="~/Admin/FeaturedItemList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                            <asp:Button runat="server" ID="SaveFeaturedItemButton" Text="Сохранить" CssClass="btn" OnClick="SaveFeaturedItem" />
                        </div>
                    </div>
                </div>
            </asp:Panel>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
