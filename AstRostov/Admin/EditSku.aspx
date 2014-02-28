<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSku.aspx.cs" MasterPageFile="~/Simple.Master" Inherits="AstRostov.Admin.EditSku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnSkuId" />
    <h2>Редактирование конфигурации продукта</h2>
    <br />
    <h3>Атрибуты и значения</h3>
    <div class="form-horizontal">
        <asp:GridView runat="server" ID="gridAttributes" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Атрибут">
                    <ItemTemplate>
                        <%# ((AstCore.Models.Attribute)Eval("Attribute")).Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Значение" DataField="Value" />
            </Columns>
        </asp:GridView>
    </div>
    <hr />
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Артикул Конфигурации</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbSkuNumber"></asp:TextBox>
            </div>
        </div>
        <div class="control-group attr-config">
            <span class="control-label">Количество на складе</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbInventory" TextMode="Number" CssClass="product-qty"></asp:TextBox>
            </div>
        </div>
        <div class="control-group attr-config">
            <span class="control-label">Переопределенная цена</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbRetailPrice"></asp:TextBox>
            </div>
        </div>
        <div class="control-group attr-config">
            <span class="control-label">Переопределенная цена со скидкой</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbSalePrice"></asp:TextBox>
            </div>
        </div>
    </div>

    <asp:Label runat="server" CssClass="text-error" ID="lblError" ClientIDMode="Static"></asp:Label>
    <br/>
    <asp:HyperLink runat="server" ID="hlBack" CssClass="btn" Text="Назад к продукту"></asp:HyperLink>
    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Сохранить" OnClick="SaveSku" />
    
    <script>
        $(document).ready(function () {
            $('.product-qty').change(function () {
                if ($(this).val() > 10) {
                    $(this).val(10);
                }
                if ($(this).val() < 0) {
                    $(this).val(0);
                }
            });
        });
    </script>
</asp:Content>
