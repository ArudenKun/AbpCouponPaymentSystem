using System.Text.Json.Serialization;

namespace CouponPaymentSystem.Core.Configurations;

public sealed class DatabaseConfig
{
    public DatabaseCredential Aso { get; init; } = new();

    public DatabaseCredential Cps { get; init; } =
        new()
        {
            Host = "192.168.100.202",
            Port = 1433,
            UserId = "sa",
            Password = "sa",
            InitialCatalog = "Test",
        };
}

public class DatabaseCredential
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 1125;
    public string InitialCatalog { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    [JsonIgnore]
    public string ConnectionString =>
        $"Data Source={Host},{Port};Initial Catalog={InitialCatalog};User Id={UserId};Password={Password}";
}
