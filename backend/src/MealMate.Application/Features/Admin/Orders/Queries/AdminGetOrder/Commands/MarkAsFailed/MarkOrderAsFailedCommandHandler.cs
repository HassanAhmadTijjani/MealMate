using MealMate.Application.Common.Interfaces;
using MealMate.Application.Common.Models.Orders;
using MealMate.Domain.Entities;
using MealMate.Domain.Enums;
using MediatR;

namespace MealMate.Application.Features.Admin.Orders.Queries.AdminGetOrder.Commands.MarkAsFailed;

public sealed record MarkOrderAsFailedCommandHandler(
    IOrderRepository OrderRepository,
    ICurrentUserService CurrentUserService) : IRequestHandler<MarkOrderAsFailedCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository = OrderRepository;
    private readonly ICurrentUserService _currentUserService = CurrentUserService;

    public async Task<OrderResponse> Handle(MarkOrderAsFailedCommand request, CancellationToken cancellationToken)
    {
        var adminId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Admin user is not authenticated.");

        var order = await _orderRepository.GetByIdWithPaymentSubmissionsAsync(request.OrderId)
            ?? throw new KeyNotFoundException("Order not found");

        var latestSubmission = order.PaymentSubmissions
            .OrderByDescending(submission => submission.SubmittedAt)
            .FirstOrDefault();

        if (latestSubmission is not null && latestSubmission.Status == PaymentSubmissionStatus.AwaitingReview)
        {
            latestSubmission.Reject(request.Reason, adminId);
        }

        order.MarkPaymentAsFailed(request.Reason);
        await _orderRepository.UpdateAsync(order);

        return MapToResponse(order);
    }

    private static OrderResponse MapToResponse(Order order)
    {
        var items = order.Items.Select(item => new OrderItemResponse(
            item.FoodItemId,
            item.FoodItemName,
            item.UnitPrice,
            item.Quantity,
            item.Subtotal
        )).ToList();

        return new OrderResponse(
            order.Id,
            order.OrderReference,
            order.OrderType,
            order.OrderStatus,
            order.PaymentStatus,
            order.DeliveryAddress,
            items,
            order.ItemsSubtotal,
            order.DeliveryFee,
            order.TotalAmount,
            order.CreatedAt,
            order.UpdatedAt,
            order.PaymentExpiresAt
        );
    }
}
