using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Abp.Dependency;
using Cogwheel;

namespace CouponPaymentSystem.Core.Configurations;

public sealed class Config : SettingsBase, ISingletonDependency
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(
        JsonSerializerDefaults.Web
    )
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
    };

    public Config(string path)
        : base(path, JsonSerializerOptions) { }

    public DatabaseConfig Database { get; init; } = new DatabaseConfig();
}
