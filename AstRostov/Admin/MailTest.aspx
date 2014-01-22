<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailTest.aspx.cs" Inherits="AstRostov.Admin.MailTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="tbTo"></asp:TextBox>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="tbBody"></asp:TextBox>
        <asp:Button runat="server" Text="Send" OnClick="SendMail"/>
    </div>
    </form>
</body>
</html>
