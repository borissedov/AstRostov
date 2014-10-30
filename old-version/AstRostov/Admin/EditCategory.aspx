<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="AstRostov.Admin.EditCategory" %>

<%@ Register TagPrefix="ast" TagName="AdminImageUploader" Src="~/Admin/Controls/ImageUploader.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <h2>
        <asp:Literal runat="server" ID="litEditTitle"></asp:Literal></h2>

    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />

    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Название категории</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbCategoryName" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" CssClass="text-error" ErrorMessage="*" ControlToValidate="tbCategoryName"></asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:PlaceHolder runat="server" ID="phImageUploader">
            <div class="control-group">
                <span class="control-label">Изображение</span>
                <div class="controls">
                    <asp:Image runat="server" ID="imgCategoryImage"></asp:Image>
                    <ast:AdminImageUploader runat="server" ID="imageUploader"></ast:AdminImageUploader>
                    <br />
                    <asp:Button runat="server" ID="btnUploadImage" Text="Загрузить изображение" CssClass="btn" OnClick="UploadImage" CausesValidation="False" />
                </div>
            </div>
        </asp:PlaceHolder>
        <div class="control-group">
            <span class="control-label">Описание категории</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbDescription" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <span class="control-label"></span>
            <div class="controls">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/CategoryList.aspx" CssClass="btn" Text="Назад к списку"></asp:HyperLink>
                <asp:Button runat="server" ID="btnSaveCategory" Text="Сохранить" CssClass="btn" OnClick="SaveCategory" />
            </div>
        </div>
    </div>
    <asp:PlaceHolder ID="phEditBindings" runat="server">
        <h3>Родительские категории</h3>
        <asp:GridView runat="server" ID="gridParentCategories" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="CategoryId" />
                <asp:BoundField HeaderText="Название" DataField="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Edit" CommandArgument='<%#Eval("CategoryId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="Delete" CommandArgument='<%#Eval("CategoryId") %>' ToolTip="Отвязать">
                                <i class="icon-white icon-trash"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="phAddParent" runat="server" CssClass="form-horizontal" DefaultButton="btnBindParent">
            <div class="control-group">
                <span class="control-label">Выберите родительскую категорию</span>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="ddlSelectParent" />
                </div>
            </div>
            <div class="control-group">
                <span class="control-label"></span>
                <div class="controls">
                    <asp:Button runat="server" ID="btnBindParent" CssClass="btn" Text="Привязать" OnClick="BindParentCategory" />
                </div>
            </div>
        </asp:Panel>
        
        <asp:PlaceHolder runat="server" ID="phChildCategories" Visible="False">
        <h3>Дочерние категории</h3>
        <asp:GridView runat="server" ID="gridChildCategories" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="CategoryId" />
                <asp:BoundField HeaderText="Название" DataField="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("CategoryId") %>' ToolTip="Редактировать">
                                <i class="icon-white icon-edit"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CategoryId") %>' ToolTip="Отвязать">
                                <i class="icon-white icon-trash"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Panel ID="phAddChild" runat="server" CssClass="form-horizontal" DefaultButton="btnBindChild">
            <div class="control-group">
                <span class="control-label">Выберите дочернюю категорию</span>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="ddlSelectChild" />
                </div>
            </div>
            <div class="control-group">
                <span class="control-label"></span>
                <div class="controls">
                    <asp:Button runat="server" ID="btnBindChild" CssClass="btn" Text="Привязать" OnClick="BindChildCategory" />
                </div>
            </div>
        </asp:Panel>
            </asp:PlaceHolder>
    </asp:PlaceHolder>



</asp:Content>
