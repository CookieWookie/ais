using AiS.Models;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public class ShowExamViewModel : BaseShowViewModel<Exam>
    {
        public override string WindowName
        {
            get { return "Zobraz: Skúška"; }
        }

        public ShowExamViewModel(IRepository<Exam> repository)
            : base(repository)
        {
        }
    }
}
