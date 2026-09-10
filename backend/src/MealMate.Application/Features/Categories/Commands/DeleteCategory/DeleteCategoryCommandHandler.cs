using MealMate.Application.Common.Interfaces;
using MediatR;

namespace MealMate.Application.Features.Categories.Commands.DeleteCategory;

public sealed record DeleteCategoryCommandHandler(ICategoryRepository CategoryRepository) : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository = CategoryRepository;
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category is null) return false;
        await _categoryRepository.DeleteAsync(category);
        return true;
    }
}