using EQUTech.JokerBoard.Data.Models;

namespace EQUTech.JokerBoard.Data.Test.Models;

[TestClass]
public class CategoryTest
{
    [TestMethod]
    public void ToCategoryItem()
    {
        var category = new Category
        {
            Id = 1L,
            Name = "Test"
        };

        var result = category.ToCategoryItem();

        Assert.AreEqual(category.Id, result.Id);
        Assert.AreEqual(category.Name, result.Name);
    }

    [TestMethod]
    public void ToCategoryItemTree()
    {
        var list = new List<Category>
            {
                new()
                {
                    Id = 1L,
                    Name = "Test1"
                },
                new()
                {
                    Id = 2L,
                    Name = "Test2",
                    Parent = new()
                    {
                        Id = 1L
                    }
                }
            };

        var result = Category.ToCategoryItemTree(list);

        Assert.AreEqual(1, result.Count);

        Assert.AreEqual(list[0].Id, result[0].Id);
        Assert.AreEqual(list[0].Name, result[0].Name);

        Assert.IsNotNull(result[0].Categories);
        Assert.AreEqual(1, result[0].Categories?.List.Count);

        Assert.AreEqual(list[1].Id, result[0].Categories?.List[0].Id);
        Assert.AreEqual(list[1].Name, result[0].Categories?.List[0].Name);
    }
}
