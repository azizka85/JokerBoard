using EQUTech.Core.Grpc.Models.JokerBoard;
using EQUTech.Core.Services.JokerBoard;
using EQUTech.JokerBoard.Data;
using EQUTech.JokerBoard.Data.Models;

namespace EQUTech.JokerBoard.Services;

public sealed class CategoryItemService : ICategoryItemService
{
    private readonly ICategoryItemRepository _categoryItemRepository;

    public CategoryItemService(ICategoryItemRepository categoryItemRepository)
    {
        _categoryItemRepository = categoryItemRepository ?? throw new ArgumentNullException(nameof(categoryItemRepository));
    }

    public List<CategoryItem> List()
    {
        using var connection = _categoryItemRepository.DataSource.OpenConnection();

        return Category.ToCategoryItemTree
        (
            _categoryItemRepository.List(connection, null)
        );
    }
}
