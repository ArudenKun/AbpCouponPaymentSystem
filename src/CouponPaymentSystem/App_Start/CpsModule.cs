using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Application.Services;
using Abp.BlobStoring;
using Abp.BlobStoring.FileSystem;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.NHibernate;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.WebApi;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using CouponPaymentSystem.Common;
using CouponPaymentSystem.Core;
using CouponPaymentSystem.Core.Configurations;
using FluentNHibernate.Cfg.Db;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security;

namespace CouponPaymentSystem;

[DependsOn(
    typeof(CpsCoreModule),
    typeof(AbpWebMvcModule),
    typeof(AbpWebApiModule),
    typeof(AbpNHibernateModule),
    typeof(AbpWebSignalRModule),
    typeof(AbpBlobStoringFileSystemModule)
)]
public class CpsModule : AbpModule
{
    public override void PreInitialize()
    {
        IocManager.IocContainer.Register(
            Component
                .For<Config>()
                .UsingFactoryMethod(() =>
                {
                    var config = new Config(Path.Combine(PathHelper.RootDir, "config.json"));
                    var loaded = config.Load();
                    if (!loaded)
                        config.Save();
                    return config;
                })
                .LifestyleSingleton()
        );

        var config = IocManager.IocContainer.Resolve<Config>();
        Configuration.DefaultNameOrConnectionString = config.Database.Cps.ConnectionString;
        Configuration
            .Modules.AbpNHibernate()
            .FluentConfiguration.Database(
                MsSqlConfiguration.MsSql2012.ConnectionString(
                    Configuration.DefaultNameOrConnectionString
                )
            )
            .Mappings(m =>
                m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                    .AddFromAssembly(typeof(CpsCoreModule).Assembly)
            );

        var services = new ServiceCollection();
        IocManager.IocContainer.AddServices(services);

        Configuration
            .Modules.AbpBlobStoring()
            .Containers.ConfigureDefault(options =>
            {
                options.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = PathHelper.BlobDir;
                    Logger.Info($"Blob Storing Path: {fileSystem.BasePath}");
                });
            });
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        IocManager.IocContainer.Register(
            Component
                .For<IAuthenticationManager>()
                .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                .LifestyleTransient()
        );

        Configuration
            .Modules.AbpWebApi()
            .DynamicApiControllerBuilder.ForAll<IApplicationService>(
                typeof(CpsCoreModule).Assembly,
                "app"
            )
            .WithConventionalServiceName()
            .WithConventionalVerbs()
            .Build();

        AreaRegistration.RegisterAllAreas();
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
}
