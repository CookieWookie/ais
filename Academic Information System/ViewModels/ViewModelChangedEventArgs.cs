using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.ViewModels
{
    public delegate void ViewModelClosingDelegate(object sender, EventArgs e);
    public delegate void ViewModelChangedDelegate(object sender, ViewModelChangedEventArgs e);

    public class ViewModelChangedEventArgs : EventArgs
    {
        private readonly IViewModel vm;

        public IViewModel ViewModel
        {
            get { return this.vm; }
        }

        public ViewModelChangedEventArgs(IViewModel vm)
        {
            vm.ThrowIfNull("vm");
            this.vm = vm;
        }
    }
}
