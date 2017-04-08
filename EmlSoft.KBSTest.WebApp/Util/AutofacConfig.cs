using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmlSoft.KBSTest.WebApp.Util
{
    public class AutofacConfig
    {
        public static IComponentContext ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<Data.SqlSourceRepository>().As<Domain.ISourceRepository>();

            // Мы не доверяем администратору
            builder.RegisterType<Data.SqlContext>().WithParameter("ConnectionString",
                @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=EmlSoft.KBSTest.SqlContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
              .InstancePerLifetimeScope()
              .ExternallyOwned();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}