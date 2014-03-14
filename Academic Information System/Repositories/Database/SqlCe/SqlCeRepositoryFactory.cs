using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.Configuration;

namespace AiS.Repositories.Database.SqlCe
{
    public class SqlCeRepositoryFactory : IRepositoryFactory
    {
        private IExamRepository examRepository;
        private IStudentRepository studentRepository;
        private IStudyProgrammeRepository studyProgrammeRepository;
        private ISubjectRepository subjectRepository;
        private ITeacherRepository teacherRepository;

        public IExamRepository ExamRepository
        {
            get { return this.examRepository ?? (this.examRepository = new ExamRepository(this.ConnectionString, this.StudentRepository, this.TeacherRepository, this.SubjectRepository)); }
        }

        public IStudentRepository StudentRepository
        {
            get { return this.studentRepository ?? (this.studentRepository = new StudentRepository(this.ConnectionString, this.StudyProgrammeRepository)); }
        }

        public IStudyProgrammeRepository StudyProgrammeRepository
        {
            get { return this.studyProgrammeRepository ?? (this.studyProgrammeRepository = new StudyProgrammeRepository(this.ConnectionString)); }
        }

        public ISubjectRepository SubjectRepository
        {
            get { return this.subjectRepository ?? (this.subjectRepository = new SubjectRepository(this.ConnectionString)); }
        }

        public ITeacherRepository TeacherRepository
        {
            get { return this.teacherRepository ?? (this.teacherRepository = new TeacherRepository(this.ConnectionString, this.SubjectRepository)); }
        }

        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["sqlce"].ConnectionString; }
        }

        public SqlCeRepositoryFactory()
        {
        }
    }
}
