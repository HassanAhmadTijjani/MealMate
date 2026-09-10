using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQueryHandler(ICategoryRepository CategoryRepository) : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    private readonly ICategoryRepository _categoryRepository = CategoryRepository;
    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetByIdAsync(request.Id);
    }
}