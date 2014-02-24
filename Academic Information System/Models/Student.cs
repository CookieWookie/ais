using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiS.Models
{
    public class Student : IEquatable<Student>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Semester { get; set; }
        public DateTime DateOfBirth { get; set; }
        public StudyProgramme StudyProgramme { get; set; }

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
