using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiS.Models;

namespace AiS.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetSingle(string ID);
        IEnumerable<T> GetAll();

        int Save(params T[] models);
    }

    public interface IStudyProgrammeRepository : IRepository<StudyProgramme>
    {
        IEnumerable<StudyProgramme> GetByType(StudyType type);
    }

    public interface IStudentRepository : IRepository<Student>
    {
        IStudyProgrammeRepository StudyProgrammeRepository { get; }

        IEnumerable<Student> GetStudying(StudyProgramme programme);
        IEnumerable<Student> GetSignedTo(Exam exam);
        IEnumerable<Student> GetUnsignedFrom(Exam exam);
    }

    public interface ISubjectRepository : IRepository<Subject>
    {
        IEnumerable<Subject> GetInSemester(int semester);
        IEnumerable<Subject> GetTeachedBy(Teacher teacher);
    }

    public interface ITeacherRepository : IRepository<Teacher>
    {
        ISubjectRepository SubjectRepository { get; }

        IEnumerable<Teacher> GetAllTeaching(Subject subject);
    }

    public interface IExamRepository : IRepository<Exam>
    {
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ISubjectRepository SubjectRepository { get; }

        IEnumerable<Exam> GetAllFrom(Subject subject);
        IEnumerable<Exam> GetAllTeachedBy(Teacher teacher);
    }
}
