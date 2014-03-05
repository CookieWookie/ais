// Assigned to: Peter Gába
// štruktúra: ID; Name; Semester

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.IO;
namespace AiS.Repositories.Import
{
    public class SubjectImportManager : IImportManager<Subject>
    {
        private readonly IRepository<Subject> repository;
        List<Subject> subjects = new List<Subject>();

        public IRepository<Subject> Repository
        {
            get { return repository; }
        }

        public SubjectImportManager(IRepository<Subject> repository)
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
                    Subject sub = new Subject();
                    sub.ID = delenyRiadok[0];
                    sub.Name = delenyRiadok[1];
                    sub.Semester = Convert.ToInt32(delenyRiadok[3]);
                    if (!subjects.Contains(sub))
                        subjects.Add(sub);
                }
            }
        }

        public void Save()
        {
            Repository.Save(subjects.ToArray());
            subjects.Clear();

        }
    }
}
