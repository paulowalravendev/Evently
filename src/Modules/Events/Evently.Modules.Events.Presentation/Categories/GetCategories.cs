using Evently.Modules.Events.Application.Categories.GetCategories;
using Evently.Modules.Events.Application.Categories.GetCategory;
using Evently.Common.Domain;
using Evently.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Application.Caching;

namespace Evently.Modules.Events.Presentation.Categories;

internal static class GetCategories
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender, ICacheService cacheService) =>
        {
            const string cacheKey = "categories";
            IReadOnlyCollection<CategoryResponse>? categoryResponses = await cacheService
                .GetAsync<IReadOnlyCollection<CategoryResponse>>(cacheKey);

            if (categoryResponses is not null)
            {
                return Results.Ok(categoryResponses);
            }

            Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(new GetCategoriesQuery());

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(cacheKey, result.Value);
            }

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Categories);
    }
}
