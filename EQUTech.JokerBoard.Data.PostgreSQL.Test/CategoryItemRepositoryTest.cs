namespace EQUTech.JokerBoard.Data.PostgreSQL.Test;

[TestClass]
public class CategoryItemRepositoryTest
{
    [TestMethod]
    public void List()
    {
        var repository = new CategoryItemRepository(ConfigurationManager.ConnectionString);

        using var connection = repository.DataSource.OpenConnection();

        var data = repository.List(connection, null);

        Assert.IsNotNull(data);
    }
}
