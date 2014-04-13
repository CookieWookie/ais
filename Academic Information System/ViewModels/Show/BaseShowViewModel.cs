using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public abstract class BaseShowViewModel<T> : BaseViewModel
        where T : class
    {
        protected readonly ICommand deleteCommand;
        protected ObservableCollection<T> data;
        protected readonly IRepository<T> repository;
        protected readonly NotifyCollectionChangedEventHandler handler;

        public virtual IList<T> Data
        {
            get { return this.data ?? (this.data = this.CreateCollection(null, handler)); }
            set
            {
                if (value != this.data)
                {
                    this.data = CreateCollection(value, this.handler);
                    this.OnPropertyChanged("Data");
                }
            }
        }
        public ICommand DeleteCommand
        {
            get { return this.deleteCommand; }
        }

        protected BaseShowViewModel(IRepository<T> repository)
        {
            repository.ThrowIfNull("repository");
            this.repository = repository;
            this.handler = (sender, e) => this.OnPropertyChanged("Data");
            this.deleteCommand = new RelayCommand(o => this.Delete((T)o), o => o is T);
        }

        public override void Refresh()
        {
            this.Data = this.repository.GetAll().ToList();
        }

        public virtual void Delete(T value)
        {
            this.repository.Remove(value);
        }

        protected ObservableCollection<T> CreateCollection(IList<T> value)
        {
            return CreateCollection(value, (sender, e) => { });
        }
        protected ObservableCollection<T> CreateCollection(IList<T> value, NotifyCollectionChangedEventHandler handler)
        {
            ObservableCollection<T> data = value == null ? new ObservableCollection<T>() : new ObservableCollection<T>(value);
            data.CollectionChanged += handler;
            return data;
        }
    }
}
