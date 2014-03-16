﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

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

    public interface IAddViewModel<T> : ISaveViewModel
    {
        T Original { get; }
        T WorkingCopy { get; }
        ICommand ResetToDefaultCommand { get; }

        void ResetToDefault();
    }

    public interface IMenuViewModel : IViewModel
    {
        event ViewModelChangedDelegate ViewModelChangedEvent;
    }
}
