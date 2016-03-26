using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebStore.DataLayer;
using WebStore.WebUI.Models.Binds;

namespace WebStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Database.SetInitializer(new DropCreateDatabaseAlways<WebStoreDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WebStoreDbContext>());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
        }
    }
}
