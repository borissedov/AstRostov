using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;
using Microsoft.AspNet.Membership.OpenAuth;

namespace AstRostov.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                // Determine the sections to render
                var hasLocalPassword = OpenAuth.HasLocalPassword(User.Identity.Name);
                setPassword.Visible = !hasLocalPassword;
                changePassword.Visible = hasLocalPassword;

                CanRemoveExternalLogins = hasLocalPassword;

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage.aspx");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Ваш пароль был изменен."
                        : message == "SetPwdSuccess" ? "Ваш пароль был установлен."
                        : message == "RemoveLoginSuccess" ? "Логин через внешний сервис удален."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }

                BindAddressForm();

                imgAvatar.ImageUrl = String.IsNullOrEmpty(AstMembership.CurrentUser.AvatarFile)
                    ? ResolveUrl("~/img/default_user.png")
                    : ResolveUrl(AstMembership.CurrentUser.AvatarFile);
            }

        }


        protected void setPassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var result = OpenAuth.AddLocalPassword(User.Identity.Name, password.Text);
                if (result.IsSuccessful)
                {
                    Response.Redirect("~/Account/Manage.aspx?m=SetPwdSuccess");
                }
                else
                {

                    ModelState.AddModelError("NewPassword", result.ErrorMessage);

                }
            }
        }


        public IEnumerable<OpenAuthAccountData> GetExternalLogins()
        {
            var accounts = OpenAuth.GetAccountsForUser(User.Identity.Name);
            CanRemoveExternalLogins = CanRemoveExternalLogins || accounts.Count() > 1;
            return accounts;
        }

        public void RemoveExternalLogin(string providerName, string providerUserId)
        {
            var m = OpenAuth.DeleteAccount(User.Identity.Name, providerName, providerUserId)
                ? "?m=RemoveLoginSuccess"
                : String.Empty;
            Response.Redirect("~/Account/Manage.aspx" + m);
        }


        protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
        {
            // You can change this method to convert the UTC date time into the desired display
            // offset and format. Here we're converting it to the server timezone and formatting
            // as a short date and a long time string, using the current thread culture.
            return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[никогда]";
        }


        private void BindAddressForm()
        {
            if (AstMembership.CurrentUser != null && AstMembership.CurrentUser.Address != null)
            {
                var address = AstMembership.CurrentUser.Address;

                tbFullName.Text = address.FullName;
                tbEmail.Text = address.Email;
                tbPhone.Text = address.Phone;
                tbRegion.Text = address.Region;
                tbCity.Text = address.City;
                tbAddress1.Text = address.Address1;
                tbAddress2.Text = address.Address2;
                tbZipCode.Text = address.ZipCode;
                tbRegion.Text = address.Region;
                tbDocumentNumber.Text = address.DocumentNumber;

                if (ddlCountry.Items.Cast<ListItem>().Any(l => l.Value == address.Country))
                {
                    ddlCountry.SelectedValue = address.Country;
                }

                if (ddlDocumentType.Items.Cast<ListItem>().Any(l => l.Value == address.DocumentType))
                {
                    ddlDocumentType.SelectedValue = address.DocumentType;
                }
            }
        }

        protected void SaveAddress(object sender, EventArgs e)
        {
            if (AstMembership.CurrentUser != null)
            {
                var address = AstMembership.CurrentUser.Address ?? new Address();

                address.FullName = tbFullName.Text;
                address.Email = tbEmail.Text;
                address.Phone = tbPhone.Text;
                address.Region = tbRegion.Text;
                address.City = tbCity.Text;
                address.Address1 = tbAddress1.Text;
                address.Address2 = tbAddress2.Text;
                address.ZipCode = tbZipCode.Text;
                address.Region = tbRegion.Text;
                address.Country = ddlCountry.SelectedValue;
                address.DocumentType = ddlDocumentType.SelectedValue;
                address.DocumentNumber = tbDocumentNumber.Text;

                if (AstMembership.CurrentUser.Address == null)
                {
                    AstMembership.CurrentUser.Address = address;
                }

                CoreData.Context.SaveChanges();

                lblAddressSavedSuccess.Visible = true;
            }
        }

        protected void UploadNewAvatar(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var fullName = String.Format("~/img/avatars/{0}", fuAvatar.PostedFile.FileName);

                var imageData = new byte[fuAvatar.PostedFile.InputStream.Length];
                fuAvatar.PostedFile.InputStream.Read(imageData, 0, (int) fuAvatar.PostedFile.InputStream.Length);
                FileHelper.WriteBinaryStorage(imageData, fullName);

                AstMembership.CurrentUser.AvatarFile = fullName;
                CoreData.Context.SaveChanges();
            }

            Response.Redirect("~/Account/Manage.aspx");
        }

    }
}