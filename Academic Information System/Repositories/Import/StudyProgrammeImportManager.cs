// Assigned to: Peter Gába
// štruktúra: ID; Name; Length; StudyType
// StudyType bude číslo, 0 = bakalár a 1 = magister

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.IO;

namespace AiS.Repositories.Import
{
    public class StudyProgrammeImportManager : IImportManager<StudyProgramme>
    {
        private readonly IRepository<StudyProgramme> repository;
        List<StudyProgramme> studyProgrammes = new List<StudyProgramme>();

        public IRepository<StudyProgramme> Repository
        {
            get { return repository; }
        }

        public StudyProgrammeImportManager(IRepository<StudyProgramme> repository)
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
                    StudyProgramme sp = new StudyProgramme();
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
                    {
                        studyProgrammes.Add(sp);
                    }
                    riadok = sr.ReadLine();
                }
            }
        }

        public void Save()
        {
            Repository.Save(studyProgrammes.ToArray());
            studyProgrammes.Clear();
        }
    }
}
