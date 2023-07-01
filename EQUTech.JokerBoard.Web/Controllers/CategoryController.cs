using EQUTech.Core.Services.JokerBoard;
using Microsoft.AspNetCore.Mvc;

namespace EQUTech.JokerBoard.Web.Controllers;

[Route("{controller}")]
public sealed class CategoryController : Controller
{
    private readonly ICategoryItemService _categoryItemService;

    public CategoryController(ICategoryItemService categoryItemService)
    {
        _categoryItemService = categoryItemService ?? throw new ArgumentNullException(nameof(categoryItemService));
    }

    [HttpGet("{action}")]
    public IActionResult List()
    {
        return Ok
        (
            _categoryItemService.List()
        );
    }
}
