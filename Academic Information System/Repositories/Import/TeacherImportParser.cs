// Assigned to: Adam Polák

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories.Import
{
    public class TeacherImportParser : IImportManager<Teacher>
    {
        private readonly IRepository<Teacher> repository;

        public IRepository<Teacher> Repository
        {
            get { return repository; }
        }

        public TeacherImportParser(IRepository<Teacher> repository)
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
