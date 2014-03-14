using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class AddTeacherViewModel : BaseAddViewModel<Teacher>
    {
        private readonly IRepository<Teacher> repository;
        private readonly Teacher original;
        private readonly string windowName;

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Teacher Original
        {
            get { return this.original; }
        }
        public override Teacher WorkingCopy
        {
            get { throw new NotImplementedException(); }
        }
        public override bool HasChanged
        {
            get { throw new NotImplementedException(); }
        }

        public AddTeacherViewModel(IRepository<Teacher> repository)
            : this(repository, new Teacher())
        {
            this.windowName = "Pridaj: Učiteľ";
        }

        public AddTeacherViewModel(IRepository<Teacher> repository, Teacher original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.windowName = "Uprav: Učiteľ";
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
