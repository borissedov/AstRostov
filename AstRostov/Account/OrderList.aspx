<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Simple.Master" CodeBehind="OrderList.aspx.cs" Inherits="AstRostov.Account.Orders" %>
<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Список заказов</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список заказов</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridOrders" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowDataBound="RowDataBound" OnRowCommand="RowCommand">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="OrderId" />
            <asp:BoundField HeaderText="Сумма" DataField="Total" />
            <asp:BoundField HeaderText="Дата" DataField="CreateDate" />
            <asp:BoundField HeaderText="Комментарий администратора" DataField="AdminComment" />
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
                    <asp:HyperLink runat="server" CssClass="btn btn-small btn-inverse" ID="hlEditOrder" ToolTip="Детали" Text="Детали"  NavigateUrl='<%#ResolveUrl(String.Format("~/Account/EditOrder.aspx?id={0}", Eval("OrderId"))) %>'></asp:HyperLink>
                    <asp:Button runat="server" CssClass="btn btn-small btn-inverse" ID="btnPay" ToolTip="Провести оплату" Text="Провести оплату" Visible="False" CommandName="Pay" CommandArgument='<%#Eval("OrderId") %>' ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
