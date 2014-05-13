using AiS.Models;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public class ShowStudyProgrammeViewModel : BaseShowViewModel<StudyProgramme>
    {
        public override string WindowName
        {
            get { return "Zobraz: Študijné programy"; }
        }

        public ShowStudyProgrammeViewModel(IRepository<StudyProgramme> repository)
            : base(repository)
        {
        }
    }
}
