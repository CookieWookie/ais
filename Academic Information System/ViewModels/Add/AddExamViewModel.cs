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
        private readonly IEqualityComparer<IEnumerable<Student>> studentComparer = new CollectionComparer<Student>();
        private readonly IExamRepository repository;
        private readonly Exam original;
        private readonly string windowName;
        private readonly ICommand selectTeacherCommand;
        private readonly ICommand selectSubjectCommand;

        private readonly ObservableCollection<Teacher> teachers;
        private readonly ObservableCollection<Subject> subjects;
        
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

        public ObservableCollection<Teacher> Teachers
        {
            get { return this.teachers; }
        }
        public ObservableCollection<Subject> Subjects
        {
            get { return this.subjects; }
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
            get { return !this.original.Time.Equals(this.Time) || !this.original.Subject.Equals(this.Subject) || !this.original.Teacher.Equals(this.Teacher) || !studentComparer.Equals(this.SignedStudents, this.original.SignedStudents); }//toto je moje
        }
        public ICommand SelectTeacherCommand
        {
            get { return this.selectTeacherCommand; }
        }
        public ICommand SelectSubjectCommand
        {
            get { return this.selectSubjectCommand; }
        }

        public AddExamViewModel(IExamRepository repository)
            : this(repository, new Exam())
        {
            this.windowName = "Pridaj: Skúška";
        }

        public AddExamViewModel(IExamRepository repository, Exam original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");

            this.windowName = "Uprav: Skúška";
            this.repository = repository;
            this.original = original;
            this.selectSubjectCommand = new RelayCommand(o => this.Subject = (Subject)o, o => o is Subject);
            this.selectTeacherCommand = new RelayCommand(o => this.Teacher = (Teacher)o, o => o is Teacher);

            this.teachers = new ObservableCollection<Teacher>(this.repository.TeacherRepository.GetAll());
            this.teachers.CollectionChanged += (sender, e) => this.OnPropertyChanged("Teachers");
            this.subjects = new ObservableCollection<Subject>(this.repository.SubjectRepository.GetAll());
            this.subjects.CollectionChanged += (sender, e) => this.OnPropertyChanged("Subjects");
        }

        public override void ResetToDefault()
        {
            this.Time = this.original.Time;
            this.Subject = this.original.Subject.Clone();
            this.Teacher = this.original.Teacher.Clone();
            this.SignedStudents = this.original.SignedStudents;            
        }

        public override void Save()
        {
            Exam exam= new Exam();
            exam.Time = this.Time;
            exam.Subject = this.Subject.Clone();
            exam.Teacher = this.Teacher.Clone();
            exam.SignedStudents = this.SignedStudents;
            repository.Save(exam);            
        }

        private void SetCollection(IList<Student> value)
        {
            this.students = value == null ? new ObservableCollection<Student>() : new ObservableCollection<Student>(value);
            this.students.CollectionChanged += (sender, e) => this.OnPropertyChanged("SignedStudents");
        }
    }
}
