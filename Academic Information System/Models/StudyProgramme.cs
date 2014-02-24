using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiS.Models
{
    public class StudyProgramme : IEquatable<StudyProgramme>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Standard length in semesters.
        /// </summary>
        public int Length { get; set; }
        public StudyType StudyType { get; set; }

        public bool Equals(StudyProgramme other)
        {
            return other != null && ID == other.ID;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as StudyProgramme);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public StudyProgramme Clone()
        {
            return new StudyProgramme { ID = ID, Name = Name, Length = Length, StudyType = StudyType };
        }
    }

    public enum StudyType
    {
        Bachelor,
        Magister
    }
}
