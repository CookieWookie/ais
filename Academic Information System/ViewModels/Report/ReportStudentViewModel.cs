using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using AiS.Repositories;
using System.Collections.ObjectModel;

namespace AiS.ViewModels.Report
{
    // bude zobrazovať študentov v okne podľa vybratého študíjneho programu
    public class ReportStudentViewModel : BaseViewModel
    {
        private readonly IStudentRepository repository;
        private StudyProgramme selectedItem;
        private ObservableCollection<StudyProgramme> studyProgrammes;
        private ObservableCollection<Student> students;

        public override string WindowName
        {
            get { throw new NotImplementedException(); }
        }
        public IList<StudyProgramme> StudyProgrammes
        {
            get
            {
                return this.studyProgrammes;
            }
        }
        public IEnumerable<Student> Students
        {
            get
            {
                return this.students.Where(s => s.StudyProgramme.Equals(this.SelectedItem));
            }
        }
        public StudyProgramme SelectedItem
        {
            get
            {
                return this.selectedItem ?? (this.selectedItem = this.StudyProgrammes.FirstOrDefault());
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                    this.OnPropertyChanged("Students");
                }
            }
        }

        public ReportStudentViewModel(IStudentRepository repository)
        {
            this.repository = repository;
        }
        public override void Refresh()
        {
            this.studyProgrammes = new ObservableCollection<StudyProgramme>(this.repository.StudyProgrammeRepository.GetAll());
            this.studyProgrammes.CollectionChanged += (sender, e) => this.OnPropertyChanged("StudyProgrammes");
            this.students = new ObservableCollection<Student>(this.repository.GetAll());
            this.students.CollectionChanged += (sender, e) => this.OnPropertyChanged("Students");

        }
    }
}
