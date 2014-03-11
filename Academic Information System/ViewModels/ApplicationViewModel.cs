using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class ApplicationViewModel : ObservableObject
    {
        private readonly IViewModel defaultWindow;
        private readonly IWindowFactory windowFactory;
        private ICommand changeWindowCommand;
        private IList<IViewModel> openWindows;
        private IViewModel currentWindow;
        private IViewModel menuWindow;

        public string ApplicationName
        {
            get { return "Akademický informačný systém"; }
        }
        public IList<IViewModel> OpenWindows
        {
            get
            {
                if (openWindows == null)
                {
                    openWindows = new List<IViewModel>();
                }
                return openWindows;
            }
        }
        public IViewModel CurrentWindow
        {
            get { return currentWindow; }
            set
            {
                if (currentWindow != value)
                {
                    currentWindow = value;
                    OnPropertyChanged("CurrentWindow");
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
                if (changeWindowCommand == null)
                {
                    changeWindowCommand = new RelayCommand(o => CurrentWindow = (IViewModel)o, o => o is IViewModel);
                }
                return changeWindowCommand;
            }
        }

        public ApplicationViewModel(IWindowFactory windowFactory)
        {
            windowFactory.ThrowIfNull("windowFactory");
            this.windowFactory = windowFactory;
            this.defaultWindow = windowFactory.CreateDefaultWindow();
            this.menuWindow = windowFactory.CreateMenuWindow();
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
    }
}
