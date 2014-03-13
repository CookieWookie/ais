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

    public abstract class BaseAddViewModel<T> : BaseSaveViewModel, IAddViewModel<T>
    {
        private readonly ICommand resetToDefaultCommand;

        public virtual ICommand ResetToDefaultCommand
        {
            get { return this.resetToDefaultCommand; }
        }
        public abstract T Original { get; }
        public abstract T WorkingCopy { get; }

        public BaseAddViewModel()
            : base()
        {
            this.resetToDefaultCommand = new RelayCommand(o => this.ResetToDefault(), o => true);
        }

        public abstract void ResetToDefault();
    }
}
