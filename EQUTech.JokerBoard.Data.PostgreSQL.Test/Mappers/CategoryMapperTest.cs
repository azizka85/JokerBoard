using EQUTech.Core.Mocks.Data;
using EQUTech.JokerBoard.Data.PostgreSQL.Mappers;

namespace EQUTech.JokerBoard.Data.PostgreSQL.Test.Mappers;

[TestClass]
public class CategoryMapperTest
{
    [TestMethod]
    public void ReadCategory()
    {
        var record = new DataRecord
        (
            new()
            {
                    { "id", 1L },
                    { "pid", DBNull.Value },
                    { "name", "name1" }
            }
        );

        var data = record.ReadCategoryItem();

        Assert.AreEqual(1, data.Id);
        Assert.AreEqual("name1", data.Name);

        Assert.IsNull(data.Parent);

        record = new DataRecord
        (
            new()
            {
                    { "id", 2L },
                    { "pid", 1L },
                    { "name", "name2" }
            }
        );

        data = record.ReadCategoryItem();

        Assert.AreEqual(2, data.Id);
        Assert.AreEqual("name2", data.Name);

        Assert.IsNotNull(data.Parent);

        Assert.AreEqual(1, data.Parent.Id);
    }
}
