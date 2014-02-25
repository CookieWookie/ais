// Assigned to: Peter Gába

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories.Import
{
    public class StudyProgrammeImportParser : IImportManager<StudyProgramme>
    {
        private readonly IRepository<StudyProgramme> repository;

        public IRepository<StudyProgramme> Repository
        {
            get { return repository; }
        }

        public StudyProgrammeImportParser(IRepository<StudyProgramme> repository)
        {
            repository.ThrowIfNull("repository");
            this.repository = repository;
        }

        public void ParseFile(string file)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
