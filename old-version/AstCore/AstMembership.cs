using System;
using System.Linq;
using AstCore.Models;


namespace AstCore
{
    public class AstMembership
    {
        public static User CurrentUser
        {
            get
            {
                var aspUser = System.Web.Security.Membership.GetUser();
                if (aspUser == null)
                {
                    return null;
                }
                return DataAccess.CoreData.Context.Users.Include("Membership").Include("Address").SingleOrDefault(u => u.UserId == (Guid)aspUser.ProviderUserKey);
            }
        }
    }
}
