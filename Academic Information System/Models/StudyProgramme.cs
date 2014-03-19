using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AiS.Models
{
    public class StudyProgramme : ObservableObject, IEquatable<StudyProgramme>
    {
        private string id;
        private string name;
        private int length;
        private StudyType studyType;

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
        /// <summary>
        /// Standard length in semesters.
        /// </summary>
        public int Length
        {
            get { return this.length; }
            set
            {
                if (this.length != value)
                {
                    this.length = value;
                    // this.OnPropertyChanged("Length");
                }
            }
        }
        public StudyType StudyType
        {
            get { return this.studyType; }
            set
            {
                if (this.studyType != value)
                {
                    this.studyType = value;
                    // this.OnPropertyChanged("StudyType");
                }
            }
        }

        public StudyProgramme()
        {
        }

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
        [Description("Bakalárske")]
        Bachelor,
        [Description("Magisterské")]
        Magister
    }
}
