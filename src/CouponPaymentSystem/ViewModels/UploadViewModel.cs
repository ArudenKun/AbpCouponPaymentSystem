using System.ComponentModel.DataAnnotations;
using System.Web;
using CouponPaymentSystem.Core.Models;

namespace CouponPaymentSystem.ViewModels;

public class UploadViewModel
{
    [Required]
    public HttpPostedFileBase? File { get; set; }

    [Required]
    public Currency Currency { get; set; }

    public bool Overwrite { get; set; }
}
