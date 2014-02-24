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
        public string ID { get; set; }
        public DateTime Time { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public IEnumerable<Student> SignedStudents { get; set; }
        public IEnumerable<Student> UnsignedStudents { get; set; }

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
            int hash = 17;
            unchecked
            {
                hash += 31 * ID.GetHashCode();
                hash += 29 * Time.GetHashCode();
                hash += 19 * Teacher.GetHashCode();
                hash += 23 * Subject.GetHashCode();
            }
            return hash;
        }

        public Exam Clone()
        {
            return new Exam
            {
                ID = ID,
                Time = Time,
                Subject = Subject.Clone(),
                Teacher = Teacher.Clone(),
                SignedStudents = SignedStudents.Select(s => s.Clone()).ToList(),
                UnsignedStudents = UnsignedStudents.Select(s => s.Clone()).ToList()
            };
        }
    }
}
