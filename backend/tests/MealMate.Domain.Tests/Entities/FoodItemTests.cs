using FluentAssertions;
using MealMate.Domain.Entities;

namespace MealMate.Domain.Tests.Entities;

public class FoodItemTests
{
    [Fact]
    public void Constructor_Throws_WhenNameIsEmpty()
    {
        var act = () => new FoodItem(
            " ",
            "Description",
            10m,
            null,
            Guid.NewGuid());

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*name is required*");
    }

    [Fact]
    public void Constructor_Throws_WhenPriceIsNegative()
    {
        var act = () => new FoodItem(
            "Pizza",
            "Description",
            -1m,
            null,
            Guid.NewGuid());

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*price must not be negative*");
    }

    [Fact]
    public void Constructor_TrimsName_AndSetsItemAsAvailable()
    {
        var foodItem = new FoodItem(
            "  Pizza  ",
            "Description",
            10m,
            null,
            Guid.NewGuid());

        foodItem.Name.Should().Be("Pizza");
        foodItem.Price.Should().Be(10m);
        foodItem.IsAvailable.Should().BeTrue();
    }

    [Fact]
    public void SetAvailability_UpdatesAvailability()
    {
        var foodItem = new FoodItem(
            "Pizza",
            null,
            10m,
            null,
            Guid.NewGuid());

        foodItem.SetAvailability(false);

        foodItem.IsAvailable.Should().BeFalse();
    }
}
