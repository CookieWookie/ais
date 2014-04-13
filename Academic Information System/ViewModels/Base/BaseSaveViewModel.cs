using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public abstract class BaseSaveViewModel : BaseViewModel, ISaveViewModel
    {
        private readonly ICommand saveCommand;

        public virtual ICommand SaveCommand
        {
            get { return this.saveCommand; }
        }
        public abstract bool HasChanged { get; }

        protected BaseSaveViewModel()
            : base()
        {
            this.saveCommand = new RelayCommand(o => this.Save(), o => true);
        }

        public abstract void Save();
    }
}
