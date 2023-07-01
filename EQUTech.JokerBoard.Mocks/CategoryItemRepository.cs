using EQUTech.JokerBoard.Data.Models;
using EQUTech.JokerBoard.Data;
using System.Data.Common;
using DbDataSourceMock = EQUTech.Core.Mocks.Data.DBDataSource;

namespace EQUTech.JokerBoard.Mocks;

public sealed class CategoryItemRepository : ICategoryItemRepository
{
    public CategoryItemRepository()
    {
        DataSource = new DbDataSourceMock();
    }

    public DbDataSource DataSource { get; }

    public List<Category> List(DbConnection connection, DbTransaction? transaction)
    {
        return new()
            {
                new()
                {
                    Id = 1,
                    Name = "Test1"
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    Parent = new()
                    {
                        Id = 1
                    }
                }
            };
    }
}
