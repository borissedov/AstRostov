<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="PreorderList.aspx.cs" Inherits="AstRostov.Admin.PreorderList" %>

<%@ Import Namespace="AstCore.Helpers" %>
<%@ Import Namespace="AstCore.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Администрирование</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список предзаказов</h2>
    <asp:Label runat="server" ID="ErrorLabel" Visible="False" />
    <asp:GridView runat="server" ID="gridPreorders" OnRowCommand="OnGridRowCommand" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowDataBound="OnGridRowDataBound">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="PreorderId" />
            <asp:BoundField HeaderText="Заказчик" DataField="CustomerName" />
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <a href='mailto:<%# Eval("CustomerEmail") %>' title="Отправить сообщение"><%# Eval("CustomerEmail") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Телефон" DataField="Phone" />
            <asp:TemplateField HeaderText="Продукт">
                <ItemTemplate>
                    <a href='<%#ResolveUrl(String.Format("~/Product.aspx?id={0}", GetProductIdBySkuId((int)Eval("SkuId")))) %>'>
                        <%# GetProductNameBySkuId((int)Eval("SkuId")) %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Конфигурация">
                <ItemTemplate>
                    <%# GetAttributeConfigBySkuId((int)Eval("SkuId")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Количество" DataField="Count" />
            <asp:BoundField HeaderText="Дата" DataField="Date" />
            <asp:TemplateField HeaderText="Статус">
                <ItemTemplate>
                    <%# ((PreorderState)Eval("State")).GetDescription() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Decline" CommandArgument='<%#Eval("PreorderId") %>' ToolTip="Отменить">
                        <i class="icon-white icon-trash"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbtnConvertToOrder" CommandName="ConvertToOrder" CommandArgument='<%#Eval("PreorderId") %>' ToolTip="Конвертировать в заказ">
                        <i class="icon-white icon-shopping-cart"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
