using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public interface ISaveViewModel : IViewModel
    {
        ICommand SaveCommand { get; }
        bool HasChanged { get; }

        void Save();
    }
}
