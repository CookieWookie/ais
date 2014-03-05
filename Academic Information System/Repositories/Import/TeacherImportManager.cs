// Assigned to: Peter Gába
// štruktúra: ID; Title; Name; Lastname; TitleSuffix; SubjectID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.IO;
namespace AiS.Repositories.Import
{
    public class TeacherImportManager : IImportManager<Teacher>
    {
        private readonly IRepository<Teacher> repository;
        List<Teacher> teachers = new List<Teacher>();
        public IRepository<Teacher> Repository
        {
            get { return repository; }
        }

        public TeacherImportManager(IRepository<Teacher> repository)
        {
            repository.ThrowIfNull("repository");
            this.repository = repository;
        }

        public void ParseFile(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string riadok = sr.ReadLine();
                while (riadok != null)
                {
                    string[] delenyRiadok = riadok.Split(';');
                    Teacher t = new Teacher();
                    t.ID = delenyRiadok[0];
                    t.Title = delenyRiadok[1];
                    t.Name = delenyRiadok[2];
                    t.Lastname = delenyRiadok[3];
                    t.TitleSuffix = delenyRiadok[4];
                    Subject sub = new Subject();
                    sub.ID = delenyRiadok[5];
                    t.Teaches.Add(sub);
                    if (!teachers.Contains(t))
                        teachers.Add(t);
                    else
                    {
                        foreach (Teacher x in teachers)
                        {
                            if (x.Equals(t))
                            {
                                x.Teaches.Add(sub);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Save()
        {
            Repository.Save(teachers.ToArray());
            teachers.Clear();
        }
    }
}
