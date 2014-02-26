﻿// Assigned to: Adam Polák
// štruktúra: ID; Time; SubjectID; TeacherID; StudentID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;

namespace AiS.Repositories.Import
{
    public class ExamImportManager : IImportManager<Exam>
    {
        private readonly IRepository<Exam> repository;

        public IRepository<Exam> Repository
        {
            get { return repository; }
        }

        public ExamImportManager(IRepository<Exam> repository)
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
