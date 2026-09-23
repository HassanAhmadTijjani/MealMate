using MealMate.Application.Common.Models.CustomerMenus;
using MediatR;

namespace MealMate.Application.Features.CustomerMenus.Queries.BrowseMenu;

public sealed record BrowseMenuQuery(
    Guid? CategoryId
) : IRequest<List<CustomerMenuItemResponse>>;