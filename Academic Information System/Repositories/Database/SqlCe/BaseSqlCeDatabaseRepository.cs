using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace AiS.Repositories.Database.SqlCe
{
    public abstract class BaseSqlCeDatabaseRepository<T> : BaseDatabaseRepository<T> where T : class
    {
        protected BaseSqlCeDatabaseRepository(string connectionString, string getSingle, string getAll, string save)
            : base(connectionString, getSingle, getAll, save)
        {
        }

        protected override System.Data.IDbConnection GetConnection()
        {
            return new SqlCeConnection(connectionString);
        }

        protected override System.Data.IDataParameter CreateParameter(string name, object value)
        {
            return new SqlCeParameter(name, ReferenceEquals(value, null) ? DBNull.Value : value);
        }

        protected override int SaveImpl(string commandString, params T[] models)
        {
            int count = 0;
            using (SqlCeConnection connection = (SqlCeConnection)GetConnection())
            {
                connection.Open();
                using (SqlCeTransaction transaction = connection.BeginTransaction())
                using (SqlCeCommand command = new SqlCeCommand(commandString, connection, transaction))
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
