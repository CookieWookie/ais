using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AiS.ViewModels
{
    public class AddExamViewModel : BaseAddViewModel<Exam>
    {
        private readonly IRepository<Exam> repository;
        private readonly Exam original;
        private readonly string windowName;
        
        private DateTime time;
        private Subject subject;
        private Teacher teacher;
        private ObservableCollection<Student> students;

        public DateTime Time
        {
            get { return this.time; }
            set
            {
                if (value != this.time)
                {
                    this.time = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Time");
                }
            }
        }
        public Subject Subject
        {
            get
            {
                if (this.subject == null)
                {
                    this.subject = new Subject();
                }
                return this.subject;
            }
            set
            {
                if (!this.subject.Equals(value))
                {
                    this.subject = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Subject");
                }
            }
        }
        public Teacher Teacher
        {
            get
            {
                if (this.teacher == null)
                {
                    this.teacher = new Teacher();
                }
                return this.teacher;
            }
            set
            {
                if (!this.teacher.Equals(value))
                {
                    this.teacher = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Teacher");
                }
            }
        }
        public IList<Student> SignedStudents
        {
            get
            {
                if (this.students == null)
                {
                    this.SetCollection(null);
                }
                return this.students;
            }
            set
            {
                if (value != this.students)
                {
                    SetCollection(value);
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("SignedStudents");
                }
            }
        }

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Exam Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get { return this.original.Time!= this.Time || this.orginal.}//toto je moje
        }
        public ICommand SelectTeacherCommand
        {
            get { throw new NotImplementedException(); }
        }
        public ICommand SelectSubjectCommand
        {
            get { throw new NotImplementedException(); }
        }

        public AddExamViewModel(IRepository<Exam> repository)
            : this(repository, new Exam())
        {
            this.windowName = "Pridaj: Skúška";
        }

        public AddExamViewModel(IRepository<Exam> repository, Exam original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.windowName = "Uprav: Skúška";
            this.repository = repository;
            this.original = original;
        }

        public override void ResetToDefault()
        {
            throw new NotImplementedException();//toto je moje
        }

        public override void Save()
        {
            throw new NotImplementedException();// toto je moje
        }

        private void SetCollection(IList<Student> value)
        {
            this.students = value == null ? new ObservableCollection<Student>() : new ObservableCollection<Student>(value);
            this.students.CollectionChanged += (sender, e) => this.OnPropertyChanged("SignedStudents");
        }
    }
}
