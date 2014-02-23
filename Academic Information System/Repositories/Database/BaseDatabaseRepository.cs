using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace AiS.Repositories.Database
{
    public abstract class BaseDatabaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly string getSingleString;
        protected readonly string getAllString;
        protected readonly string saveString;
        protected readonly string connectionString;

        protected BaseDatabaseRepository(string connectionString, string getSingle, string getAll, string save)
        {
            if (string.IsNullOrWhiteSpace("connectionString"))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrWhiteSpace(getAll))
            {
                throw new ArgumentNullException("getAll");
            }
            if (string.IsNullOrWhiteSpace(save))
            {
                throw new ArgumentNullException("save");
            }
            if (string.IsNullOrWhiteSpace(getSingle))
            {
                throw new ArgumentNullException("getSingle");
            }

            this.connectionString = connectionString;
            this.getAllString = getAll;
            this.saveString = save;
            this.getSingleString = getSingle;
        }

        protected abstract T Create(IDataReader reader);
        protected abstract void SetParameters(IDbCommand command, T model);
        protected abstract IDbConnection GetConnection();
        protected abstract IDataParameter CreateParameter(string name, object value);

        protected virtual IEnumerable<T> GetImpl(string commandString, params IDataParameter[] parameters)
        {
            return GetValuesImpl<T>(commandString, Create, parameters);
        }

        protected virtual TValue GetValuesImpl<TValue>(string commandString, params IDataParameter[] parameters)
        {
            return GetValuesImpl<TValue>(commandString, reader => (TValue)reader.GetValue(0), parameters).FirstOrDefault();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected virtual IEnumerable<TValue> GetValuesImpl<TValue>(string commandString, Func<IDataReader, TValue> selector, params IDataParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = commandString;
                parameters.ForEach(p => command.Parameters.Add(p));
                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return selector(reader);
                    }
                }
            }
        }

        protected virtual int SaveImpl(string commandString, params T[] models)
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                models.ForEach(m =>
                {
                    SaveModel(command, m);
                    count++;
                });
            }
            return count;
        }

        protected virtual void SaveModel(IDbCommand command, T model)
        {
            SetParameters(command, model);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetImpl(getAllString).ToList();
        }

        public virtual int Save(params T[] models)
        {
            return SaveImpl(saveString, models);
        }

        public virtual T GetSingle(string ID)
        {
            return GetImpl(getSingleString, CreateParameter("@ID", ID)).FirstOrDefault();
        }
    }
}
