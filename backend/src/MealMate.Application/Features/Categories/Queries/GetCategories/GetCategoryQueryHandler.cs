using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoryQueryHandler(ICategoryRepository CategoryRepository) : IRequestHandler<GetCategoryQuery, List<Category>>
{
    private readonly ICategoryRepository _categoryRepository = CategoryRepository;
    public async Task<List<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAllAsync();
    }
}