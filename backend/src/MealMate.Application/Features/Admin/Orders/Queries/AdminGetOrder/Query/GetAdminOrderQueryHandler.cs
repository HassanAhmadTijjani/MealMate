using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Query;

public sealed record GetAdminOrderQueryHandler(IOrderRepository OrderRepository) : IRequestHandler<GetAdminOrderQuery, AdminOrderResponse>
{
    private readonly IOrderRepository _orderRepository = OrderRepository;
    public async Task<AdminOrderResponse> Handle(GetAdminOrderQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByIdForAdminAsync(request.OrderId, cancellationToken) ?? throw new KeyNotFoundException("Order not found");
    }


}