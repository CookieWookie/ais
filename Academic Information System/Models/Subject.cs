using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiS.Models
{
    public class Subject : ObservableObject, IEquatable<Subject>
    {
        private string id;
        private string name;
        private int semester;

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

        public Subject()
        {
        }

        public bool Equals(Subject other)
        {
            return other != null && ID == other.ID;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Subject);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public Subject Clone()
        {
            return new Subject { ID = ID, Name = Name, Semester = Semester };
        }
    }
}
