using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class AddStudyProgrammeViewModel : BaseAddViewModel<StudyProgramme>
    {
        private readonly IRepository<StudyProgramme> repository;
        private readonly StudyProgramme original;

        public override string WindowName
        {
            get { return "Pridaj: Študíjny program"; }
        }
        public StudyProgramme Original
        {
            get { return this.original; }
        }
        public StudyProgramme WorkingCopy
        {
            get { throw new NotImplementedException(); }
        }
        public bool HasChanged
        {
            get { throw new NotImplementedException(); }
        }

        public AddStudyProgrammeViewModel(IRepository<StudyProgramme> repository, StudyProgramme original)
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.repository = repository;
            this.original = original;
        }

        public void ResetToDefault()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
