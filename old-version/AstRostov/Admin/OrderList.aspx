<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="OrderList.aspx.cs" Inherits="AstRostov.Admin.Orders" %>
<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список заказов</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridOrders" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="OrderId" />
            <asp:BoundField HeaderText="Заказчик" DataField="Account" />
            <asp:BoundField HeaderText="Сумма" DataField="Total" />
            <asp:BoundField HeaderText="Дата" DataField="CreateDate" />
            <asp:TemplateField HeaderText="Способ оплаты">
                <ItemTemplate>
                    <%# ((PaymentMethod)Eval("PaymentMethod")).GetDescription() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Доставка">
                <ItemTemplate>
                    <%# ((ShippingType)Eval("ShippingType")).GetDescription() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Статус">
                <ItemTemplate>
                    <%# ((OrderState)Eval("OrderState")).GetDescription() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a title="Просмотр/Редактирование" href='<%#ResolveUrl(String.Format("~/Admin/EditOrder.aspx?id={0}", Eval("OrderId"))) %>'>
                        <i class="icon-white icon-edit"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
