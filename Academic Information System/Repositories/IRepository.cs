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
        IEnumerable<StudyProgramme> GetByStudyType(StudyType type);
    }

    public interface IStudentRepository : IRepository<Student>
    {
        IStudyProgrammeRepository StudyProgrammeRepository { get; }

        IEnumerable<Student> GetByStudyProgramme(StudyProgramme programme);
        IEnumerable<Student> GetByStudyProgramme(string ID);
        IEnumerable<Student> GetByExam(Exam exam);
        IEnumerable<Student> GetByExam(string ID);
    }

    public interface ISubjectRepository : IRepository<Subject>
    {
        IEnumerable<Subject> GetbySemester(int semester);
        IEnumerable<Subject> GetByTeacher(Teacher teacher);
        IEnumerable<Subject> GetByTeacher(string ID);
    }

    public interface ITeacherRepository : IRepository<Teacher>
    {
        ISubjectRepository SubjectRepository { get; }

        IEnumerable<Teacher> GetBySubject(Subject subject);
        IEnumerable<Teacher> GetBySubject(string ID);
    }

    public interface IExamRepository : IRepository<Exam>
    {
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ISubjectRepository SubjectRepository { get; }

        IEnumerable<Exam> GetBySubject(Subject subject);
        IEnumerable<Exam> GetBySubject(string ID);
        IEnumerable<Exam> GetByTeacher(Teacher teacher);
        IEnumerable<Exam> GetByTeacher(string ID);
    }
}
