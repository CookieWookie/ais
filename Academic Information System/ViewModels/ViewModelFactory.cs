using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;

namespace AiS.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IImportMangerFactory importManagerFactory;
        private IMenuViewModel menuViewModel;
        private IViewModel defaultViewModel;

        private IViewModel importExamViewModel;
        private IViewModel importStudentViewModel;
        private IViewModel importStudyProgrammeViewModel;
        private IViewModel importSubjectViewModel;
        private IViewModel importTeacherViewModel;

        private IViewModel showExamViewModel;
        private IViewModel showStudentViewModel;
        private IViewModel showStudyProgrammeViewModel;
        private IViewModel showSubjectViewModel;
        private IViewModel showTeacherViewModel;

        public ViewModelFactory(IRepositoryFactory repositoryFactory, IImportMangerFactory importManagerFactory)
        {
            repositoryFactory.ThrowIfNull("repositoryFactory");
            importManagerFactory.ThrowIfNull("importManagerFactory");
            this.repositoryFactory = repositoryFactory;
            this.importManagerFactory = importManagerFactory;
        }

        public IViewModel CreateDefaultWindow()
        {
            return this.defaultViewModel ?? (this.defaultViewModel = new DefaultViewModel());
        }
        public IMenuViewModel CreateMenuWindow()
        {
            return this.menuViewModel ?? (this.menuViewModel = new MenuViewModel(this));
        }

        public IViewModel CreateImportExamWindow()
        {
            return this.importExamViewModel ??
                (this.importExamViewModel = new ImportExamViewModel(this.importManagerFactory.ExamImportManager));
        }
        public IViewModel CreateImportStudentWindow()
        {
            return this.importStudentViewModel ??
                (this.importStudentViewModel = new ImportStudentViewModel(this.importManagerFactory.StudentImportManager));
        }
        public IViewModel CreateImportStudyProgrammeWindow()
        {
            return this.importStudyProgrammeViewModel ??
                (this.importStudyProgrammeViewModel = new ImportStudyProgrammeViewModel(this.importManagerFactory.StudyProgrammeImportManager));
        }
        public IViewModel CreateImportSubjectWindow()
        {
            return this.importSubjectViewModel ??
                (this.importSubjectViewModel = new ImportSubjectViewModel(this.importManagerFactory.SubjectImportManager));
        }
        public IViewModel CreateImportTeacherWindow()
        {
            return this.importTeacherViewModel ??
                (this.importTeacherViewModel = new ImportTeacherViewModel(this.importManagerFactory.TeacherImportManager));
        }

        public IViewModel CreateAddExamWindow(object o)
        {
            return o == null ? 
                new AddExamViewModel(this.repositoryFactory.ExamRepository) :
                new AddExamViewModel(this.repositoryFactory.ExamRepository, o as Exam);
        }
        public IViewModel CreateAddStudentWindow(object o)
        {
            return o == null ?
                new AddStudentViewModel(this.repositoryFactory.StudentRepository) :
                new AddStudentViewModel(this.repositoryFactory.StudentRepository, o as Student);
        }
        public IViewModel CreateAddStudyProgrammeWindow(object o)
        {
            return o == null ?
                new AddStudyProgrammeViewModel(this.repositoryFactory.StudyProgrammeRepository) :
                new AddStudyProgrammeViewModel(this.repositoryFactory.StudyProgrammeRepository, o as StudyProgramme);
        }
        public IViewModel CreateAddSubjectWindow(object o)
        {
            return o == null ?
                new AddSubjectViewModel(this.repositoryFactory.SubjectRepository) :
                new AddSubjectViewModel(this.repositoryFactory.SubjectRepository, o as Subject);
        }
        public IViewModel CreateAddTeacherWindow(object o)
        {
            return o == null ?
                new AddTeacherViewModel(this.repositoryFactory.TeacherRepository) :
                new AddTeacherViewModel(this.repositoryFactory.TeacherRepository, o as Teacher);
        }

        public IViewModel CreateShowExamWindow()
        {
            return this.showExamViewModel ??
                (this.showExamViewModel = new ShowExamViewModel(this.repositoryFactory.ExamRepository));
        }
        public IViewModel CreateShowStudentWindow()
        {
            return this.showStudentViewModel ??
                (this.showStudentViewModel = new ShowStudentViewModel(this.repositoryFactory.StudentRepository));
        }
        public IViewModel CreateShowStudyProgrammeWindow()
        {
            return this.showStudyProgrammeViewModel ??
                (this.showStudyProgrammeViewModel = new ShowStudyProgrammeViewModel(this.repositoryFactory.StudyProgrammeRepository));
        }
        public IViewModel CreateShowSubjectWindow()
        {
            return this.showSubjectViewModel ??
                (this.showSubjectViewModel = new ShowSubjectViewModel(this.repositoryFactory.SubjectRepository));
        }
        public IViewModel CreateShowTeacherWindow()
        {
            return this.showTeacherViewModel ??
                (this.showTeacherViewModel = new ShowTeacherViewModel(this.repositoryFactory.TeacherRepository));
        }
    }
}
