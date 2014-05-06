using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using AiS.Repositories;
using System.Collections.ObjectModel;

namespace AiS.ViewModels.Report
{
    // bude zobrazovať učiteľov, ktorí učia daný predmet,
    // počet skúšok z daného predmetu
    public class ReportSubjectViewModel : BaseViewModel
    {
        private readonly IExamRepository repository;
        private Subject selectedItem;
        private ObservableCollection<Subject> subjects;
        private ObservableCollection<Teacher> teachers;
        private ObservableCollection<Exam> exams;

        public override string WindowName
        {
            get { throw new NotImplementedException(); }
        }

        public Subject SelectedItem
        {
            get
            {
                return this.selectedItem ?? (this.selectedItem = this.Subjects.FirstOrDefault());
            }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                    this.OnPropertyChanged("Teachers");
                    this.OnPropertyChanged("Exams");
                }
            }
        }
        public IList<Subject> Subjects
        {
            get { return this.subjects; }
        }
        public IEnumerable<Teacher> Teachers
        {
            get { return this.teachers.Where(t => t.Teaches.Contains(this.SelectedItem)); }
        }
        public int Exams
        {
            get { return this.exams.Count(e => e.Subject.Equals(this.SelectedItem)); }
        }

        public ReportSubjectViewModel(IExamRepository repository)
        {
            this.repository = repository;
        }

        public override void Refresh()
        {
            this.subjects = new ObservableCollection<Subject>(this.repository.SubjectRepository.GetAll());
            this.subjects.CollectionChanged += (sender, e) => this.OnPropertyChanged("Subjects");
            this.teachers = new ObservableCollection<Teacher>(this.repository.TeacherRepository.GetAll());
            this.teachers.CollectionChanged += (sender, e) => this.OnPropertyChanged("Teachers");
            this.exams = new ObservableCollection<Exam>(this.repository.GetAll());
            this.exams.CollectionChanged += (sender, e) => this.OnPropertyChanged("Exams");
        }
    }
}
