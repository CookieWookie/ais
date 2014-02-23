using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AiS.ViewModels
{
    public interface ISaveWindowViewModel<T> : INotifyPropertyChanged
    {
        T Original { get; }
        T WorkingCopy { get; }

        void Load(string ID);
        void Save();
        void Close(bool save);
    }
}
