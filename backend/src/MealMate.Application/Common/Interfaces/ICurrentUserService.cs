// Knows who is logged in
namespace MealMate.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}