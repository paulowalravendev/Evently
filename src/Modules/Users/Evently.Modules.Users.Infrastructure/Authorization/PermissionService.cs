using Evently.Common.Application.Authorization;
using Evently.Common.Application.Caching;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace Evently.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(
    ISender sender, 
    ICacheService cacheService)
    : IPermissionService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(
        string identityId)
    {
        string cacheKey = CreateCacheKey(identityId);

        Result<PermissionsResponse>? permissions = await cacheService
            .GetAsync<Result<PermissionsResponse>>(cacheKey);

        if (permissions is null)
        {
            permissions = await sender.Send(new GetUserPermissionsQuery(identityId));
            await cacheService.SetAsync(cacheKey, permissions, DefaultExpiration);
        }

        return permissions;
    }

    private static string CreateCacheKey(string identityId) => $"permissions:{identityId}";
}
