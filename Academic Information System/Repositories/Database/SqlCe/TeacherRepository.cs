using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using AiS.Models;

namespace AiS.Repositories.Database.SqlCe
{
    public class TeacherRepository : BaseSqlCeDatabaseRepository<Teacher>, ITeacherRepository
    {
        private const string SELECT = "SELECT [ID], [Title], [Name], [Lastname], [TitleSuffix] FROM [Teachers]";
        private const string SELECT_SINGLE = SELECT + " WHERE [ID] = @id";
        private const string SELECT_BYSUBJECT = SELECT + " JOIN [SubjectTeachers] WHERE [SubjectID] = @subjectId;";
        private const string SAVE =
            "IF EXISTS(" + SELECT + " WHERE [ID] = @id) " +
            "UPDATE [Teachers] SET [Title] = @title, [Name] = @name, [Lastname] = @lastname, [TitleSuffix] = @titleSuffix WHERE [ID] = @id; " +
            "ELSE " +
            "INSERT INTO [Teachers] ([ID], [Title], [Name], [Lastname], [TitleSuffix]) VALUES (@id, @title, @name, @lastanme, @titleSuffix);";
        private const string SAVE_SUBJECTS = "INSERT INTO [SubjectTeachers] ([SubjectID], [TeacherID]) VALUES (@subjectId, @id);";

        private readonly ISubjectRepository subjectRepository;

        public ISubjectRepository SubjectRepository
        {
            get { return subjectRepository; }
        }

        public TeacherRepository(string connectionString, ISubjectRepository subjectRepository)
            : base(connectionString, SELECT_SINGLE, SELECT, SAVE)
        {
            subjectRepository.ThrowIfNull("subjectRepository");
            this.subjectRepository = subjectRepository;
        }

        protected override Teacher Create(System.Data.IDataReader reader)
        {
            return new Teacher
            {
                ID = reader.GetString(0),
                Title = reader.GetString(1),
                Name = reader.GetString(2),
                Lastname = reader.GetString(3),
                TitleSuffix = reader.GetString(4),
                Teaches = subjectRepository.GetByTeacher(reader.GetString(0))
            };
        }

        protected override void SetParameters(System.Data.IDbCommand command, Teacher model)
        {
            command.Parameters.Add(CreateParameter("@id", model.ID));
            command.Parameters.Add(CreateParameter("@title", model.Title));
            command.Parameters.Add(CreateParameter("@name", model.Name));
            command.Parameters.Add(CreateParameter("@lastname", model.Lastname));
            command.Parameters.Add(CreateParameter("@titleSuffix", model.TitleSuffix));
        }

        public IEnumerable<Teacher> GetBySubject(Subject subject)
        {
            subject.ThrowIfNull("subject");
            return GetBySubject(subject.ID);
        }

        public IEnumerable<Teacher> GetBySubject(string ID)
        {
            return GetImpl(SELECT_BYSUBJECT, CreateParameter("@subjectId", ID));
        }

        public override int Save(params Teacher[] models)
        {
            int count = 0;
            using (SqlConnection connection = (SqlConnection)GetConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                using (SqlCommand command = new SqlCommand("DELETE FROM [SubjectTeachers] WHERE [TeacherID] = @id", connection, transaction))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        models.ForEach(m =>
                        {
                            command.CommandText = SAVE;
                            SaveModel(command, m);
                            foreach (var s in m.Teaches)
                            {
                                command.Parameters.AddWithValue("@subjectId", s.ID);
                                command.ExecuteNonQuery();
                            }
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
