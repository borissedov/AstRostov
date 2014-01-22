using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AstCore.CommonControls
{
    public class RichTextBox: HtmlTextArea
    {
        public String Text
        {
            get { return Value; }
            set { Value = value; }
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            this.Attributes["class"] = "wysihtml5";
            base.Render(writer);
            //this.Page.ClientScript.RegisterStartupScript(, String.Format("{0}-init-rich-text-box", this.ClientID), String.Format("$(document).ready(function() {{ $('#{0}').wysihtml5(); }});", this.ClientID));
        }
    }
}
