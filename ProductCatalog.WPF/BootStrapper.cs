using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.DAL;
using ProductCatalog.DAL.Contracts;
using ProductCatalog.DAL.Repositories;
using ProductCatalog.WPF.BLL.Services.Services;
using RestSharp;
using Parameter = Autofac.Core.Parameter;

namespace ProductCatalog.WPF {
    public static class BootStrapper {
        private static ILifetimeScope _rootScope;
        //private static IChromeViewModel _chromeViewModel;

        //public static IViewModel RootVisual
        //{
        //    get
        //    {
        //        if (_rootScope == null)
        //        {
        //            Start();
        //        }

        //        _chromeViewModel = _rootScope.Resolve<IChromeViewModel>();
        //        return _chromeViewModel;
        //    }
        //}

        public static void Start() {
            if (_rootScope != null) {
                return;
            }

            var builder = new ContainerBuilder();
            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            builder.Register(ctx => {
                var client = new RestClient {BaseUrl = new Uri("http://localhost:53806/api")};
                return client;
            }).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProductDialogDbContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<CsvRepository>().As<IBaseCsvRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ApiRepository>().As<IApiRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(EquipmentApiService).Assembly).Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            // several view model instances are transitory and created on the fly, if these are tracked by the container then they
            // won't be disposed of in a timely manner

            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(t => typeof(IViewModel).IsAssignableFrom(t))
            //    .Where(t =>
            //    {
            //        var isAssignable = typeof(ITransientViewModel).IsAssignableFrom(t);
            //        if (isAssignable)
            //        {
            //            Debug.WriteLine("Transient view model - " + t.Name);
            //        }

            //        return isAssignable;
            //    })
            //    .AsImplementedInterfaces()
            //    .ExternallyOwned();

            _rootScope = builder.Build();
        }

        public static void Stop() {
            _rootScope.Dispose();
        }

        public static T Resolve<T>() {
            if (_rootScope == null) {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters) {
            if (_rootScope == null) {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(parameters);
        }
    }
}
