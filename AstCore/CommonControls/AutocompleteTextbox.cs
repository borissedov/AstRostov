using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AstCore.CommonControls
{
    public class AutocompleteTextbox : TextBox
    {
        public IEnumerable<string> DataSource { private get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            Attributes["data-provide"] = "typeahead";
            Attributes["data-source"] = String.Format("[\"{0}\"]", String.Join("\",\"", DataSource));
            base.Render(writer);
        }
    }
}