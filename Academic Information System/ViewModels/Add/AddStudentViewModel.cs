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
        
        private string name;
        private string lastname;
        private int semester;
        private DateTime dateOfBirth;
        private StudyProgramme programme;

        
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Name");
                }
            }
        }
        public string Lastname
        {
            get { return this.lastname; }
            set
            {
                if (this.lastname != value)
                {
                    this.lastname = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Lastname");
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
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Semester");
                }
            }
        }
        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                if (this.dateOfBirth != value)
                {
                    this.dateOfBirth = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("DateOfBirth");
                }
            }
        }
        public StudyProgramme StudyProgramme
        {
            get { return this.programme; }
            set
            {
                if (this.programme != value || (this.programme != null && !this.programme.Equals(value)))
                {
                    this.programme = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("StudyProgramme");
                }
            }
        }

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
            get { return !this.original.Name.Equals(this.Name) || !this.original.Lastname.Equals(this.Lastname) || !this.original.Semester.Equals(this.Semester) || !this.original.DateOfBirth.Equals(this.DateOfBirth) || !this.original.StudyProgramme.Equals(this.StudyProgramme); }//toto je moje
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
            this.Name = this.original.Name;
            this.Lastname = this.original.Lastname;
            this.DateOfBirth = this.original.DateOfBirth;
            this.Semester = this.original.Semester;
            this.StudyProgramme = this.original.StudyProgramme;
        }

        public override void Save()
        {
            Student student = new Student();
            student.Name = this.Name;
            student.Lastname = this.Lastname;
            student.DateOfBirth = this.DateOfBirth;
            student.Semester = this.Semester;
            student.StudyProgramme = this.StudyProgramme;
            repository.Save(student);
        }
    }
}
