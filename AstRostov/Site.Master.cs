﻿using System;

namespace AstRostov
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ClearSession(object sender, EventArgs e)
        {
            Session.Abandon();
        }
    }
}