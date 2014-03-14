using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public delegate void ViewModelClosingDelegate(object sender, EventArgs e);
    public delegate void ViewModelChangedDelegate(object sender, ViewModelChangeEventArgs e);

    public class ViewModelChangeEventArgs : EventArgs
    {
        private readonly IViewModel vm;

        public IViewModel ViewModel
        {
            get { return this.vm; }
        }

        public ViewModelChangeEventArgs(IViewModel vm)
        {
            vm.ThrowIfNull("vm");
            this.vm = vm;
        }
    }

    public interface IViewModel
    {
        string WindowName { get; }

        void Close();

        event ViewModelClosingDelegate ViewModelClosingEvent;
    }

    public interface IMenuViewModel : IViewModel
    {
        event ViewModelChangedDelegate ViewModelChangedEvent;
    }

    public interface ISaveViewModel : IViewModel
    {
        ICommand SaveCommand { get; }
        bool HasChanged { get; }

        void Save();
    }

    public interface IImportViewModel : ISaveViewModel
    {
        ICommand FindFileCommand { get; }
        ICommand ParseFileCommand { get; }
        string FilePath { get; set; }
        bool CanParse { get; }

        void FindFile();
        void ParseFile();
    }

    public interface IAddViewModel<T> : ISaveViewModel
    {
        T Original { get; }
        T WorkingCopy { get; }
        ICommand ResetToDefaultCommand { get; }

        void ResetToDefault();
    }
}
