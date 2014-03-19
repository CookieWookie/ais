using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiS.Models
{
    public class Student : ObservableObject, IEquatable<Student>
    {
        private string id;
        private string name;
        private string lastname;
        private int semester;
        private DateTime dateOfBirth;
        private StudyProgramme programme;

        public string ID
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    // this.OnPropertyChanged("ID");
                }
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    // this.OnPropertyChanged("Name");
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
                    // this.OnPropertyChanged("Lastname");
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
                    // this.OnPropertyChanged("Semester");
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
                    // this.OnPropertyChanged("DateOfBirth");
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
                    // this.OnPropertyChanged("StudyProgramme");
                }
            }
        }

        public Student()
        {
        }

        public bool Equals(Student other)
        {
            return other != null && other.ID == ID;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Student);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public Student Clone()
        {
            return new Student { ID = ID, Name = Name, Lastname = Lastname, Semester = Semester, DateOfBirth = DateOfBirth, StudyProgramme = StudyProgramme.Clone() };
        }
    }
}
