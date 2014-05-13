// Assigned to: Adam Polák
// štruktúra: ID; Name; Lastname; Semester; DateOfBirth; StudyProgrammeID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.IO;

namespace AiS.Repositories.Import
{
    // nejaky dalsi komentar
    public class StudentImportManager : IImportManager<Student>
    {
        private readonly IRepository<Student> repository;

        List<Student> students = new List<Student>();

        public IRepository<Student> Repository
        {
            get { return repository; }
        }

        public StudentImportManager(IRepository<Student> repository)
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
                    Student s = new Student();
                    s.ID = delenyRiadok[0];
                    s.Name = delenyRiadok[1];
                    s.Lastname = delenyRiadok[2];
                    s.Semester = Convert.ToInt32(delenyRiadok[3]);
                    s.DateOfBirth = Convert.ToDateTime(delenyRiadok[4]);

                    StudyProgramme sp = new StudyProgramme();
                    sp.ID = delenyRiadok[5];

                    s.StudyProgramme = sp;

                    if (!students.Contains(s))
                    {
                        students.Add(s);
                    }
                    riadok = sr.ReadLine();
                }
            }
        }

        public void Save()
        {
            Repository.Save(students.ToArray());
            students.Clear();

        }
    }
}
