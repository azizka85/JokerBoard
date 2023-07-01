using EQUTech.Core.Grpc.Models.JokerBoard;
using EQUTech.Core.Services.JokerBoard;

namespace EQUTech.Core.Mocks.JokerBoard;

public sealed class CategoryItemService : ICategoryItemService
{
    public List<CategoryItem> List()
    {
        return new()
            {
                new CategoryItem
                {
                    Id = 1,
                    Name = "Test1",
                }
            };
    }
}
