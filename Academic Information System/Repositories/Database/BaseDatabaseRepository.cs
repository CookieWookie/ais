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
        protected const string AllowedCharacters =
            "abcdefghijklmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.-?:_!)([#&]<>*+$@";
        private static readonly Random rnd = new Random();

        protected readonly string getSingleString;
        protected readonly string getAllString;
        protected readonly string saveString;
        protected readonly string deleteString;
        protected readonly string connectionString;

        protected BaseDatabaseRepository(string connectionString, string getSingle, string getAll, string save, string delete)
        {
            if (string.IsNullOrWhiteSpace("connectionString"))
            {
                throw new ArgumentException("connectionString");
            }
            if (string.IsNullOrWhiteSpace(getAll))
            {
                throw new ArgumentException("getAll");
            }
            if (string.IsNullOrWhiteSpace(save))
            {
                throw new ArgumentException("save");
            }
            if (string.IsNullOrWhiteSpace(getSingle))
            {
                throw new ArgumentException("getSingle");
            }
            if (string.IsNullOrWhiteSpace(delete))
            {
                throw new ArgumentException("delete");
            }

            this.connectionString = connectionString;
            this.getAllString = getAll;
            this.saveString = save;
            this.getSingleString = getSingle;
            this.deleteString = delete;
        }

        protected abstract T Create(IDataReader reader);
        protected abstract void SetParameters(IDbCommand command, T model);
        protected abstract IDbConnection GetConnection();
        protected abstract IDataParameter CreateParameter(string name, object value);

        protected virtual string GetID()
        {
            string id = this.GenerateID();
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.Parameters.Add(CreateParameter("@id", id));
                while (command.ExecuteScalar() != null)
                {
                    ((IDataParameter)command.Parameters["@id"]).Value = (id = this.GenerateID());
                }
            }
            return id;
        }

        protected virtual string GenerateID()
        {
            char[] array = new char[8];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = AllowedCharacters[rnd.Next(0, AllowedCharacters.Length)];
            }
            return new string(array);
        }

        protected virtual IEnumerable<T> GetImpl(string commandString, params IDataParameter[] parameters)
        {
            return GetValuesImpl<T>(commandString, Create, parameters);
        }

        protected virtual TValue GetValueImpl<TValue>(string commandString, params IDataParameter[] parameters)
        {
            return GetValuesImpl<TValue>(commandString, reader => (TValue)reader.GetValue(0), parameters).FirstOrDefault();
        }

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
            using (var connection = this.GetConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = saveString;
                models.ForEach(m =>
                {
                    this.SaveModel(command, m);
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

        protected virtual void Exec(string commandString, params IDataParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = commandString;
                parameters.ForEach(p => command.Parameters.Add(p));
                connection.Open();
                command.ExecuteNonQuery();
            }
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
            return GetImpl(getSingleString, CreateParameter("@id", ID)).FirstOrDefault();
        }

        public void Remove(T model)
        {
            using (var connection = this.GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = this.deleteString;
                this.SetParameters(command, model);
                command.ExecuteNonQuery();
            }
        }
    }
}
