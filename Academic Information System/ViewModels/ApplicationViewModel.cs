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
        private ObservableCollection<IViewModel> openWindows;

        public string ApplicationName
        {
            get
            {
                string name = "Akademický informačný systém";
                IViewModel vm = this.CurrentWindow;
                if (vm != null && vm != this.defaultWindow)
                {
                    name += " - " + vm.WindowName;
                }
                return name;
            }
        }
        public ObservableCollection<IViewModel> OpenWindows
        {
            get
            {
                if (this.openWindows == null)
                {
                    this.openWindows = this.CreateCollection();
                }
                return this.openWindows;
            }
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
                    this.OnPropertyChanged("ApplicationName");
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
                        this.OpenWindows.Add(vm);
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

        private ObservableCollection<IViewModel> CreateCollection()
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

        protected void OnViewModelChange(object sender, ViewModelChangedEventArgs e)
        {
            if (this.CurrentWindow != this.defaultWindow && !this.OpenWindows.Contains(this.CurrentWindow))
            {
                this.OpenWindows.Add(this.CurrentWindow);
            }
            this.ChangeWindowCommand.Execute(e.ViewModel);
        }
    }
}
