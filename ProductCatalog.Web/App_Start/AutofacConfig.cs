using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ProductCatalog.BLL.Services.Services;
using ProductCatalog.DAL;
using ProductCatalog.DAL.Contracts;
using ProductCatalog.DAL.Repositories;
using RestSharp;

namespace ProductCatalog.Web.App_Start {
    public class AutofacConfig {
        public static void RegisterComponents() {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            //builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            builder.Register(ctx => {
                var client = new RestClient {BaseUrl = new Uri("http://localhost:53806/api")};
                return client;
            }).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProductDialogDbContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<CsvRepository>().As<IBaseCsvRepository>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IBaseRepository<>));
            builder.RegisterType<ApiRepository>().As<IApiRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(BrandService).Assembly).Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            var container = builder.Build();
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}