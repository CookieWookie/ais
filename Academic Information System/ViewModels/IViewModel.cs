using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.ViewModels
{
    public delegate void ViewModelClosingDelegate(object sender, EventArgs e);

    public interface IViewModel
    {
        string WindowName { get; }

        void Close();

        event ViewModelClosingDelegate ViewModelClosingEvent;
    }

    public interface ISaveViewModel<T> : IViewModel
    {
        T Source { get; }
        T WorkingCopy { get; }
        bool HasChanged { get; }

        void Save();
    }
}
