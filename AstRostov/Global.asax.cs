using System;
using System.Data.Entity.Validation;
using System.Text;
using System.Web;
using System.Web.Routing;
using AstCore.DataAccess;
using AstECommerce;
using AstRostov.App_Start;
using NLog;

namespace AstRostov
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            ShoppingCartRepository.ClearRepository();
            // Code that runs on application startup
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            ShoppingCartRepository.ClearRepository();
        }

        protected virtual void Application_BeginRequest()
        {
            CoreData.Context = new AstEntities();
        }

        protected virtual void Application_EndRequest()
        {
            if (CoreData.Context != null)
            {
                CoreData.Context.Dispose();
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception lastException = Server.GetLastError();
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Fatal(lastException);
            logger.ErrorException("This is an error with an Exception", lastException);
            var dbEntityValidationException = lastException.InnerException as DbEntityValidationException;
            if (dbEntityValidationException != null)
            {
                var sb = new StringBuilder();

                foreach (var failure in dbEntityValidationException.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                logger.Fatal(sb);
            }
        }

        void Session_End(object sender, EventArgs e)
        {
            ShoppingCartRepository.DisposeState();
        }
    }
}
