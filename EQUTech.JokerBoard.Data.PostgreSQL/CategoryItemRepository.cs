using EQUTech.JokerBoard.Data.Models;
using EQUTech.JokerBoard.Data.PostgreSQL.Mappers;
using Npgsql;
using System.Data.Common;

namespace EQUTech.JokerBoard.Data.PostgreSQL;

public sealed class CategoryItemRepository : ICategoryItemRepository
{
    public CategoryItemRepository(string? connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

        DataSource = dataSourceBuilder.Build();
    }

    public DbDataSource DataSource { get; }

    public List<Category> List(DbConnection connection, DbTransaction? transaction)
    {
        using var command = connection.CreateCommand();

        command.CommandText = "select * from category_items_list()";
        command.Transaction = transaction;

        using var reader = command.ExecuteReader();

        var data = new List<Category>();

        while (reader.Read())
        {
            data.Add
            (
                reader.ReadCategoryItem()
            );
        }

        return data;
    }
}
