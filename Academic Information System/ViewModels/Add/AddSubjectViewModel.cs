using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class AddSubjectViewModel : BaseAddViewModel<Subject>
    {
        private readonly IRepository<Subject> repository;
        private readonly Subject original;
        private readonly string windowName;

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Subject Original
        {
            get { return this.original; }
        }
        public override Subject WorkingCopy
        {
            get { throw new NotImplementedException(); }
        }
        public override bool HasChanged
        {
            get { throw new NotImplementedException(); }
        }

        public AddSubjectViewModel(IRepository<Subject> repository)
            : this(repository, new Subject())
        {
            this.windowName = "Pridaj: Predmet";
        }

        public AddSubjectViewModel(IRepository<Subject> repository, Subject original)
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.windowName = "Uprav: Predmet";
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
