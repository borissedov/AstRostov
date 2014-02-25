<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="AddSku.aspx.cs" Inherits="AstRostov.Admin.AddSku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnProductId" />
    <asp:HiddenField runat="server" ID="hdnAttributeDictionary" ClientIDMode="Static" />
    <%--    <input type="hidden" id="hdnAttributeDictionary" value="<%=ViewState["hdnAttributeDictionary"] %>"/>--%>
    <asp:HiddenField runat="server" ID="hdnAttributeDictionaryToSave" ClientIDMode="Static" />
    <h2>Добавление конфигурации продукта</h2>
    <br />
    <h3>Атрибуты и значения</h3>
    <div class="form-horizontal">
        <asp:Repeater runat="server" ID="rptAttributes" OnItemDataBound="OnAttributeItemDataBound">
            <ItemTemplate>
                <div class="control-group">
                    <asp:Label runat="server" ID="lblAttributeTitle" CssClass="control-label"></asp:Label>
                    <div class="controls">
                        <ucc:AutocompleteTextbox runat="server" ID="tbAttributeValue"></ucc:AutocompleteTextbox>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <hr />
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Количество на складе</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbInventory" TextMode="Number" CssClass="product-qty"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Переопределенная цена</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbRetailPrice"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="control-group">
            <span class="control-label">Переопределенная цена со скидкой</span>
            <div class="controls">
                <asp:TextBox runat="server" ID="tbSalePrice"></asp:TextBox>
            </div>
        </div>
    </div>
    <script>
        //$(document).ready(function () {
        //    $('#btnAddAttribute').click(function () {
        //        AddAttribute();
        //    });
        //});

        //function AddAttribute() {
        //    $('.add-attr').before($('#templateHolder').html());
        //    var attrNames = $('.attr-name').val();

        //    var availableNames = Object.keys(JSON.parse($('#hdnAttributeDictionary').val())).filter(function (value, index, ar) {
        //        return attrNames.indexOf(value) == -1;
        //    });

        //    var lastAttrNameInput = $('.form-horizontal').find('.attr-name:last');
        //    //lastAttrNameInput.attr('data-source', JSON.stringify(availableNames));
        //    lastAttrNameInput.typeahead({ source: availableNames });

        //    // setTimeout($('#btnAddAttribute').click(AddAttribute()), 400);

        //    return false;
        //}

        //function FormatAttributeDictionary() {
        //    var isValid = true;

        //    //Attribute dictionary formatting
        //    var dictionary = new Object();
        //    try {
        //        $('.form-horizontal').children('.attr-config').each(function () {
        //            var attrName = $(this).find('.attr-name').val().trim();
        //            var attrVal = $(this).find('.attr-value').val().trim();
        //            var attrDefVal = $(this).find('.attr-default-value').val().trim();
        //            if (attrName == '' || attrVal == '') {
        //                throw ('');
        //            }
        //            if (Object.keys(dictionary).indexOf(attrName) == -1) {
        //                dictionary[attrName] =
        //                {
        //                    Value: attrVal,
        //                    Default: attrDefVal
        //                };
        //            } else {
        //                isValid = false;
        //            }
        //        });

        //        if (isValid) {
        //            //$('#hdnAttributeDictionaryToSave').val(Sys.Serialization.JavaScriptSerializer.serialize(dictionary));
        //            $('#hdnAttributeDictionaryToSave').val(JSON.stringify(dictionary));
        //            return true;
        //        } else {
        //            $('#lblError').html('Проверьте правильность заполнения атрибутов и значений');
        //            return false;
        //        }
        //    }
        //    catch (e) {
        //        $('#lblError').html('Проверьте правильность заполнения атрибутов и значений');
        //        return false;
        //    }
        //    return false;
        //}

        //function toggleDefaultValue(sender) {
        //    setTimeout(function() {
        //        var attrName = $(sender).val();
        //        //var defaultValInput = $(sender).parent('div').find('.attr-default-value');
        //        var valInput = $(sender).parent('div').find('.attr-value');

        //        var attrAvailNames = Object.keys(JSON.parse($('#hdnAttributeDictionary').val()));

        //        if (attrAvailNames.indexOf(attrName) != -1) {
        //            //defaultValInput.hide();
        //            //defaultValInput.val('');

        //            //valInput.attr('data-source', JSON.stringify(JSON.parse($('#hdnAttributeDictionary').val())[attrName]));
        //            valInput.typeahead({ source: JSON.parse($('#hdnAttributeDictionary').val())[attrName] });
        //        } else {
        //            //$(sender).parent('div').find('.attr-default-value').show();

        //            valInput.attr('data-source', '');
        //            valInput.typeahead();
        //        }
        //    }, 500);
        //}

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

    <asp:Label runat="server" CssClass="text-error" ID="lblError" ClientIDMode="Static"></asp:Label><br />
    <asp:HyperLink runat="server" ID="hlBack" CssClass="btn" Text="Назад к продукту"></asp:HyperLink>
    <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-success" Text="Сохранить" OnClick="SaveSku" />
</asp:Content>
