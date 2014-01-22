<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="HowToPay.aspx.cs" Inherits="AstRostov.HowToPay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Способы оплаты заказа</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function printframe() {
            window.frames['check'].print();
            return false;
        }
    </script>
    <h2>Способы оплаты заказа</h2>
    <div class="row-fluid">
        <div class="span12">
            <p>Вы можете оплатить заказ в отделении банка, распечатав квитанцию.</p>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <iframe id="check" src='<%=ResolveUrl(String.Format("~/Check.aspx?id={0}", Request.Params["id"])) %>' width="750" height="680"></iframe>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <a href="#" onclick="return printframe();" class="btn btn-inverse">
                <i class="icon-white icon-print"></i>
                Печать
            </a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <p>Также вы можете провести оплату переводом на одну из наших карт:</p>
            <dl>
                <dt>Сбербанк</dt>
                <dd>4111-1111-1111-1111</dd>
                <dt>Альфа-Банк</dt>
                <dd>4111-1111-1111-1111</dd>
            </dl>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <h4>После оплаты обязательно свяжитесь с нами!</h4>
            
            <p>Вы можете сообщить нам об оплате, позвонив нам по номеру, указанному в <a href='<%=ResolveUrl("~/Contact.aspx") %>'>контактах</a></p>
            <p>Или через форму просмотра сведений о заказе (<a href='<%=ResolveUrl(String.Format("~/Account/EditOrder.aspx?id={0}", Request.Params["id"])) %>'>перейти к форме</a>)</p>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12"></div>
    </div>


</asp:Content>
