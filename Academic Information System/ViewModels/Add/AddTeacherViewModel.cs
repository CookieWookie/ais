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
        private readonly ITeacherRepository repository;
        private readonly Teacher original;
        private readonly string windowName;

        private readonly ICommand addSubjectCommand;
        private readonly ICommand removeSubjectCommand;
        private List<Subject> subjects;

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Teacher Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Subject> Subjects
        {
            get { throw new NotImplementedException(); }
        }

        public AddTeacherViewModel(ITeacherRepository repository)
            : this(repository, new Teacher())
        {
            this.windowName = "Pridaj: Učiteľ";
        }

        public AddTeacherViewModel(ITeacherRepository repository, Teacher original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");

            this.windowName = "Uprav: Učiteľ";
            this.repository = repository;
            this.original = original;

            this.subjects = this.repository.SubjectRepository.GetAll().ToList();
            
            this.ResetToDefault();
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
