using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public abstract class BaseViewModel : ObservableObject, IViewModel
    {
        private readonly ICommand closeCommand;
        private readonly ICommand refreshCommand;

        public abstract string WindowName { get; }
        public virtual ICommand CloseCommand
        {
            get { return this.closeCommand; }
        }
        public virtual ICommand RefreshCommand
        {
            get { return this.refreshCommand; }
        }

        protected BaseViewModel()
        {
            this.closeCommand = new RelayCommand(o => this.Close(), o => true);
            this.refreshCommand = new RelayCommand(o => this.Refresh(), o => true);
        }

        public virtual void Refresh()
        {
        }

        public virtual void Close()
        {
            if (ViewModelClosingEvent != null)
            {
                ViewModelClosingEvent(this, new EventArgs());
            }
        }

        public event ViewModelClosingDelegate ViewModelClosingEvent;
    }
}
