using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AstCore.CommonControls
{
    public class Alert : WebControl
    {
        protected string Text
        {
            get;
            set;
        }

        public AlertMode Mode
        {
            get;
            set;
        }

        public Alert()
            : base(HtmlTextWriterTag.Div)
        {
            Mode = AlertMode.Default;
            CssClass = ClassName;
            Controls.Add(new Literal { Text = Text });
            Visible = !String.IsNullOrEmpty(Text);
        }


        private string ClassName
        {
            get
            {
                switch (Mode)
                {
                    case AlertMode.Default:
                        return "alert";
                    case AlertMode.Info:
                        return "alert alert-info";
                    case AlertMode.Success:
                        return "alert alert-success";
                    case AlertMode.Error:
                        return "alert alert-error";
                    default:
                        return "alert";
                }
            }
        }
    }

    public enum AlertMode
    {
        Default,
        Success,
        Info,
        Error
    }
}
