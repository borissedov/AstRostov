<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditMainSliderItem.aspx.cs" Inherits="AstRostov.Admin.EditMainSliderItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="updEditMainSliderItem">
        <Triggers>
            <asp:PostBackTrigger ControlID="SaveMainSliderItemButton" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnItemId" />
            <h2>
                <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>

            <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
            
                <div class="form-horizontal">
                    <div class="control-group">
                        <span class="control-label">Название продукта</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbMainSliderItemTitle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label">Цена</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbMainSliderItemPrice" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="control-group">
                        <span class="control-label">Ссылка на продукт</span>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="tbMainSliderItemUrl" ></asp:TextBox>
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
                            <asp:Image runat="server" ID="imgMainSliderItemImage" />
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
                            <asp:HyperLink runat="server" NavigateUrl="~/Admin/MainSliderItemList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                             <asp:Button runat="server" ID="SaveMainSliderItemButton" Text="Сохранить" CssClass="btn" OnClick="SaveMainSliderItem" />
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
