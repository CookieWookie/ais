using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace AiS.ViewModels
{
    public interface IAddViewModel<T> : ISaveViewModel, INotifyPropertyChanged
    {
        T Original { get; }
        ICommand ResetToDefaultCommand { get; }

        void ResetToDefault();
    }
}
