using System;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Humanizer;

namespace CouponPaymentSystem.Common;

public static class DynamicApiControllerExtensions
{
    private const string ServicePostfix = "Service";
    private const string AppServicePostfix = "AppService";

    public static IBatchApiControllerBuilder<T> WithConventionalServiceName<T>(
        this IBatchApiControllerBuilder<T> builder
    ) =>
        builder.WithServiceName(t =>
        {
            var name = t.Name.AsSpan();
            if (t.IsInterface && name.StartsWith("I", StringComparison.OrdinalIgnoreCase))
                name = name.Slice(1);
            if (name.EndsWith(ServicePostfix, StringComparison.OrdinalIgnoreCase))
                name = name.Slice(0, name.Length - ServicePostfix.Length);
            else if (name.EndsWith(AppServicePostfix, StringComparison.OrdinalIgnoreCase))
                name = name.Slice(0, name.Length - AppServicePostfix.Length);
            return name.ToString().Camelize();
        });
}
