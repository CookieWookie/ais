using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using AiS.Repositories.Import;

namespace AiS.Repositories
{
    public class ImportManagerFactory : IImportMangerFactory
    {
        private readonly IRepositoryFactory factory;
        private IImportManager<Exam> exam;
        private IImportManager<Student> student;
        private IImportManager<StudyProgramme> studyProgramme;
        private IImportManager<Subject> subject;
        private IImportManager<Teacher> teacher;

        public IImportManager<Exam> ExamImportManager
        {
            get { return this.exam ?? (this.exam = new ExamImportManager(factory.ExamRepository)); }
        }

        public IImportManager<Student> StudentImportManager
        {
            get { return this.student ?? (this.student = new StudentImportManager(factory.StudentRepository)); }
        }

        public IImportManager<StudyProgramme> StudyProgrammeImportManager
        {
            get { return this.studyProgramme ?? (this.studyProgramme = new StudyProgrammeImportManager(factory.StudyProgrammeRepository)); }
        }

        public IImportManager<Subject> SubjectImportManager
        {
            get { return this.subject ?? (this.subject = new SubjectImportManager(factory.SubjectRepository)); }
        }

        public IImportManager<Teacher> TeacherImportManager
        {
            get { return this.teacher ?? (this.teacher = new TeacherImportManager(factory.TeacherRepository)); }
        }

        public ImportManagerFactory(IRepositoryFactory factory)
        {
            factory.ThrowIfNull("factory");
            this.factory = factory;
        }
    }
}
