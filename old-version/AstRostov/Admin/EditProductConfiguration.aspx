<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="EditProductConfiguration.aspx.cs" Inherits="AstRostov.Admin.EditProductConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnProductId" />
    <asp:HiddenField runat="server" ID="hdnAttributeDictionary" ClientIDMode="Static" />
    <h2>Редактирование схемы конфигураций продукта</h2>
    <br />
    <span class="info">Заполните схему указав названия атрибутов продукта и значения для формирования "конфигурации по умолчанию"</span>
    <div class="form-horizontal">
        <asp:Repeater runat="server" ID="rptAttributes">
            <ItemTemplate>
                <div class="control-group">
                    <asp:TextBox runat="server" CssClass="control-label attr-name" ID="tbAttributeName" placeholder="Название атрибута" Text='<%#Eval("Name") %>' onchange="SetValAutocomplete(this)"></asp:TextBox>
                    <div class="controls">
                        <asp:TextBox runat="server" CssClass="attr-value" ID="tbAttributeDefaultValue" placeholder="Значение атрибута по умолчанию" ></asp:TextBox>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Label runat="server" CssClass="text-error" ID="lblError" ClientIDMode="Static"></asp:Label>
    <br />
    <span class="warning">Внимание! Сохранение схемы конфигураций удалит все существующие конфигурации продукта и создаст конфигурацию "по умолчанию" с заданными значаниями атрибутов.</span>
    <br />
    <asp:HyperLink runat="server" ID="hlBack" CssClass="btn" Text="Назад к продукту"></asp:HyperLink>
    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success" Text="Сохранить" OnClick="SaveConfiguration" />
    <script>
        $(document).ready(function () {
            var availableNames = Object.keys(JSON.parse($('#hdnAttributeDictionary').val()));
            var lastAttrNameInput = $('.form-horizontal').find('.attr-name');
            lastAttrNameInput.typeahead({ source: availableNames });
            $('.attr-name').each(function () { SetValAutocomplete(this); });
        });
        
        function SetValAutocomplete(sender) {
            setTimeout(function() {
                var attrName = $(sender).val();
                //var defaultValInput = $(sender).parent('div').find('.attr-default-value');
                var valInput = $(sender).parent('div').find('.attr-value');

                var attrAvailNames = Object.keys(JSON.parse($('#hdnAttributeDictionary').val()));

                if (attrAvailNames.indexOf(attrName) != -1) {
                    //defaultValInput.hide();
                    //defaultValInput.val('');

                    //valInput.attr('data-source', JSON.stringify(JSON.parse($('#hdnAttributeDictionary').val())[attrName]));
                    valInput.typeahead({ source: JSON.parse($('#hdnAttributeDictionary').val())[attrName] });
                } else {
                    //$(sender).parent('div').find('.attr-default-value').show();

                    valInput.attr('data-source', '');
                    valInput.typeahead();
                }
            }, 500);
        }
    </script>
</asp:Content>
