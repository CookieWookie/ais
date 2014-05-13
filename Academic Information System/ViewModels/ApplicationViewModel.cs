using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AiS.ViewModels
{
    public class ApplicationViewModel : ObservableObject
    {
        private IViewModel currentWindow;
        private readonly IViewModel defaultWindow;
        private readonly IList<IViewModel> openWindows;

        private readonly ImageBrush lienka;
        private readonly ImageBrush kitty;
        private readonly ImageBrush drevo;

        private ImageBrush background;
        private readonly ICommand altF4Command;
        private readonly ICommand changeWindowCommand;
        private readonly ICommand changeBackgroundCommand;

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
        public ICommand ChangeBackgroundCommand
        {
            get { return this.changeBackgroundCommand; }
        }
        public ImageBrush Background
        {
            get { return this.background; }
        }

        public ApplicationViewModel()
        {
            this.lienka = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Academic Information System;component/Images/lienka.jpg")));
            this.kitty = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Academic Information System;component/Images/kitty.jpg")));
            this.drevo = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Academic Information System;component/Images/drevo.jpg")));

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

            this.background = this.lienka;
            this.changeBackgroundCommand = new RelayCommand(o =>
                {
                    if (this.background == this.lienka)
                    {
                        this.background = this.kitty;
                    }
                    else if (this.background == this.kitty)
                    {
                        this.background = this.drevo;
                    }
                    else
                    {
                        this.background = this.lienka;
                    }
                    this.OnPropertyChanged("Background");
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
