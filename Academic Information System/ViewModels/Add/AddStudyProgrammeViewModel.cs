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
    public class AddStudyProgrammeViewModel : BaseAddViewModel<StudyProgramme>
    {
        private readonly IStudyProgrammeRepository repository;
        private readonly StudyProgramme original;
        private readonly string windowName;

        
        private string name;
        private int length;
        private StudyType studyType;

        
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Name");
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
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("Length");
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
                    this.OnPropertyChanged("HasChanged");
                    this.OnPropertyChanged("StudyType");
                }
            }
        }

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override StudyProgramme Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get { return !this.original.Name.Equals(this.Name) || !this.original.Length.Equals(this.Length) || !this.original.StudyType.Equals(this.StudyType); }
        }

        public AddStudyProgrammeViewModel(IStudyProgrammeRepository repository)
            : this(repository, new StudyProgramme())
        {
            this.windowName = "Pridaj: Študíjny program";
        }

        public AddStudyProgrammeViewModel(IStudyProgrammeRepository repository, StudyProgramme original)
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.windowName = "Uprav: Študíjny program";
            this.repository = repository;
            this.original = original;
            this.ResetToDefault();
        }

        public override void ResetToDefault()
        {
            this.Name = this.original.Name;
            this.Length = this.original.Length;
            this.StudyType = this.original.StudyType;
            
        }

        public override void Save()
        {
            StudyProgramme studyProgramme = new StudyProgramme();
            studyProgramme.Name = this.Name;
            studyProgramme.Length = this.Length;
            studyProgramme.StudyType = this.StudyType;
            repository.Save(studyProgramme);
            this.Close();
        }
    }
}
