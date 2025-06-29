using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.NHibernate.EntityMappings;
using CouponPaymentSystem.Core.Models;
using Humanizer;
using JetBrains.Annotations;

namespace CouponPaymentSystem.Core.Data.Entities;

public class Upload : Entity<Guid>
{
    public virtual required string UserId { get; set; }
    public virtual required string FileName { get; set; }
    public virtual required string FileHash { get; set; }
    public virtual required string DepartmentId { get; set; }
    public virtual required Currency Currency { get; set; }
    public virtual IList<Transaction> Transactions { get; set; } = [];
    public virtual DateTime UploadDate { get; init; } = DateTime.Now;
}

[UsedImplicitly]
public class UploadMapping : EntityMap<Upload, Guid>
{
    public UploadMapping()
        : base(nameof(Upload).Pluralize())
    {
        Id(x => x.Id).GeneratedBy.GuidNative();
        Map(x => x.UserId).Not.Nullable().Length(16);
        Map(x => x.FileName).Not.Nullable().Unique().Length(60);
        Map(x => x.FileHash).Not.Nullable().Unique().Length(64);
        Map(x => x.DepartmentId).Not.Nullable().Length(4);
        Map(x => x.Currency).Not.Nullable();
        HasMany(s => s.Transactions).LazyLoad();
        Map(x => x.UploadDate).Not.Nullable().ReadOnly().Generated.Always();
    }
}
