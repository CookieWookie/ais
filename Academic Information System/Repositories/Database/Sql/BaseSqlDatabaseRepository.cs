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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected override int SaveImpl(string commandString, params T[] models)
        {
            int count = 0;
            using (SqlConnection connection = (SqlConnection)GetConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                using (SqlCommand command = new SqlCommand(commandString, connection, transaction))
                {
                    try
                    {
                        models.ForEach(m =>
                        {
                            SaveModel(command, m);
                            count++;
                        });
                        transaction.Commit();
                    }
                    catch
                    {
                        if (transaction != null && connection.State != System.Data.ConnectionState.Closed)
                        {
                            count = 0;
                            transaction.Rollback();
                        }
                        throw;
                    }
                }
            }
            return count;
        }
    }
}
