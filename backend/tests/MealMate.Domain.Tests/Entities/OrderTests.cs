using FluentAssertions;
using MealMate.Domain.Entities;
using MealMate.Domain.Enums;

namespace MealMate.Domain.Tests.Entities;

public class OrderTests
{
    [Fact]
    public void MarkPaymentAsFailed_Throws_WhenReasonIsEmpty()
    {
        var order = new Order(
            "customer-1",
            OrderType.Delivery,
            "Main Street 123",
            5m,
            [new OrderItem(Guid.NewGuid(), "Burger", 10m, 2)]);

        var act = () => order.MarkPaymentAsFailed("   ");

        act.Should().Throw<ArgumentException>()
            .WithMessage("*reason is required*");
    }

    [Fact]
    public void MarkPaymentAsFailed_SetsPaymentStatusToFailed_WhenReasonIsProvided()
    {
        var order = new Order(
            "customer-1",
            OrderType.Delivery,
            "Main Street 123",
            5m,
            [new OrderItem(Guid.NewGuid(), "Burger", 10m, 2)]);

        order.MarkPaymentAsFailed("Proof is blurry.");

        order.PaymentStatus.Should().Be(PaymentStatus.Failed);
        order.UpdatedAt.Should().NotBe(default);
    }
}
