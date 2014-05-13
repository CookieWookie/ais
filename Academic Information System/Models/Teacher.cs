using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace AiS.Models
{
    public class Teacher : ObservableObject, IEquatable<Teacher>
    {
        private string id;
        private string title;
        private string name;
        private string lastname;
        private string titleSuffix;
        private ObservableCollection<Subject> subjects;

        public string ID
        {
            get { return this.id; }
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }
        public string Title
        {
            get { return this.title ?? ""; }
            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }
        public string Name
        {
            get { return this.name ?? ""; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
        public string Lastname
        {
            get { return this.lastname ?? ""; }
            set
            {
                if (value != this.lastname)
                {
                    this.lastname = value;
                    this.OnPropertyChanged("Lastname");
                }
            }
        }
        public string TitleSuffix
        {
            get { return this.titleSuffix ?? ""; }
            set
            {
                if (value != this.titleSuffix)
                {
                    this.titleSuffix = value;
                    this.OnPropertyChanged("TitleSuffix");
                }
            }
        }
        public IList<Subject> Teaches
        {
            get
            {
                if (subjects == null)
                {
                    SetCollection(null);
                }
                return subjects;
            }
            set
            {
                if (value != this.subjects)
                {
                    this.SetCollection(value);
                    this.OnPropertyChanged("Teaches");
                }
            }
        }

        public Teacher()
        {
        }

        private void SetCollection(IList<Subject> value)
        {
            this.subjects = value == null ? new ObservableCollection<Subject>() : new ObservableCollection<Subject>(value);
            this.subjects.CollectionChanged += (sender, e) => this.OnPropertyChanged("Teaches");
        }

        public bool Equals(Teacher other)
        {
            return other != null && other.ID == ID;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Teacher);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public Teacher Clone()
        {
            return new Teacher { ID = ID, Title = Title, Name = Name, Lastname = Lastname, TitleSuffix = TitleSuffix, Teaches = Teaches.Select(x => x.Clone()).ToList() };
        }
    }
}
