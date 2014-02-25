using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        public abstract string WindowName { get; }

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
