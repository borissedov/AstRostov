using System.Configuration;
using Authentication;
using Microsoft.AspNet.Membership.OpenAuth;

namespace AstRostov.App_Start
{
    internal static class AuthConfig
    {
        public static void RegisterOpenAuth()
        {
            // See http://go.microsoft.com/fwlink/?LinkId=252803 for details on setting up this ASP.NET
            // application to support logging in via external services.

            //OpenAuth.AuthenticationClients.AddTwitter(
            //    consumerKey: "your Twitter consumer key",
            //    consumerSecret: "your Twitter consumer secret");

            OpenAuth.AuthenticationClients.AddFacebook(ConfigurationManager.AppSettings["fbAppId"], ConfigurationManager.AppSettings["fbAppSecret"]);
            
            //OpenAuth.AuthenticationClients.AddMicrosoft(
            //    clientId: "your Microsoft account client id",
            //    clientSecret: "your Microsoft account client secret");

            //OpenAuth.AuthenticationClients.AddGoogle();
            
            //OAuthWebSecurity2.RegisterVkontakteClient(appId: "3815761", appSecret: "2ZUmvRslblqj6JvF3qEx");
            OpenAuth.AuthenticationClients.Add("ВКонтакте", () => new VkClient(ConfigurationManager.AppSettings["vkAppId"], ConfigurationManager.AppSettings["vkAppSecret"]));
            OpenAuth.AuthenticationClients.Add("Одноклассники", () => new OkClient(ConfigurationManager.AppSettings["okAppId"], ConfigurationManager.AppSettings["okAppSecret"], ConfigurationManager.AppSettings["okAppPublic"]));
        }
    }
}