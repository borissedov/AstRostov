using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;
using AstECommerce;

namespace AstRostov
{
    public partial class AddressPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindAddressForm();
            }
        }

        private void BindAddressForm()
        {
            var account = AstMembership.CurrentUser;
            if (account == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            Address address = account.Address;
            if (address != null)
            {
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
            else
            {
                tbEmail.Text = account.Membership.Email;
            }
        }

        protected void NextStepCheckout(object sender, EventArgs e)
        {
            if (AstMembership.CurrentUser == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

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

            Checkout.SetAddress(address);

            Response.Redirect("~/Checkout.aspx");
        }
    }
}