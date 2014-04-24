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
        private readonly ICommand removeStudentCommand;
        private readonly ICommand addStudentCommand;
        private List<Student> students;

        private TimeSpan time;
        private DateTime date;
        private Subject subject;
        private Teacher teacher;
        private ObservableCollection<Student> signedStudents;

        public TimeSpan Time
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
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                if (value != this.date)
                {
                    this.date = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Date");
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
                if (!this.Subject.Equals(value))
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
                if (!this.Teacher.Equals(value))
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
                if (this.signedStudents == null)
                {
                    this.SetCollection(null);
                }
                return this.signedStudents;
            }
            set
            {
                if (value != this.signedStudents)
                {
                    SetCollection(value);
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("SignedStudents");
                }
            }
        }

        public IEnumerable<Teacher> Teachers
        {
            get { return this.repository.TeacherRepository.GetAll(); }
        }
        public IEnumerable<Subject> Subjects
        {
            get { return this.repository.SubjectRepository.GetAll(); }
        }
        public IEnumerable<Student> Students
        {
            get { return this.students.Except(this.SignedStudents); }
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
            get
            {
                return this.original.Time != this.Date.Add(Time) ||
                  !this.original.Subject.Equals(this.Subject) ||
                  !this.original.Teacher.Equals(this.Teacher) ||
                  !studentComparer.Equals(this.SignedStudents, this.original.SignedStudents);
            }
        }
        public ICommand SelectTeacherCommand
        {
            get { return this.selectTeacherCommand; }
        }
        public ICommand SelectSubjectCommand
        {
            get { return this.selectSubjectCommand; }
        }
        public ICommand RemoveStudentCommand
        {
            get { return this.removeStudentCommand; }
        }
        public ICommand AddStudentCommand
        {
            get { return this.addStudentCommand; }
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
            this.removeStudentCommand = new RelayCommand(o => this.SignedStudents.Remove((Student)o), o => o is Student);
            this.addStudentCommand = new RelayCommand(o => this.SignedStudents.Add((Student)o), o => o is Student);

            this.students = this.repository.StudentRepository.GetAll().ToList();
            this.ResetToDefault();
        }

        public override void ResetToDefault()
        {
            this.Time = this.original.Time.TimeOfDay;
            this.Date = this.original.Time.Date;
            this.Subject = this.original.Subject.Clone();
            this.Teacher = this.original.Teacher.Clone();
            this.SignedStudents = this.original.SignedStudents;
        }

        public override void Save()
        {
            Exam exam = new Exam();
            exam.ID = this.original.ID;
            exam.Time = this.Date.Add(this.Time);
            exam.Subject = this.Subject.Clone();
            exam.Teacher = this.Teacher.Clone();
            exam.SignedStudents = this.SignedStudents;
            repository.Save(exam);
            this.Close();
        }

        private void SetCollection(IList<Student> value)
        {
            value = value ?? new List<Student>();
            this.signedStudents = new ObservableCollection<Student>(value);
            this.signedStudents.CollectionChanged += (sender, e) =>
                {
                    this.OnPropertyChanged("SignedStudents");
                    this.OnPropertyChanged("Students");
                };
        }

        public override void Refresh()
        {
            this.OnPropertyChanged("Subjects");
            this.OnPropertyChanged("Teachers");
            base.Refresh();
        }
    }
}
