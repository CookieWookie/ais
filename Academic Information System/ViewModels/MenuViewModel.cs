using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AiS.Models;

namespace AiS.ViewModels
{
    public class MenuViewModel : BaseViewModel, IMenuViewModel
    {
        private readonly IViewModelFactory factory;

        private ICommand importExamCommand;
        private ICommand importStudentCommand;
        private ICommand importStudyProgrammeCommand;
        private ICommand importSubjectCommand;
        private ICommand importTeacherCommand;

        private ICommand addExamCommand;
        private ICommand addStudentCommand;
        private ICommand addStudyProgrammeCommand;
        private ICommand addSubjectCommand;
        private ICommand addTeacherCommand;

        private ICommand showExamCommand;
        private ICommand showStudentCommand;
        private ICommand showStudyProgrammeCommand;
        private ICommand showSubjectCommand;
        private ICommand showTeacherCommand;

        public ICommand ImportExamCommand
        {
            get
            {
                return this.importExamCommand ??
                    (this.importExamCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateImportExamWindow()),
                        o => true));
            }
        }
        public ICommand ImportStudentCommand
        {
            get
            {
                return this.importStudentCommand ??
                    (this.importStudentCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateImportStudentWindow()),
                        o => true));
            }
        }
        public ICommand ImportStudyProgrammeCommand
        {
            get
            {
                return this.importStudyProgrammeCommand ??
                    (this.importStudyProgrammeCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateImportStudyProgrammeWindow()),
                        o => true));
            }
        }
        public ICommand ImportSubjectCommand
        {
            get
            {
                return this.importSubjectCommand ??
                    (this.importSubjectCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateImportSubjectWindow()),
                        o => true));
            }
        }
        public ICommand ImportTeacherCommand
        {
            get
            {
                return this.importTeacherCommand ??
                    (this.importTeacherCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateImportTeacherWindow()),
                        o => true));
            }
        }

        public ICommand AddExamCommand
        {
            get
            {
                return this.addExamCommand ??
                    (this.addExamCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateAddExamWindow(o)),
                        o => o == null || o is Exam));
            }
        }
        public ICommand AddStudentCommand
        {
            get
            {
                return this.addStudentCommand ??
                    (this.addStudentCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateAddStudentWindow(o)),
                        o => o == null || o is Student));
            }
        }
        public ICommand AddStudyProgrammeCommand
        {
            get
            {
                return this.addStudyProgrammeCommand ??
                    (this.addStudyProgrammeCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateAddStudyProgrammeWindow(o)),
                        o => o == null || o is StudyProgramme));
            }
        }
        public ICommand AddSubjectCommand
        {
            get
            {
                return this.addSubjectCommand ??
                    (this.addSubjectCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateAddSubjectWindow(o)),
                        o => o == null || o is Subject));
            }
        }
        public ICommand AddTeacherCommand
        {
            get
            {
                return this.addTeacherCommand ??
                    (this.addTeacherCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateAddTeacherWindow(o)),
                        o => o == null || o is Teacher));
            }
        }

        public ICommand ShowExamCommand
        {
            get
            {
                return this.showExamCommand ??
                    (this.showExamCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateShowExamWindow()),
                        o => true));
            }
        }
        public ICommand ShowStudentCommand
        {
            get
            {
                return this.showStudentCommand ??
                    (this.showStudentCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateShowStudentWindow()),
                        o => true));
            }
        }
        public ICommand ShowStudyProgrammeCommand
        {
            get
            {
                return this.showStudyProgrammeCommand ??
                    (this.showStudyProgrammeCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateShowStudyProgrammeWindow()),
                        o => true));
            }
        }
        public ICommand ShowSubjectCommand
        {
            get
            {
                return this.showSubjectCommand ??
                    (this.showSubjectCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateShowSubjectWindow()),
                        o => true));
            }
        }
        public ICommand ShowTeacherCommand
        {
            get
            {
                return this.showTeacherCommand ??
                    (this.showTeacherCommand = new RelayCommand(
                        o => this.RaiseViewModelChangedEvent(this.factory.CreateShowTeacherWindow()),
                        o => true));
            }
        }

        public override string WindowName
        {
            get { return "Menu"; }
        }

        public MenuViewModel(IViewModelFactory factory)
            : base()
        {
            factory.ThrowIfNull("factory");
            this.factory = factory;
        }

        protected virtual void RaiseViewModelChangedEvent(IViewModel vm)
        {
            vm.ThrowIfNull("vm");
            ViewModelChangedDelegate d = this.ViewModelChangedEvent;
            if (d != null)
            {
                d(this, new ViewModelChangedEventArgs(vm));
            }
        }

        public event ViewModelChangedDelegate ViewModelChangedEvent;
    }
}
