using System.Web;

namespace AstCore.DataAccess
{
    public static class CoreData
    {
        public static AstEntities Context
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["AstCoreEntities"] as AstEntities ?? new AstEntities();
                }
                return new AstEntities();
                //var context = HttpContext.Current.Items["AstCoreEntities"] as AstEntities;
                //if (context == null)
                //{
                //    context = new AstEntities();
                //    HttpContext.Current.Items["AstCoreEntities"] = context;
                //}
                //return context;
            }
            set
            {
                HttpContext.Current.Items["AstCoreEntities"] = value;
            }
        }
    }
}
