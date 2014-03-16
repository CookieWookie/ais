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
            return this.importExamViewModel ?? (this.importExamViewModel = new ImportExamViewModel(this.importManagerFactory.ExamImportManager));
        }
        public IViewModel CreateImportStudentWindow()
        {
            return this.importStudentViewModel ?? (this.importStudentViewModel = new ImportStudentViewModel(this.importManagerFactory.StudentImportManager));
        }
        public IViewModel CreateImportStudyProgrammeWindow()
        {
            return this.importStudyProgrammeViewModel ?? (this.importStudyProgrammeViewModel = new ImportStudyProgrammeViewModel(this.importManagerFactory.StudyProgrammeImportManager));
        }
        public IViewModel CreateImportSubjectWindow()
        {
            return this.importSubjectViewModel ?? (this.importSubjectViewModel = new ImportSubjectViewModel(this.importManagerFactory.SubjectImportManager));
        }
        public IViewModel CreateImportTeacherWindow()
        {
            return this.importTeacherViewModel ?? (this.importTeacherViewModel = new ImportTeacherViewModel(this.importManagerFactory.TeacherImportManager));
        }

        public IViewModel CreateAddExamWindow(object o)
        {
            return new AddExamViewModel(this.repositoryFactory.ExamRepository, (o as Exam) ?? new Exam());
        }
        public IViewModel CreateAddStudentWindow(object o)
        {
            return new AddStudentViewModel(this.repositoryFactory.StudentRepository, (o as Student) ?? new Student());
        }
        public IViewModel CreateAddStudyProgrammeWindow(object o)
        {
            return new AddStudyProgrammeViewModel(this.repositoryFactory.StudyProgrammeRepository, (o as StudyProgramme) ?? new StudyProgramme());
        }
        public IViewModel CreateAddSubjectWindow(object o)
        {
            return new AddSubjectViewModel(this.repositoryFactory.SubjectRepository, (o as Subject) ?? new Subject());
        }
        public IViewModel CreateAddTeacherWindow(object o)
        {
            return new AddTeacherViewModel(this.repositoryFactory.TeacherRepository, (o as Teacher) ?? new Teacher());
        }
    }
}
