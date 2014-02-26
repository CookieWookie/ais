﻿// Assigned to: Peter Gába
// štruktúra: ID; Name; Length; StudyType
// StudyType bude číslo, 0 = bakalár a 1 = magister

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories.Import
{
    public class StudyProgrammeImportManager : IImportManager<StudyProgramme>
    {
        private readonly IRepository<StudyProgramme> repository;

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
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
