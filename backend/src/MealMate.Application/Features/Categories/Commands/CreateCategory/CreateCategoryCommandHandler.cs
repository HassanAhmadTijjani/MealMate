using MealMate.Application.Common.Interfaces;
using MealMate.Domain.Entities;
using MediatR;

namespace MealMate.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommandHandler(ICategoryRepository CategoryRepository) : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository = CategoryRepository;
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(
            request.Name,
            request.Description
        );
        await _categoryRepository.AddAsync(category);
        return  category.Id;
    }
}