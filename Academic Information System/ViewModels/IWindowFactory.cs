using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public interface IWindowFactory
    {
        IViewModel CreateDefaultWindow();

        IViewModel CreateImportExamWindow();
        IViewModel CreateImportStudentWindow();
        IViewModel CreateImportStudyProgrammeWindow();
        IViewModel CreateImportTeacherWindow();
        IViewModel CreateImportSubjectWindow();

        IViewModel CreateAddExamWindow();
        IViewModel CreateAddStudentWindow();
        IViewModel CreateAddStudyProgrammeWindow();
        IViewModel CreateAddTeacherWindow();
        IViewModel CreateAddSubjectWindow();
    }
}
