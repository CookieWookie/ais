﻿// Assigned to: Peter Gába

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using AiS.Repositories;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class ImportExamViewModel : BaseViewModel, IImportViewModel
    {
        private readonly IImportManager<Exam> importManager;
        private readonly ICommand findFileCommand;
        private readonly ICommand parseFileCommand;
        private bool hasChanged = false;

        public override string WindowName
        {
            get { return "Import: Skúška"; }
        }
        public ICommand FindFileCommand
        {
            get { return this.findFileCommand; }
        }
        public ICommand ParseFileCommand
        {
            get { return this.parseFileCommand; }
        }
        public string FilePath { get; set; }
        public bool HasChanged
        {
            get { return this.hasChanged; }
        }
        public bool CanParse
        {
            get { throw new NotImplementedException(); }
        }

        public ImportExamViewModel(IImportManager<Exam> importManager)
        {
            importManager.ThrowIfNull("importManager");
            this.importManager = importManager;
            this.findFileCommand = new RelayCommand(x => this.FindFile());
            this.parseFileCommand = new RelayCommand(x => this.ParseFile(), x => this.CanParse);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        
        public void FindFile()
        {
            throw new NotImplementedException();
        }

        public void ParseFile()
        {
            throw new NotImplementedException();
        }
    }
}
