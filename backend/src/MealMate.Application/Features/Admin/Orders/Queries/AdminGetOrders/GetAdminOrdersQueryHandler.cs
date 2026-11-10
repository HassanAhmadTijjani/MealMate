using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrders;

public sealed class GetAdminOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetAdminOrdersQuery, List<AdminOrdersResponse>>
{
    public Task<List<AdminOrdersResponse>> Handle(GetAdminOrdersQuery request, CancellationToken cancellationToken)
    {
        return orderRepository.GetAllForAdminAsync(cancellationToken);
    }
}