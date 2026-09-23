using MealMate.Application.Common.Models.CustomerMenus;
using MediatR;

namespace MealMate.Application.Features.CustomerMenus.Queries.GetMenuItemById;

public sealed record GetMenuItemByIdQuery(Guid Id) : IRequest<CustomerMenuItemResponse?>;