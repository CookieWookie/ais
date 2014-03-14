using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories
{
    public interface IImportMangerFactory
    {
        IImportManager<Exam> ExamImportManager { get; }
        IImportManager<Student> StudentImportManager { get; }
        IImportManager<StudyProgramme> StudyProgrammeImportManager { get; }
        IImportManager<Subject> SubjectImportManager { get; }
        IImportManager<Teacher> TeacherImportManager { get; }
    }
}
