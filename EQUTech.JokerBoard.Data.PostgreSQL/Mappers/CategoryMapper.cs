using EQUTech.JokerBoard.Data.Models;
using System.Data;

namespace EQUTech.JokerBoard.Data.PostgreSQL.Mappers;

public static class CategoryMapper
{
    public static Category ReadCategoryItem(this IDataRecord record)
    {
        return new()
        {
            Id = (long)record["id"],
            Name = (string)record["name"],
            Parent = record["pid"] is DBNull ? null : new Category
            {
                Id = (long)record["pid"]
            }
        };
    }
}
