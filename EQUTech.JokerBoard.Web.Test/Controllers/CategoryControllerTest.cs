using EQUTech.Core.Grpc.Models.JokerBoard;
using EQUTech.Core.Mocks.JokerBoard;
using EQUTech.JokerBoard.Web.GrpcServices;
using Microsoft.AspNetCore.Mvc;

namespace EQUTech.JokerBoard.Web.Test.Controllers;

[TestClass]
public class CategoryControllerTest
{
    [TestMethod]
    public void List()
    {
        var service = new CategoryItemService();
        var controller = new CategoryService(service);

        var list = service.List();
        var result = controller.List();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        var okObjectResult = (OkObjectResult)result;

        Assert.AreEqual(200, okObjectResult.StatusCode);
        Assert.IsInstanceOfType(okObjectResult.Value, typeof(List<CategoryItem>));

        var objResult = (List<CategoryItem>)okObjectResult.Value!;

        Assert.AreEqual(list.Count, objResult.Count);

        Assert.AreEqual(list[0].Id, objResult[0].Id);
        Assert.AreEqual(list[0].Name, objResult[0].Name);
    }
}
