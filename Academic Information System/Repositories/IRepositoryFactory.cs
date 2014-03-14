using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories
{
    public interface IRepositoryFactory
    {
        IExamRepository ExamRepository { get; }
        IStudentRepository StudentRepository { get; }
        IStudyProgrammeRepository StudyProgrammeRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ITeacherRepository TeacherRepository { get; }
    }
}
