using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiS.Models
{
    /// <summary>
    /// Trieda na ukladanie skusok.
    /// </summary>
    public class Exam : IEquatable<Exam>
    {
        private IList<Student> students;

        public string ID { get; set; }
        public DateTime Time { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public IList<Student> SignedStudents
        {
            get
            {
                if (students == null)
                    students = new List<Student>();
                return students;
            }
            set { students = value; }
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
