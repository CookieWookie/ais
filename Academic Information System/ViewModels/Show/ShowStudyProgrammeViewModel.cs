using AiS.Models;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public class ShowStudyProgrammeViewModel : BaseShowViewModel<StudyProgramme>
    {
        public override string WindowName
        {
            get { return "Zobraz: Študíjny program"; }
        }

        public ShowStudyProgrammeViewModel(IRepository<StudyProgramme> repository)
            : base(repository)
        {
        }
    }
}
