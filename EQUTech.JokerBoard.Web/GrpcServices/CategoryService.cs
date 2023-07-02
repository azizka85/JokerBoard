using EQUTech.Core.Services.JokerBoard;
using CategoryServiceGrpc = EQUTech.Core.Grpc.Services.JokerBoard.Category.Service.ServiceBase;

namespace EQUTech.JokerBoard.Web.GrpcServices;

public sealed class CategoryService : CategoryServiceGrpc
{
    private readonly ICategoryItemService _categoryItemService;

    public CategoryService(ICategoryItemService categoryItemService)
    {
        _categoryItemService = categoryItemService ?? throw new ArgumentNullException(nameof(categoryItemService));
    }
}
