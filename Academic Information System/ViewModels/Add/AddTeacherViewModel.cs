﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Repositories;
using AiS.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AiS.ViewModels
{
    public class AddTeacherViewModel : BaseAddViewModel<Teacher>
    {
        private readonly ITeacherRepository repository;
        private readonly Teacher original;
        private readonly string windowName;

        private readonly ICommand addSubjectCommand;
        private readonly ICommand removeSubjectCommand;
        private List<Subject> subjects;

        private string title;
        private string name;
        private string lastname;
        private string titleSuffix;
        private ObservableCollection<Subject> teaches;

        public string Title
        {
            get { return this.title; }
            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
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
                if (value != this.lastname)
                {
                    this.lastname = value;
                    this.OnPropertyChanged("Lastname");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public string TitleSuffix
        {
            get { return this.titleSuffix; }
            set
            {
                if (value != this.titleSuffix)
                {
                    this.titleSuffix = value;
                    this.OnPropertyChanged("TitleSuffix");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public IList<Subject> Teaches
        {
            get
            {
                if (teaches == null)
                {
                    this.SetCollection(null);
                }
                return teaches;
            }
            set
            {
                if (value != this.teaches)
                {
                    this.SetCollection(value);
                    this.OnPropertyChanged("Teaches");
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }

        public override string WindowName
        {
            get { return this.windowName; }
        }
        public override Teacher Original
        {
            get { return this.original; }
        }
        public override bool HasChanged
        {
            get
            {
                return this.Name != this.original.Name ||
                    this.Lastname != this.original.Lastname ||
                    !new CollectionComparer<Subject>().Equals(this.Teaches, this.original.Teaches);
            }
        }
        public ICommand AddSubjectCommand
        {
            get { return this.addSubjectCommand; }
        }
        public ICommand RemoveSubjectCommand
        {
            get { return this.removeSubjectCommand; }
        }

        public IEnumerable<Subject> Subjects
        {
            get { return this.subjects.Except(this.Teaches); }
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
                        message = "Meno nemôže byť prázdny reťazec.";
                    }
                }
                else if (columnName == "Lastname")
                {
                    if (string.IsNullOrWhiteSpace(this.Lastname))
                    {
                        message = "Priezvisko nemôže byť prázdny reťazec.";
                    }
                }
                return message;
            }
        }

        public AddTeacherViewModel(ITeacherRepository repository)
            : this(repository, new Teacher())
        {
            this.windowName = "Pridaj: Učiteľ";
        }
        public AddTeacherViewModel(ITeacherRepository repository, Teacher original)
            : base()
        {
            repository.ThrowIfNull("repository");
            original.ThrowIfNull("original");

            this.windowName = "Uprav: Učiteľ";
            this.repository = repository;
            this.original = original;

            this.subjects = this.repository.SubjectRepository.GetAll().ToList();
            this.addSubjectCommand = new RelayCommand(o =>
            {
                this.teaches.Add((Subject)o);

            }, o => o is Subject && o != null);
            this.removeSubjectCommand = new RelayCommand(o =>
            {
                this.teaches.Remove((Subject)o);
            }, o => o is Subject);

            this.ResetToDefault();
        }

        public override void ResetToDefault()
        {
            this.Title = this.original.Title;
            this.Name = this.original.Name;
            this.Lastname = this.original.Lastname;
            this.TitleSuffix = this.original.TitleSuffix;
            this.Teaches = this.Original.Teaches;
        }
        public override void Save()
        {
            Teacher teacher = new Teacher();
            teacher.ID = this.original.ID;
            teacher.Name = this.Name;
            teacher.Title = this.Title;
            teacher.Lastname = this.Lastname;
            teacher.TitleSuffix = this.TitleSuffix;
            teacher.Teaches = this.Teaches;
            repository.Save(teacher);
            this.Close();
        }
        private void SetCollection(IList<Subject> value)
        {
            this.teaches = value == null ? new ObservableCollection<Subject>() : new ObservableCollection<Subject>(value);
            this.teaches.CollectionChanged += (sender, e) =>
            {
                this.OnPropertyChanged("Teaches");
                this.OnPropertyChanged("Subjects");
                this.OnPropertyChanged("HasChanged");
            };
        }
    }
}
