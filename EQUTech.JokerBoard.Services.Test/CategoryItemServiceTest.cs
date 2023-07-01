using EQUTech.JokerBoard.Mocks;

namespace EQUTech.JokerBoard.Services.Test;

[TestClass]
public class CategoryItemServiceTest
{
    [TestMethod]
    public void List()
    {
        var repository = new CategoryItemRepository();
        var service = new CategoryItemService(repository);

        var list = repository.List(null!, null);
        var result = service.List();

        Assert.AreEqual(1, result.Count);

        Assert.AreEqual(list[0].Id, result[0].Id);
        Assert.AreEqual(list[0].Name, result[0].Name);

        Assert.IsNotNull(result[0].Categories);
        Assert.AreEqual(1, result[0].Categories?.List.Count);

        Assert.AreEqual(list[1].Id, result[0].Categories?.List[0].Id);
        Assert.AreEqual(list[1].Name, result[0].Categories?.List[0].Name);
    }
}
