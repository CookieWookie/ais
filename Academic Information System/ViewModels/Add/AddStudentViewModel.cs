using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class AddStudentViewModel : BaseAddViewModel<Student>
    {
        private readonly IRepository<Student> repository;
        private readonly Student original;
        private readonly string windowName;

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Student Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get { throw new NotImplementedException(); }
        }
        
        public AddStudentViewModel(IRepository<Student> repository)
            : this(repository, new Student())
        {
            this.windowName = "Pridaj: Študent";
        }

        public AddStudentViewModel(IRepository<Student> repository, Student original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.windowName = "Uprav: Študent";
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
