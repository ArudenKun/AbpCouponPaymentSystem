using CouponPaymentSystem.Core.Models;

namespace CouponPaymentSystem.Core.Uploads;

public class UploadRequestDto
{
    public required string Name { get; set; }
    public required string Hash { get; set; }
    public required byte[] Content { get; set; }
    public required Currency Currency { get; set; }
    public required string UserId { get; set; }
    public required string DepartmentId { get; set; }
}
