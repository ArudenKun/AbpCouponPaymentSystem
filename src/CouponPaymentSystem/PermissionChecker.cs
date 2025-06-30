using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Runtime.Session;

namespace CouponPaymentSystem;

public class PermissionChecker : IPermissionChecker
{
    private readonly IPermissionManager _permissionManager;
    private readonly IPrincipalAccessor _principalAccessor;

    public PermissionChecker(
        IPermissionManager permissionManager,
        IPrincipalAccessor principalAccessor
    )
    {
        _permissionManager = permissionManager;
        _principalAccessor = principalAccessor;
    }

    public Task<bool> IsGrantedAsync(string permissionName) =>
        Task.FromResult(IsGranted(permissionName));

    public bool IsGranted(string permissionName)
    {
        if (!_principalAccessor.Principal.Identity.IsAuthenticated)
            return false;

        var permission = _permissionManager.GetPermissionOrNull(permissionName);
        if (
            permission?.Properties["Roles"] is not List<string> grantedRoles
            || grantedRoles.Count == 0
        )
            return false;

        var userRoles = _principalAccessor
            .Principal?.Claims.Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToHashSet(); // Using HashSet for faster lookups

        if (userRoles == null || userRoles.Count == 0)
            return false;

        return grantedRoles.Any(userRoles.Contains);
    }

    public Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName) =>
        Task.FromResult(IsGranted(user, permissionName));

    public bool IsGranted(UserIdentifier user, string permissionName) => IsGranted(permissionName);
}

public class Test : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        context.CreatePermission(
            "Checker",
            properties: new Dictionary<string, object>
            {
                {
                    "Roles",
                    new List<string> { "CPS_CHECKER" }
                },
            }
        );
    }
}
