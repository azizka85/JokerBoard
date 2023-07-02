using EQUTech.Core.Grpc.Models.JokerBoard;
using EQUTech.Core.Services.JokerBoard;

namespace EQUTech.Core.Mocks.JokerBoard;

public sealed class CategoryItemService : ICategoryItemService
{
    public CategoryItems List()
    {
        var data = new CategoryItems();

        data.List.Add
        (
            new CategoryItem
            {
                Id = 1,
                Name = "Test1",
            }
        );

        return data;
    }
}
