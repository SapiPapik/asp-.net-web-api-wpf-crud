using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ProductCatalog.BLL.Services.Mappings;
using ProductCatalog.Web.App_Start;
using ProductCatalog.Web.Mappings;
using ProductCatalog.DAL.Migrations;

namespace ProductCatalog.Web {
    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            UpdateDatabase();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterComponents();
            Mapper.Initialize(cfg => {
                cfg.AddProfile<DataMappingsProfile>();
                cfg.AddProfile<DataMappingsProfileWeb>();
            });
        }

        private void UpdateDatabase() {
            var migrationConfig = new Configuration();
            var migrator = new DbMigrator(migrationConfig);
            migrator.Update();
        }
    }
}
