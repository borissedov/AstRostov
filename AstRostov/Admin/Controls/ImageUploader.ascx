<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="AstRostov.Admin.Controls.AdminImageUploader" CodeBehind="ImageUploader.ascx.cs" %>

<script type="text/javascript">
    function setEnabled() {
        document.getElementById('<%=txtFileName.ClientID %>').disabled = !document.getElementById('<%=rdoNewFileName.ClientID %>').checked;
        }
</script>
<span>
    <asp:FileUpload ID="UploadImage" runat="server" EnableViewState="true" />
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UploadImage"
        CssClass="text-error" Display="dynamic" ErrorMessage="Выберите корректное JPEG, JPG, PNG или GIF изображение"
        ValidationExpression=".*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])"></asp:RegularExpressionValidator>
</span>
<asp:Panel ID="pnlSaveOption" runat="server" Visible="false" Style="width: 300px; border: solid 0px gray; padding: 5px;">
    <div class="Error">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    Выберите опцию:
        <br />
    <asp:RadioButton ID="rdoOverwrite" Text="Перезаписать текущий файл" runat="server"
        GroupName="SaveOption" />
    <br />
    <asp:RadioButton ID="rdoNewFileName" Checked="true" runat="server" GroupName="SaveOption"
        Text="Испоьлзовать имя файла" />
    <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
    <br />
    <asp:RegularExpressionValidator ID="revFilename" runat="server" ControlToValidate="txtFileName"
        CssClass="Error" Display="dynamic" ErrorMessage="Введите корректное имя файла без расширения."
        ValidationExpression=".*([a-zA-Z_0-9 ])"></asp:RegularExpressionValidator>
</asp:Panel>

