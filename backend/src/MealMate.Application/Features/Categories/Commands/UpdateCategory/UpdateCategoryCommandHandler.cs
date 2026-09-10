using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommandHandler(ICategoryRepository CategoryRepository) : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository = CategoryRepository;
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category is null) return false;
        category.UpdateDetails(
            request.Name,
            request.Description
        );
        await _categoryRepository.UpdateAsync(category);
        return true;
    }
}