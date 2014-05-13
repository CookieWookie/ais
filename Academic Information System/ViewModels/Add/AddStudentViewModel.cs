using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AiS.ViewModels
{
    public class AddStudentViewModel : BaseAddViewModel<Student>
    {
        private readonly IStudentRepository repository;
        private readonly Student original;
        private readonly string windowName;
        private readonly ICommand selectStudyProgrammeCommand;
        private readonly ObservableCollection<StudyProgramme> studyProgrammes;

        private string name;
        private string lastname;
        private int semester;
        private DateTime dateOfBirth;
        private StudyProgramme programme;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public string Lastname
        {
            get { return this.lastname; }
            set
            {
                if (this.lastname != value)
                {
                    this.lastname = value;
                    this.OnPropertyChanged("Lastname");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public int Semester
        {
            get { return this.semester; }
            set
            {
                if (this.semester != value)
                {
                    this.semester = value;
                    this.OnPropertyChanged("Semester");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                if (this.dateOfBirth != value)
                {
                    this.dateOfBirth = value;
                    this.OnPropertyChanged("DateOfBirth");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public StudyProgramme StudyProgramme
        {
            get { return this.programme; }
            set
            {
                if (this.programme != value || (this.programme != null && !this.programme.Equals(value)))
                {
                    this.programme = value;
                    this.OnPropertyChanged("StudyProgramme");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Student Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get
            {
                return this.original.Name != this.Name ||
                    this.original.Lastname != this.Lastname ||
                    this.original.Semester != this.Semester ||
                    this.original.DateOfBirth != this.DateOfBirth ||
                    !Comparers.Equals(this.StudyProgramme, this.Original.StudyProgramme);
            }
        }
        public ICommand SelectStudyProgrammeCommand
        {
            get { return this.selectStudyProgrammeCommand; }
        }
        public ObservableCollection<StudyProgramme> StudyProgrammes
        {
            get { return this.studyProgrammes; }
        }

        public override string this[string columnName]
        {
            get
            {
                string message = string.Empty;
                if (columnName == "Name")
                {
                    if (string.IsNullOrWhiteSpace(this.Name))
                    {
                        message = "Meno nemôže byť prázdna hodnota.";
                    }
                }
                else if (columnName == "Lastname")
                {
                    if (string.IsNullOrWhiteSpace(this.Lastname))
                    {
                        message = "Priezvisko nemôže byť prázdna hodnota.";
                    }
                }
                else if (columnName == "Semester")
                {
                    if (this.Semester < 1 || 12 < this.Semester)
                    {
                        message = "Semester môže byť iba v rozsahu od 1 do 12.";
                    }
                }
                else if (columnName == "DateOfBirth")
                {
                    if (this.DateOfBirth >= DateTime.Now.Date)
                    {
                        message = "Dátum narodenia nemôže byť v budúcnosti.";
                    }
                    else if (this.DateOfBirth < new DateTime(1900, 1, 1))
                    {
                        message = "Dátum narodenia nemôže byť skôr ako 1. 1. 1900.";
                    }
                }
                else if (columnName == "StudyProgramme")
                {
                    if (this.StudyProgramme == null || string.IsNullOrWhiteSpace(this.StudyProgramme.ID))
                    {
                        message = "Musí byť zvolený nejaký študíjny program.";
                    }
                }
                return message;
            }
        }

        public AddStudentViewModel(IStudentRepository repository)
            : this(repository, new Student())
        {
            this.windowName = "Pridaj: Študent";
        }
        public AddStudentViewModel(IStudentRepository repository, Student original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");

            this.windowName = "Uprav: Študent";
            this.repository = repository;
            this.original = original;
            this.selectStudyProgrammeCommand = new RelayCommand(o => this.StudyProgramme = (StudyProgramme)o, o => o is StudyProgramme);

            this.studyProgrammes = new ObservableCollection<StudyProgramme>(this.repository.StudyProgrammeRepository.GetAll());
            this.studyProgrammes.CollectionChanged += (sender, e) => this.OnPropertyChanged("StudyProgrammes");

            this.ResetToDefault();
        }

        public override void ResetToDefault()
        {
            this.Name = this.original.Name;
            this.Lastname = this.original.Lastname;
            this.DateOfBirth = this.original.DateOfBirth;
            this.Semester = this.original.Semester;
            this.StudyProgramme = this.original.StudyProgramme;
        }

        public override void Save()
        {
            Student student = new Student();
            student.ID = this.original.ID;
            student.Name = this.Name;
            student.Lastname = this.Lastname;
            student.DateOfBirth = this.DateOfBirth;
            student.Semester = this.Semester;
            student.StudyProgramme = this.StudyProgramme;
            repository.Save(student);
            this.Close();
        }
    }
}
