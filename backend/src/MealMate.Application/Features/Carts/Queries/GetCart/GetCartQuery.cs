using MealMate.Application.Common.Models.Carts;
using MediatR;

namespace MealMate.Application.Features.Carts.Queries.GetCart;

public sealed record GetCartQuery : IRequest<CartResponse>;