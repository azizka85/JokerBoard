using System.Data.Common;
using DBDataSourceAbstract = System.Data.Common.DbDataSource;

namespace EQUTech.Core.Mocks.Data;

public sealed class DBDataSource : DBDataSourceAbstract
{
    public override string ConnectionString => throw new NotImplementedException();

    protected override DbConnection CreateDbConnection()
    {
        return default!;
    }

    protected override DbConnection OpenDbConnection()
    {
        return default!;
    }
}