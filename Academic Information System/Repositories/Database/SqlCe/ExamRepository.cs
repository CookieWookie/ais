﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.Data.SqlServerCe;

namespace AiS.Repositories.Database.SqlCe
{
    public class ExamRepository : BaseSqlCeDatabaseRepository<Exam>, IExamRepository
    {
        private const string SELECT = "SELECT [ID], [Time], [SubjectID], [TeacherID] FROM [Exams]";
        private const string SELECT_SINGLE = SELECT + " WHERE [ID] = @id";
        private const string SELECT_BYSUBJECT = SELECT + " WHERE [SubjectID] = @subjectId";
        private const string SELECT_BYTEACHER = SELECT + " WHERE [TeacherID] = @teacherId";
        private const string UPDATE = "UPDATE [Exams] SET [Time] = @time, [SubjectID] = @subjectId, [TeacherID] = @teacherId WHERE [ID] = @id; ";
        private const string INSERT = "INSERT INTO [Exams] ([ID], [Time], [SubjectID], [TeacherID]) VALUES (@id, @time, @subjectId, @teacherId);";
        private const string SAVE_STUDENTS = "INSERT INTO [StudentsSignedToExam] ([StudentID], [ExamID]) VALUES (@studentId, @id)";
        private const string DELETE = "DELETE FROM [Exams] WHERE [ID] = @id";

        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly ISubjectRepository subjectRepository;

        public IStudentRepository StudentRepository
        {
            get { return studentRepository; }
        }
        public ITeacherRepository TeacherRepository
        {
            get { return teacherRepository; }
        }
        public ISubjectRepository SubjectRepository
        {
            get { return subjectRepository; }
        }

        public ExamRepository(string commandString, IStudentRepository studentRepository, ITeacherRepository teacherRepository, ISubjectRepository subjectRepository)
            : base(commandString, SELECT_SINGLE, SELECT, INSERT, UPDATE, DELETE)
        {
            studentRepository.ThrowIfNull("studentRepository");
            teacherRepository.ThrowIfNull("teacherRepository");
            subjectRepository.ThrowIfNull("subjectRepository");

            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
            this.subjectRepository = subjectRepository;
        }

        protected override Exam Create(System.Data.IDataReader reader)
        {
            return new Exam
            {
                ID = reader.GetString(0),
                Time = reader.GetDateTime(1),
                Subject = SubjectRepository.GetSingle(reader.GetString(2)),
                Teacher = TeacherRepository.GetSingle(reader.GetString(3)),
                SignedStudents = StudentRepository.GetByExam(reader.GetString(0)).ToList()
            };
        }

        protected override void SetParameters(System.Data.IDbCommand command, Exam model)
        {
            command.Parameters.Add(CreateParameter("@id", model.ID));
            command.Parameters.Add(CreateParameter("@time", model.Time));
            command.Parameters.Add(CreateParameter("@subjectId", model.Subject.ID));
            command.Parameters.Add(CreateParameter("@teacherId", model.Teacher.ID));
        }

        public IEnumerable<Exam> GetBySubject(Subject subject)
        {
            subject.ThrowIfNull("subject");
            return GetBySubject(subject.ID);
        }

        public IEnumerable<Exam> GetBySubject(string ID)
        {
            return GetImpl(SELECT_BYSUBJECT, CreateParameter("@subjectId", ID));
        }

        public IEnumerable<Exam> GetByTeacher(Teacher teacher)
        {
            teacher.ThrowIfNull("teacher");
            return GetByTeacher(teacher.ID);
        }

        public IEnumerable<Exam> GetByTeacher(string ID)
        {
            return GetImpl(SELECT_BYTEACHER, CreateParameter("@teacherId", ID));
        }

        public override int Save(params Exam[] models)
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
                            if (string.IsNullOrWhiteSpace(m.ID))
                            {
                                m.ID = this.GetID();
                            }
                            SaveModel(command, m);

                            this.SetParameters(command, m);
                            command.CommandText = "DELETE FROM [StudentsSignedToExam] WHERE [ExamID] = @id";
                            command.ExecuteNonQuery();

                            command.CommandText = ExamRepository.SAVE_STUDENTS;
                            command.Parameters.Add("@studentId", System.Data.SqlDbType.NVarChar);
                            foreach (var s in m.SignedStudents)
                            {
                                command.Parameters["@studentId"].Value = s.ID;
                                command.ExecuteNonQuery();
                            }
                            command.Parameters.Clear();
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
