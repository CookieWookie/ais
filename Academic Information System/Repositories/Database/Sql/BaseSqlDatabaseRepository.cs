using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace AiS.Repositories.Database.Sql
{
    public abstract class BaseSqlDatabaseRepository<T> : BaseDatabaseRepository<T> where T : class
    {
        protected BaseSqlDatabaseRepository(string connectionString, string getSingle, string getAll, string save)
            : base(connectionString, getSingle, getAll, save)
        {
        }

        protected override System.Data.IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        protected override System.Data.IDataParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, ReferenceEquals(value, null) ? DBNull.Value : value);
        }
    }
}
