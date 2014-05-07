using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AiS.ViewModels;
using AiS.Models;

namespace AiS
{
    public static class WindowChangeCommands
    {
        private static ICommand importExamCommand;
        private static ICommand importStudentCommand;
        private static ICommand importStudyProgrammeCommand;
        private static ICommand importSubjectCommand;
        private static ICommand importTeacherCommand;

        private static ICommand addExamCommand;
        private static ICommand addStudentCommand;
        private static ICommand addStudyProgrammeCommand;
        private static ICommand addSubjectCommand;
        private static ICommand addTeacherCommand;

        private static ICommand showExamCommand;
        private static ICommand showStudentCommand;
        private static ICommand showStudyProgrammeCommand;
        private static ICommand showSubjectCommand;
        private static ICommand showTeacherCommand;

        private static ICommand reportStudentCommand;
        private static ICommand reportSubjectCommand;

        public static ICommand ImportExamCommand
        {
            get
            {
                return WindowChangeCommands.importExamCommand ??
                    (WindowChangeCommands.importExamCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateImportExamWindow()),
                        o => true));
            }
        }
        public static ICommand ImportStudentCommand
        {
            get
            {
                return WindowChangeCommands.importStudentCommand ??
                    (WindowChangeCommands.importStudentCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateImportStudentWindow()),
                        o => true));
            }
        }
        public static ICommand ImportStudyProgrammeCommand
        {
            get
            {
                return WindowChangeCommands.importStudyProgrammeCommand ??
                    (WindowChangeCommands.importStudyProgrammeCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateImportStudyProgrammeWindow()),
                        o => true));
            }
        }
        public static ICommand ImportSubjectCommand
        {
            get
            {
                return WindowChangeCommands.importSubjectCommand ??
                    (WindowChangeCommands.importSubjectCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateImportSubjectWindow()),
                        o => true));
            }
        }
        public static ICommand ImportTeacherCommand
        {
            get
            {
                return WindowChangeCommands.importTeacherCommand ??
                    (WindowChangeCommands.importTeacherCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateImportTeacherWindow()),
                        o => true));
            }
        }

        public static ICommand AddExamCommand
        {
            get
            {
                return WindowChangeCommands.addExamCommand ??
                    (WindowChangeCommands.addExamCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateAddExamWindow(o)),
                        o => o == null || o is Exam));
            }
        }
        public static ICommand AddStudentCommand
        {
            get
            {
                return WindowChangeCommands.addStudentCommand ??
                    (WindowChangeCommands.addStudentCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateAddStudentWindow(o)),
                        o => o == null || o is Student));
            }
        }
        public static ICommand AddStudyProgrammeCommand
        {
            get
            {
                return WindowChangeCommands.addStudyProgrammeCommand ??
                    (WindowChangeCommands.addStudyProgrammeCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateAddStudyProgrammeWindow(o)),
                        o => o == null || o is StudyProgramme));
            }
        }
        public static ICommand AddSubjectCommand
        {
            get
            {
                return WindowChangeCommands.addSubjectCommand ??
                    (WindowChangeCommands.addSubjectCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateAddSubjectWindow(o)),
                        o => o == null || o is Subject));
            }
        }
        public static ICommand AddTeacherCommand
        {
            get
            {
                return WindowChangeCommands.addTeacherCommand ??
                    (WindowChangeCommands.addTeacherCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateAddTeacherWindow(o)),
                        o => o == null || o is Teacher));
            }
        }

        public static ICommand ShowExamCommand
        {
            get
            {
                return WindowChangeCommands.showExamCommand ??
                    (WindowChangeCommands.showExamCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateShowExamWindow()),
                        o => true));
            }
        }
        public static ICommand ShowStudentCommand
        {
            get
            {
                return WindowChangeCommands.showStudentCommand ??
                    (WindowChangeCommands.showStudentCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateShowStudentWindow()),
                        o => true));
            }
        }
        public static ICommand ShowStudyProgrammeCommand
        {
            get
            {
                return WindowChangeCommands.showStudyProgrammeCommand ??
                    (WindowChangeCommands.showStudyProgrammeCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateShowStudyProgrammeWindow()),
                        o => true));
            }
        }
        public static ICommand ShowSubjectCommand
        {
            get
            {
                return WindowChangeCommands.showSubjectCommand ??
                    (WindowChangeCommands.showSubjectCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateShowSubjectWindow()),
                        o => true));
            }
        }
        public static ICommand ShowTeacherCommand
        {
            get
            {
                return WindowChangeCommands.showTeacherCommand ??
                    (WindowChangeCommands.showTeacherCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateShowTeacherWindow()),
                        o => true));
            }
        }

        public static ICommand ReportStudentCommand
        {
            get
            {
                return WindowChangeCommands.reportStudentCommand ??
                    (WindowChangeCommands.reportStudentCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateReportStudentWindow()),
                        o => true));
            }
        }
        public static ICommand ReportSubjectCommand
        {
            get
            {
                return WindowChangeCommands.reportSubjectCommand ??
                    (WindowChangeCommands.reportSubjectCommand = new RelayCommand(
                        o => WindowChangeCommands.RaiseViewModelChangedEvent(App.ViewModelFactory.CreateReportSubjectWindow()),
                        o => true));
            }
        }

        private static void RaiseViewModelChangedEvent(IViewModel vm)
        {
            vm.ThrowIfNull("vm");
            ViewModelChangedDelegate d = WindowChangeCommands.ViewModelChangedEvent;
            if (d != null)
            {
                d(null, new ViewModelChangedEventArgs(vm));
            }
        }

        public static event ViewModelChangedDelegate ViewModelChangedEvent;
    }
}
