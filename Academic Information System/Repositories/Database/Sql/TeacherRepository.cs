using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using AiS.Models;

namespace AiS.Repositories.Database.Sql
{
    public class TeacherRepository : BaseSqlDatabaseRepository<Teacher>, ITeacherRepository
    {
        private const string SELECT = "SELECT [ID], [Name], [Semester] FROM [Subjects]";
        private const string SELECT_SINGLE = SELECT + " WHERE [ID] = @id";
        private const string SELECT_BYSEMESTER = SELECT + " WHERE [Semester] = @semester;";
        private const string SELECT_BYTEACHER = SELECT + " WHERE [ID] IN (SELECT [SubjectID] FROM [Teachers] WHERE [ID] = @teacherId)";
        private const string SAVE =
            "IF EXISTS(" + SELECT + " WHERE [ID] = @id) " +
            "UPDATE [Subjects] SET [Name] = @name, [Semester] = @semester WHERE [ID] = @id; " +
            "ELSE " +
            "INSERT INTO [Subjects] ([ID], [Name], [Semester]) VALUES (@id, @name, @semester);";

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
                Title_Suffix = reader.GetString(4),
                Teaches = subjectRepository.GetTeachedBy(reader.GetString(0))
            };
        }

        protected override void SetParameters(System.Data.IDbCommand command, Teacher model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teacher> GetAllTeaching(Subject subject)
        {
            throw new NotImplementedException();
        }
    }
}
