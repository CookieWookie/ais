using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AiS.ViewModels
{
    public class ApplicationViewModel : ObservableObject
    {
        private IViewModel menuWindow;
        private IViewModel defaultWindow;
        private IViewModel currentWindow;
        private ICommand changeWindowCommand;
        private IList<IViewModel> openWindows;

        public string ApplicationName
        {
            get { return "Akademický informačný systém"; }
        }
        public IList<IViewModel> OpenWindows
        {
            get { return this.openWindows ?? (this.openWindows = this.CreateCollection()); }
        }
        public IViewModel CurrentWindow
        {
            get
            {
                return this.currentWindow ?? (this.CurrentWindow = (this.OpenWindows.FirstOrDefault() ?? this.defaultWindow));
            }
            set
            {
                if (currentWindow != value)
                {
                    currentWindow = value;
                    this.OnPropertyChanged("CurrentWindow");
                }
            }
        }
        public IViewModel MenuWindow
        {
            get { return this.menuWindow; }
        }
        public ICommand ChangeWindowCommand
        {
            get
            {
                return this.changeWindowCommand ?? (this.changeWindowCommand = new RelayCommand(o =>
                    {
                        IViewModel vm = (IViewModel)o;
                        vm.ViewModelClosingEvent += this.OnViewModelClosing;
                        this.CurrentWindow = vm;
                    }, o => o is IViewModel));
            }
        }

        public ApplicationViewModel(IMenuViewModel menuWindow, IViewModel defaultWindow)
        {
            menuWindow.ThrowIfNull("menuWindow");
            defaultWindow.ThrowIfNull("defaultWindow");
            menuWindow.ViewModelChangedEvent += this.OnViewModelChange;
            this.menuWindow = menuWindow;
            this.defaultWindow = defaultWindow;
        }

        private IList<IViewModel> CreateCollection()
        {
            ObservableCollection<IViewModel> collection = new ObservableCollection<IViewModel>();
            collection.CollectionChanged += (sender, e) => this.OnPropertyChanged("OpenWindows");
            return collection;
        }

        protected void OnViewModelClosing(object sender, EventArgs e)
        {
            IViewModel viewModel = (IViewModel)sender;
            OpenWindows.Remove(viewModel);
            if (viewModel == CurrentWindow)
            {
                CurrentWindow = OpenWindows.FirstOrDefault() ?? defaultWindow;
            }
        }

        protected void OnViewModelChange(object sender, ViewModelChangeEventArgs e)
        {
            if (this.CurrentWindow != this.defaultWindow)
            {
                this.OpenWindows.Add(this.CurrentWindow);
            }
            this.ChangeWindowCommand.Execute(e.ViewModel);
        }
    }
}
