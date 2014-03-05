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
                    Teacher sp = new Teacher();
                    sp.ID = delenyRiadok[0];
                    sp.Name = delenyRiadok[1];
                    sp.Length = Convert.ToInt32(delenyRiadok[2]);
                    if (delenyRiadok[3] == "0")
                    {
                        sp.StudyType = StudyType.Bachelor;
                    }
                    else
                    {
                        sp.StudyType = StudyType.Magister;
                    }
                    if (!studyProgrammes.Contains(sp))
                        studyProgrammes.Add(sp);
                }
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
