using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public abstract class BaseAddViewModel<T> : BaseSaveViewModel, IAddViewModel<T>
    {
        private readonly ICommand resetToDefaultCommand;

        public virtual ICommand ResetToDefaultCommand
        {
            get { return this.resetToDefaultCommand; }
        }
        public abstract T Original { get; }

        protected BaseAddViewModel()
            : base()
        {
            this.resetToDefaultCommand = new RelayCommand(o => this.ResetToDefault(), o => true);
        }

        public abstract void ResetToDefault();
    }
}
