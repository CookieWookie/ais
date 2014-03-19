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
        private readonly string windowName;

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
            get { throw new NotImplementedException(); }
        }

        public AddStudyProgrammeViewModel(IRepository<StudyProgramme> repository)
            : this(repository, new StudyProgramme())
        {
            this.windowName = "Pridaj: Študíjny program";
        }

        public AddStudyProgrammeViewModel(IRepository<StudyProgramme> repository, StudyProgramme original)
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");
            this.windowName = "Uprav: Študíjny program";
            this.repository = repository;
            this.original = original;
        }

        public override void ResetToDefault()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
