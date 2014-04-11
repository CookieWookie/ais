﻿using System;
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

    public interface IAddViewModel<T> : ISaveViewModel, INotifyPropertyChanged
    {
        T Original { get; }
        ICommand ResetToDefaultCommand { get; }

        void ResetToDefault();
    }
}
