using AiS.Models;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public class ShowTeacherViewModel : BaseShowViewModel<Teacher>
    {
        public override string WindowName
        {
            get { return "Zobraz: Učiteľ"; }
        }

        public ShowTeacherViewModel(IRepository<Teacher> repository)
            : base(repository)
        {
        }
    }
}
