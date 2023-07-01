using EQUTech.JokerBoard.Data.Models;
using System.Data.Common;

namespace EQUTech.JokerBoard.Data;

public interface ICategoryItemRepository
{
    DbDataSource DataSource { get; }

    List<Category> List(DbConnection connection, DbTransaction? transaction);
}
