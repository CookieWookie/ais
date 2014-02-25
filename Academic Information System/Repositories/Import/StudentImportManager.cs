// Assigned to: Adam Polák

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories.Import
{
    public class StudentImportManager : IImportManager<Student>
    {
        private readonly IRepository<Student> repository;

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
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
