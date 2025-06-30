using Abp.Authorization;
using Abp.Web.Mvc.Controllers;

namespace CouponPaymentSystem.Controllers.Common;

public abstract class CpsControllerBase : AbpController
{
    protected CpsControllerBase()
    {
        PermissionChecker.Authorize();
    }
}
