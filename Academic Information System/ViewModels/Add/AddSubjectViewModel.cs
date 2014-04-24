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
        private readonly ISubjectRepository repository;
        private readonly Subject original;
        private readonly string windowName;

        private string name;
        private int semester;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
        public int Semester
        {
            get { return this.semester; }
            set
            {
                if (this.semester != value)
                {
                    this.semester = value;
                    this.OnPropertyChanged("Semester");
                }
            }
        }

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Subject Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get
            {
                return this.original.Name != this.Name ||
                   this.original.Semester !=this.Semester; 
            }
        }

        public AddSubjectViewModel(ISubjectRepository repository)
            : this(repository, new Subject())
        {
            this.windowName = "Pridaj: Predmet";
        }

        public AddSubjectViewModel(ISubjectRepository repository, Subject original)
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");

            this.windowName = "Uprav: Predmet";
            this.repository = repository;
            this.original = original;
            this.ResetToDefault();
        }

        public override void ResetToDefault()
        {
            this.Semester = this.original.Semester;
            this.Name = this.original.Name;
        }

        public override void Save()
        {
            Subject subject = new Subject();
            subject.ID = this.original.ID;
            subject.Name = this.Name;
            subject.Semester = this.Semester;
            repository.Save(subject);
            this.Close();
        }
    }
}
