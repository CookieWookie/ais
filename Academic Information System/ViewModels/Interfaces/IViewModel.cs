using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace AiS.ViewModels
{
    public interface IViewModel
    {
        string WindowName { get; }
        ICommand CloseCommand { get; }

        void Close();

        event ViewModelClosingDelegate ViewModelClosingEvent;
    }
}
