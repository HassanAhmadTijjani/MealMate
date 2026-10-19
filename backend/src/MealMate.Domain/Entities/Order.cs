using MealMate.Domain.Enums;

namespace MealMate.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string OrderReference { get; private set; } = string.Empty;

    public string CustomerId { get; private set; } = string.Empty;

    public OrderType OrderType { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public string? DeliveryAddress { get; private set; }

    public ICollection<OrderItem> Items { get; private set; } = [];
    public ICollection<PaymentSubmission> PaymentSubmissions { get; private set; } = [];

    public decimal ItemsSubtotal { get; private set; }

    public decimal DeliveryFee { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
    public DateTime PaymentExpiresAt { get; private set; }
    public DateTime? FirstPaymentSubmissionAt { get; private set; }

    private Order() { }

    public Order(string customerId, OrderType orderType, string? deliveryAddress, decimal deliveryFee, List<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentException("Customer ID is required.", nameof(customerId));

        if (items is null || items.Count == 0) throw new ArgumentException("Order must contain at least one item.", nameof(items));

        if (deliveryFee < 0) throw new ArgumentException("Delivery fee cannot be negative.", nameof(deliveryFee));

        if (orderType == OrderType.Delivery && string.IsNullOrWhiteSpace(deliveryAddress)) throw new ArgumentException("Delivery address is required for delivery orders.", nameof(deliveryAddress));

        if (orderType == OrderType.Pickup && !string.IsNullOrWhiteSpace(deliveryAddress)) throw new ArgumentException("Pickup orders cannot have a delivery address.", nameof(deliveryAddress));

        if (orderType == OrderType.Pickup && deliveryFee > 0) throw new ArgumentException("Pickup orders cannot have a delivery fee.", nameof(deliveryFee));

        Id = Guid.NewGuid();
        OrderReference = OrderRefrenceGenerator();
        CustomerId = customerId;
        OrderType = orderType;
        OrderStatus = OrderStatus.Pending;
        PaymentStatus = PaymentStatus.Pending;
        DeliveryAddress = deliveryAddress;
        Items = items;
        ItemsSubtotal = items.Sum(item => item.Subtotal);
        DeliveryFee = deliveryFee;
        TotalAmount = ItemsSubtotal + DeliveryFee;

        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
        PaymentExpiresAt = now.AddMinutes(30);
    }



    // public void UpdateStatus(OrderStatus newOrderStatus)
    // {
    //     OrderStatus = newOrderStatus;
    //     UpdatedAt = DateTime.UtcNow;
    // }
    public static string OrderRefrenceGenerator()
    {
        const string characters = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        return "MM-" + new string([.. Enumerable.Range(0, 5).Select(_ => characters[Random.Shared.Next(characters.Length)])]);
    }

    public void Confirm()
    {
        if (OrderStatus != OrderStatus.Pending) throw new InvalidOperationException("Only pending orders can be confirmed.");
        OrderStatus = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;
    }
    public void StartPreparing()
    {
        if (OrderStatus != OrderStatus.Confirmed) throw new InvalidOperationException("Only confirmed order can be preparing.");
        OrderStatus = OrderStatus.Preparing;
        UpdatedAt = DateTime.UtcNow;
    }
    public void MarkAsReady()
    {
        if (OrderStatus != OrderStatus.Preparing) throw new InvalidOperationException("Only preparing orders can be ready.");
        OrderStatus = OrderStatus.Ready;
        UpdatedAt = DateTime.UtcNow;
    }
    public void MarkAsOutForDelivery()
    {
        if (OrderType != OrderType.Delivery) throw new InvalidOperationException("Only delivery orders can be marked as out for delivery.");
        if (OrderStatus != OrderStatus.Ready) throw new InvalidOperationException("Only ready order can be out for delivery.");
        OrderStatus = OrderStatus.OutForDelivery;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Complete()
    {
        if (OrderType == OrderType.Pickup && OrderStatus != OrderStatus.Ready) throw new InvalidOperationException("Only ready pickup orders can be completed.");

        if (OrderType == OrderType.Delivery && OrderStatus != OrderStatus.OutForDelivery) throw new InvalidOperationException("Only orders that are out for delivery can be completed.");

        OrderStatus = OrderStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Cancel()
    {
        if (OrderStatus is OrderStatus.Completed or OrderStatus.OutForDelivery or OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("This order cannot be cancelled.");
        }

        OrderStatus = OrderStatus.Cancelled;

        UpdatedAt = DateTime.UtcNow;
    }
    public void MarkAsPaid()
    {
        if (OrderStatus == OrderStatus.Cancelled) throw new InvalidOperationException("A cancelled order cannot receive payment.");
        if (PaymentStatus == PaymentStatus.Paid) throw new InvalidOperationException("Payment has already been completed.");

        PaymentStatus = PaymentStatus.Paid;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkPaymentAsFailed()
    {
        if (OrderStatus == OrderStatus.Cancelled) throw new InvalidOperationException("A cancelled order cannot have a payment attempt.");

        if (PaymentStatus == PaymentStatus.Paid) throw new InvalidOperationException("A completed payment cannot be marked as failed.");

        PaymentStatus = PaymentStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ExpirePayment()
    {
        if (OrderStatus == OrderStatus.Cancelled) return;

        if (PaymentStatus == PaymentStatus.Paid) return;

        PaymentStatus = PaymentStatus.Failed;
        OrderStatus = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
    public void RecordFirstPaymentSubmission(DateTime submittedAt)
    {
        if (FirstPaymentSubmissionAt.HasValue ||
            PaymentSubmissions.Count > 0)
        {
            throw new InvalidOperationException(
                "The first payment submission already exists."
            );
        }

        if (OrderStatus == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                "A cancelled order cannot accept payment submissions."
            );

        if (PaymentStatus != PaymentStatus.Pending)
            throw new InvalidOperationException(
                "This order is not awaiting its first payment submission."
            );

        if (submittedAt > PaymentExpiresAt)
            throw new InvalidOperationException(
                "The payment deadline has passed."
            );

        FirstPaymentSubmissionAt = submittedAt;
        PaymentStatus = PaymentStatus.AwaitingVerification;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordCorrectedPaymentSubmission(DateTime submittedAt)
    {
        if (!FirstPaymentSubmissionAt.HasValue)
            throw new InvalidOperationException(
                "An initial payment submission is required first."
            );

        if (OrderStatus == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                "A cancelled order cannot accept payment submissions."
            );

        if (PaymentStatus != PaymentStatus.AwaitingVerification)
            throw new InvalidOperationException(
                "This order is not awaiting payment verification."
            );

        if (PaymentSubmissions.Count >= 2)
            throw new InvalidOperationException(
                "The maximum number of payment submissions has been reached."
            );

        var latestSubmission = PaymentSubmissions
            .OrderByDescending(submission => submission.SubmittedAt)
            .FirstOrDefault();

        if (latestSubmission?.Status != PaymentSubmissionStatus.Rejected)
            throw new InvalidOperationException(
                "The previous submission must be rejected before submitting a correction."
            );

        if (submittedAt < FirstPaymentSubmissionAt.Value)
            throw new InvalidOperationException(
                "The correction timestamp cannot precede the first submission."
            );

        UpdatedAt = DateTime.UtcNow;
    }
}