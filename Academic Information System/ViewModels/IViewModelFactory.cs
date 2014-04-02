using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AiS.Models;

namespace AiS.ViewModels
{
    public interface IViewModelFactory
    {
        IViewModel CreateDefaultWindow();
        IMenuViewModel CreateMenuWindow();

        IViewModel CreateImportExamWindow();
        IViewModel CreateImportStudentWindow();
        IViewModel CreateImportStudyProgrammeWindow();
        IViewModel CreateImportSubjectWindow();
        IViewModel CreateImportTeacherWindow();

        IViewModel CreateAddExamWindow(object o);
        IViewModel CreateAddStudentWindow(object o);
        IViewModel CreateAddStudyProgrammeWindow(object o);
        IViewModel CreateAddSubjectWindow(object o);
        IViewModel CreateAddTeacherWindow(object o);

        IViewModel CreateShowExamWindow();
        IViewModel CreateShowStudentWindow();
        IViewModel CreateShowStudyProgrammeWindow();
        IViewModel CreateShowSubjectWindow();
        IViewModel CreateShowTeacherWindow();
    }
}
