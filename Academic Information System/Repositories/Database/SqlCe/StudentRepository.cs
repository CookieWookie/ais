using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.Data.SqlServerCe;

namespace AiS.Repositories.Database.SqlCe
{
    public class StudentRepository : BaseSqlCeDatabaseRepository<Student>, IStudentRepository
    {
        private const string SELECT = "SELECT [ID], [Name], [Lastname], [Semester], [DateOfBirth], [StudyProgrammeID] FROM [Students]";
        private const string SELECT_SINGLE = SELECT + " WHERE [ID] = @id";
        private const string SELECT_BYPROGRAMME = SELECT + " WHERE [StudyProgrammeID] = @studyProgrammeId";
        private const string SELECT_BYEXAM = SELECT + " JOIN [StudentsSignedToExam] ON [StudentID] = [ID] WHERE [ExamID] = @examId";
        private const string UPDATE = "UPDATE [Students] SET [Name] = @name, [Lastname] = @lastname, [Semester] = @semester, [DateOfBirth] = @dateOfBirth, [StudyProgrammeID] = @studyProgrammeId WHERE [ID] = @id; ";
        private const string INSERT = "INSERT INTO [Students] ([ID], [Name], [Lastname], [Semester], [DateOfBirth], [StudyProgrammeID]) VALUES (@id, @name, @lastname, @semester, @dateOfBirth, @studyProgrammeId);";
        private const string DELETE = "DELETE FROM [Students] WHERE [ID] = @id";
        private readonly IStudyProgrammeRepository studyProgrammeRepository;

        public IStudyProgrammeRepository StudyProgrammeRepository
        {
            get { return studyProgrammeRepository; ; }
        }

        public StudentRepository(string connectionString, IStudyProgrammeRepository studyProgrammeRepository)
            : base(connectionString, SELECT_SINGLE, SELECT, INSERT, UPDATE, DELETE)
        {
            studyProgrammeRepository.ThrowIfNull("studyProgrammeRepository");
            this.studyProgrammeRepository = studyProgrammeRepository;
        }

        protected override Student Create(System.Data.IDataReader reader)
        {
            return new Student
            {
                ID = reader.GetString(0),
                Name = reader.GetString(1),
                Lastname = reader.GetString(2),
                Semester = reader.GetInt32(3),
                DateOfBirth = reader.GetDateTime(4),
                StudyProgramme = StudyProgrammeRepository.GetSingle(reader.GetString(5))
            };
        }

        protected override void SetParameters(System.Data.IDbCommand command, Student model)
        {
            command.Parameters.Add(CreateParameter("@id", model.ID));
            command.Parameters.Add(CreateParameter("@name", model.Name));
            command.Parameters.Add(CreateParameter("@lastname", model.Lastname));
            command.Parameters.Add(CreateParameter("@semester", model.Semester));
            command.Parameters.Add(CreateParameter("@dateOfBirth", model.DateOfBirth));
            command.Parameters.Add(CreateParameter("@studyProgrammeId", model.StudyProgramme.ID));
        }

        public IEnumerable<Student> GetByStudyProgramme(StudyProgramme programme)
        {
            programme.ThrowIfNull("programme");
            return GetByStudyProgramme(programme.ID);
        }

        public IEnumerable<Student> GetByStudyProgramme(string ID)
        {
            return GetImpl(SELECT_BYPROGRAMME, CreateParameter("@studyProgrammeId", ID));
        }

        public IEnumerable<Student> GetByExam(Exam exam)
        {
            exam.ThrowIfNull("exam");
            return GetByExam(exam.ID);
        }

        public IEnumerable<Student> GetByExam(string ID)
        {
            return GetImpl(SELECT_BYEXAM, CreateParameter("@examId", ID));
        }

        public override int Save(params Student[] models)
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
