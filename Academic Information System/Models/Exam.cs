using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AiS.Models
{
    /// <summary>
    /// Trieda na ukladanie skusok.
    /// </summary>
    public class Exam : ObservableObject, IEquatable<Exam>
    {
        private string id;
        private DateTime time;
        private Subject subject;
        private Teacher teacher;
        private ObservableCollection<Student> students;

        public string ID
        {
            get { return this.id ?? ""; }
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }
        public DateTime Time
        {
            get { return this.time; }
            set
            {
                if (value != this.time)
                {
                    this.time = value;
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
                if (!Comparers.Equals(this.subject, value))
                {
                    this.subject = value;
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
                if (!Comparers.Equals(this.teacher, value))
                {
                    this.teacher = value;
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
                    this.SetCollection(value);
                    this.OnPropertyChanged("SignedStudents");
                }
            }
        }

        public Exam()
        {
        }

        private void SetCollection(IList<Student> value)
        {
            this.students = value == null ? new ObservableCollection<Student>() : new ObservableCollection<Student>(value);
            this.students.CollectionChanged += (sender, e) => this.OnPropertyChanged("SignedStudents");
        }

        public bool Equals(Exam other)
        {
            return other != null && other.ID == ID;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Exam);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public Exam Clone()
        {
            return new Exam
            {
                ID = ID,
                Time = Time,
                Subject = Subject.Clone(),
                Teacher = Teacher.Clone(),
                SignedStudents = SignedStudents.Select(s => s.Clone()).ToList()
            };
        }
    }
}
