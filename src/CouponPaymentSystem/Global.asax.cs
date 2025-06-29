using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Owin;
using Abp.Web;
using Castle.Facilities.Logging;
using CouponPaymentSystem;
using JetBrains.Annotations;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(MvcApplication))]

namespace CouponPaymentSystem;

public class MvcApplication : AbpWebApplication<CpsModule>
{
    [UsedImplicitly]
    public void Configuration(IAppBuilder app)
    {
        app.UseAbp();
        app.UseCookieAuthentication(
            new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/sign-in"),
                CookieName = CookieAuthenticationDefaults.CookiePrefix + "CPS.",
            }
        );
        app.MapSignalR();
    }

    protected override void Application_Start(object sender, EventArgs e)
    {
#if DEBUG
        AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
            f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
        );
#else
        AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
            f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.Production.config"))
        );
#endif
        base.Application_Start(sender, e);
    }
}
