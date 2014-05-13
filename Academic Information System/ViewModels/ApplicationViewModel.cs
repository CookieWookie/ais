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
        private IViewModel currentWindow;
        private readonly IViewModel defaultWindow;
        private readonly IList<IViewModel> openWindows;

        private readonly ICommand altF4Command;
        private readonly ICommand changeWindowCommand;

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
        public IList<IViewModel> OpenWindows
        {
            get { return this.openWindows; }
        }
        public IViewModel CurrentWindow
        {
            get
            {
                if (this.currentWindow == null)
                {
                    this.currentWindow = this.OpenWindows.FirstOrDefault() ?? this.defaultWindow;
                }
                return this.currentWindow;
            }
            set
            {
                if (this.currentWindow != value)
                {
                    this.currentWindow = value;
                    this.currentWindow.Refresh();
                    this.OnPropertyChanged("CurrentWindow");
                    this.OnPropertyChanged("ApplicationName");
                }
            }
        }
        public ICommand ChangeWindowCommand
        {
            get { return this.changeWindowCommand; }
        }
        public ICommand AltF4Command
        {
            get { return this.altF4Command; }
        }

        public ApplicationViewModel()
        {
            WindowChangeCommands.ViewModelChangedEvent += this.OnViewModelChanged;
            this.defaultWindow = App.ViewModelFactory.CreateDefaultWindow();
            this.changeWindowCommand = new RelayCommand(o =>
                {
                    IViewModel vm = (IViewModel)o;
                    vm.ViewModelClosingEvent += this.OnViewModelClosing;
                    this.CurrentWindow = vm;
                }, o => o is IViewModel);

            this.altF4Command = new RelayCommand(o =>
                {
                    this.Close();
                }, o => true);

            this.openWindows = this.CreateCollection();
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

        protected void OnViewModelChanged(object sender, ViewModelChangedEventArgs e)
        {
            IViewModel vm = e.ViewModel;
            if (vm != this.defaultWindow && !this.OpenWindows.Contains(vm))
            {
                this.OpenWindows.Insert(0, vm);
            }
            this.ChangeWindowCommand.Execute(vm);
        }

        public void Close()
        {
            if (this.CurrentWindow != this.defaultWindow)
            {
                this.CurrentWindow.Close();
            }
            else
            {
                App.Current.Shutdown();
            }
        }
    }
}
