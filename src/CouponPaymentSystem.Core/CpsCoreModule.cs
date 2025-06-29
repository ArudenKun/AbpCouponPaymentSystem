using System.Reflection;
using Abp.Modules;

namespace CouponPaymentSystem.Core;

public class CpsCoreModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
    }
}
