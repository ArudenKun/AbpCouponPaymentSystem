using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Authorization;
using CouponPaymentSystem.Controllers.Common;
using CouponPaymentSystem.Core.Uploads;

namespace CouponPaymentSystem.Controllers;

[AbpAuthorize]
public class HomeController : CpsControllerBase
{
    private readonly IUploadService _uploadService;

    public HomeController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    public async Task<ActionResult> Index()
    {
        return View();
    }

    public ActionResult About()
    {
        ViewBag.Message = "Your application description page.";

        return View();
    }

    public ActionResult Contact()
    {
        ViewBag.Message = "Your contact page.";

        return View();
    }
}
