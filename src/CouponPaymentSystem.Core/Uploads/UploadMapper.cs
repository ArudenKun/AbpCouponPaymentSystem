using CouponPaymentSystem.Core.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace CouponPaymentSystem.Core.Uploads;

[Mapper]
public static partial class UploadMapper
{
    [MapProperty(nameof(UploadRequestDto.Name), nameof(Upload.FileName))]
    [MapProperty(nameof(UploadRequestDto.Hash), nameof(Upload.FileHash))]
    public static partial Upload Map(this UploadRequestDto source);
}
