using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebStore.DataLayer;
using WebStore.DataLayer.Migrations;
using WebStore.WebUI.Infrastructure.Binders;
using WebStore.Domain.Entities;

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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebStoreDbContext, Configuration>());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
