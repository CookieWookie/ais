using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class AddExamViewModel : BaseAddViewModel<Exam>
    {
        private readonly IRepository<Exam> repository;
        private readonly Exam original;

        public override Exam Original
        {
            get { return this.original; }
        }
        public override Exam WorkingCopy
        {
            get { throw new NotImplementedException(); }
        }
        public override string WindowName
        {
            get { return "Pridaj: Skúška"; }
        }
        public override bool HasChanged
        {
            get { throw new NotImplementedException(); }
        }

        public AddExamViewModel(IRepository<Exam> repository, Exam original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.repository = repository;
            this.original = original;
        }

        public override void ResetToDefault()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
