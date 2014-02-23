using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiS.Models
{
    public class Subject : IEquatable<Subject>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }

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
