using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public abstract class BaseViewModel : ObservableObject, IViewModel
    {
        public abstract string WindowName { get; }

        public BaseViewModel()
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

    public abstract class BaseSaveViewModel : BaseViewModel, ISaveViewModel
    {
        private readonly ICommand saveCommand;

        public virtual ICommand SaveCommand
        {
            get { throw new NotImplementedException(); }
        }
        public abstract bool HasChanged { get; }

        public BaseSaveViewModel()
            : base()
        {
            this.saveCommand = new RelayCommand(o => this.Save(), o => true);
        }

        public abstract void Save();
    }
}
