using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.Data.SqlClient;

namespace AiS.Repositories.Database.Sql
{
    public class SubjectRepository : BaseSqlDatabaseRepository<Subject>, ISubjectRepository
    {
        private const string SELECT = "SELECT [ID], [Name], [Semester] FROM [Subjects]";
        private const string SELECT_SINGLE = SELECT + " WHERE [ID] = @id";
        private const string SELECT_BYSEMESTER = SELECT + " WHERE [Semester] = @semester;";
        private const string SELECT_BYTEACHER = SELECT + " JOIN [SubjectTeachers] ON [ID] = [SubjectID] WHERE [TeacherID] = @teacherId";
        private const string SAVE =
            "IF EXISTS(" + SELECT + " WHERE [ID] = @id) " +
            "UPDATE [Subjects] SET [Name] = @name, [Semester] = @semester WHERE [ID] = @id; " +
            "ELSE " +
            "INSERT INTO [Subjects] ([ID], [Name], [Semester]) VALUES (@id, @name, @semester);";

        public SubjectRepository(string connectionString)
            : base(connectionString, SELECT_SINGLE, SELECT, SAVE)
        {
        }

        protected override Subject Create(System.Data.IDataReader reader)
        {
            return new Subject
            {
                ID = reader.GetString(0),
                Name = reader.GetString(1),
                Semester = reader.GetInt32(2)
            };
        }

        protected override void SetParameters(System.Data.IDbCommand command, Subject model)
        {
            command.Parameters.Add(CreateParameter("@id", model.ID));
            command.Parameters.Add(CreateParameter("@name", model.Name));
            command.Parameters.Add(CreateParameter("@semester", model.Semester));
        }

        public IEnumerable<Subject> GetbySemester(int semester)
        {
            return GetImpl(SELECT_BYSEMESTER, CreateParameter("@semester", semester));
        }

        public IEnumerable<Subject> GetByTeacher(Teacher teacher)
        {
            teacher.ThrowIfNull("teacher");
            return GetByTeacher(teacher.ID);
        }

        public IEnumerable<Subject> GetByTeacher(string ID)
        {
            return GetImpl(SELECT_BYTEACHER, CreateParameter("@teacherId", ID));
        }

        public override int Save(params Subject[] models)
        {
            models.ForEach(m =>
            {
                if (string.IsNullOrWhiteSpace(m.ID))
                    m.ID = this.GetID();
            });
            return base.Save(models);
        }
    }
}
