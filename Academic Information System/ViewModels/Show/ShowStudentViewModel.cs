using AiS.Models;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public class ShowStudentViewModel : BaseShowViewModel<Student>
    {
        public override string WindowName
        {
            get { return "Zobraz: Študent"; }
        }

        public ShowStudentViewModel(IRepository<Student> repository)
            : base(repository)
        {
        }
    }
}
