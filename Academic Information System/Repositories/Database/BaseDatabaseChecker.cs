using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;

namespace AiS.Repositories.Database
{
    public abstract class BaseDatabaseChecker
    {
        protected readonly string checkExists;
        protected readonly string connectionString;
        protected readonly string identifier;
        protected readonly Assembly assembly;

        protected BaseDatabaseChecker(string connectionString, string identifier)
        {
            this.connectionString = connectionString;
            this.identifier = identifier;
            this.assembly = Assembly.GetExecutingAssembly();
        }

        protected virtual string[] GetFiles()
        {
            List<string> contents = new List<string>();
            foreach (string name in assembly.GetManifestResourceNames())
            {
                if (name.Contains(identifier))
                {
                    contents.Add(name);
                }
            }
            return contents.ToArray();
        }

        protected virtual string GetContent(string file)
        {
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(file)))
            {
                return reader.ReadToEnd();
            }
        }

        protected abstract IDbConnection CreateConnection();
        protected abstract void CreateDatabase();

        protected virtual string GetVersion(string file)
        {
            return file.Substring(file.IndexOf(identifier) + identifier.Length).Replace(".sql", "");
        }

        protected virtual void Update(string[] files)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                using (var command = connection.CreateCommand())
                {
                    var param = command.CreateParameter();
                    param.ParameterName = "@version";
                    command.Parameters.Add(param);

                    command.Transaction = transaction;
                    foreach (string file in files)
                    {
                        string version = GetVersion(file);
                        command.Parameters["@version"] = version;
                        command.CommandText = checkExists;
                    }
                }
            }
        }

        public void Update()
        {
            var files = GetFiles();
        }
    }
}
