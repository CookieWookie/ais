﻿// Assigned to: Peter Gába

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories.Import
{
    public class SubjectImportParser : IImportManager<Subject>
    {
        private readonly IRepository<Subject> repository;

        public IRepository<Subject> Repository
        {
            get { return repository; }
        }

        public SubjectImportParser(IRepository<Subject> repository)
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
