// Assigned to: Adam Polák
// štruktúra: ID; Time; SubjectID; TeacherID; StudentID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.IO;

namespace AiS.Repositories.Import
{
    public class ExamImportManager : IImportManager<Exam>
    {
        private readonly IRepository<Exam> repository;
        List<Exam> exams = new List<Exam>();
        public IRepository<Exam> Repository
        {
            get { return repository; }
        }

        public ExamImportManager(IRepository<Exam> repository)
        {
            repository.ThrowIfNull("repository");
            this.repository = repository;
        }

        public void ParseFile(string file)
        {
            this.ParseFile(file, Encoding.Default);
        }
        public void ParseFile(string file, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(file, encoding))
            {
                string riadok = sr.ReadLine();
                while (riadok != null)
                {
                    string[] delenyRiadok = riadok.Split(';');
                    Exam e = new Exam();
                    e.ID = delenyRiadok[0];
                    e.Time = DateTime.Parse(delenyRiadok[1]);
                    Subject sub = new Subject();
                    sub.ID = delenyRiadok[2];
                    e.Subject = sub;
                    Teacher t = new Teacher();
                    t.ID = delenyRiadok[3];
                    e.Teacher = t;
                    Student s = new Student();
                    s.ID = delenyRiadok[4];

                    e.SignedStudents.Add(s);
                    if (!exams.Contains(e))
                    {
                        exams.Add(e);
                    }
                    else
                    {
                        foreach (Exam x in exams)
                        {
                            if (x.Equals(e))
                            {
                                x.SignedStudents.Add(s);
                                break;
                            }
                        }
                    }
                    riadok = sr.ReadLine();
                }
            }
        }
        public void Save()
        {
            Repository.Save(exams.ToArray());
            exams.Clear();
        }
    }
}
