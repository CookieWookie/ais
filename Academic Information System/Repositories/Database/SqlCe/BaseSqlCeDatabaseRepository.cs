using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;

namespace AiS.Repositories.Database.SqlCe
{
    public abstract class BaseSqlCeDatabaseRepository<T> : BaseDatabaseRepository<T> where T : class
    {
        protected readonly string insert;
        protected readonly string update;

        protected BaseSqlCeDatabaseRepository(string connectionString, string getSingle, string getAll, string insert, string update, string delete)
            : base(connectionString, getSingle, getAll, "NOT USING", delete)
        {
            if (string.IsNullOrWhiteSpace(insert))
            {
                throw new ArgumentException("insert");
            }
            if (string.IsNullOrWhiteSpace(update))
            {
                throw new ArgumentException("update");
            }

            this.insert = insert;
            this.update = update;

            SqlCeConnectionStringBuilder sb = new SqlCeConnectionStringBuilder(connectionString);
            string path = sb.DataSource;
            if (!File.Exists(path))
            {
                using (SqlCeEngine engine = new SqlCeEngine(connectionString))
                {
                    engine.CreateDatabase();
                }
                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                using (SqlCeCommand command = new SqlCeCommand(Properties.Resources.SQLCE_1_0_0_0, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected override System.Data.IDbConnection GetConnection()
        {
            return new SqlCeConnection(connectionString);
        }

        protected override System.Data.IDataParameter CreateParameter(string name, object value)
        {
            return new SqlCeParameter(name, ReferenceEquals(value, null) ? DBNull.Value : value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected override int SaveImpl(string commandString, params T[] models)
        {
            int count = 0;
            using (SqlCeConnection connection = (SqlCeConnection)GetConnection())
            {
                connection.Open();
                using (SqlCeTransaction transaction = connection.BeginTransaction())
                using (SqlCeCommand command = new SqlCeCommand("", connection, transaction))
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected override void SaveModel(System.Data.IDbCommand command, T model)
        {
            SetParameters(command, model);
            command.CommandText = getSingleString;
            bool exists = command.ExecuteScalar() != null;
            command.CommandText = exists ? update : insert;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
    }
}
