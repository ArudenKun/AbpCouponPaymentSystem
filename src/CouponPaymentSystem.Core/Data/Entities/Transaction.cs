using System;
using Abp.Domain.Entities;
using Abp.NHibernate.EntityMappings;
using Humanizer;
using JetBrains.Annotations;

namespace CouponPaymentSystem.Core.Data.Entities;

public class Transaction : Entity<Guid>
{
    public virtual required decimal Amount { get; set; }
    public virtual Upload Upload { get; set; } = null!;
}

[UsedImplicitly]
public class TransactionMap : EntityMap<Transaction, Guid>
{
    public TransactionMap()
        : base(nameof(Transaction).Pluralize())
    {
        Id(x => x.Id).GeneratedBy.GuidNative();
        Map(x => x.Amount).Not.Nullable();
        References(x => x.Upload).Column("UploadId").LazyLoad().Cascade.Delete();
    }
}
