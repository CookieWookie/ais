﻿// Assigned to: Adam Polák

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using AiS.Repositories;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;

namespace AiS.ViewModels
{
    public class ImportSubjectViewModel : BaseViewModel, IImportViewModel
    {
        private readonly IImportManager<Subject> importManager;
        private readonly ICommand findFileCommand;
        private readonly ICommand parseFileCommand;
        private readonly ICommand saveCommand;
        private bool hasChanged = false;
        private string filePath;

        public override string WindowName
        {
            get { return "Import: Predmet"; }
        }
        public ICommand FindFileCommand
        {
            get { return this.findFileCommand; }
        }
        public ICommand ParseFileCommand
        {
            get { return this.parseFileCommand; }
        }
        public ICommand SaveCommand
        {
            get { return this.saveCommand; }
        }
        public string FilePath
        {
            get { return this.filePath; }
            set
            {
                if (value != this.filePath)
                {
                    this.filePath = value;
                    this.OnPropertyChanged("FilePath");
                    this.OnPropertyChanged("CanParse");
                }
            }
        }
        public bool HasChanged
        {
            get { return this.hasChanged; }
            set
            {
                if (value != this.hasChanged)
                {
                    this.hasChanged = value;
                    this.OnPropertyChanged("HasChanged");
                }
            }
        }
        public bool CanParse
        {
            get { return File.Exists(this.FilePath) && this.FilePath.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase); }
        }

        public ImportSubjectViewModel(IImportManager<Subject> importManager)
        {
            importManager.ThrowIfNull("importManager");
            this.importManager = importManager;
            this.findFileCommand = new RelayCommand(x => this.FindFile());
            this.parseFileCommand = new RelayCommand(x => this.ParseFile(), x => this.CanParse);
            this.saveCommand = new RelayCommand(x => this.Save(), x => true);
        }

        public void Save()
        {
            this.importManager.Save();
            this.HasChanged = false;
            FilePath = "";
        }

        public void FindFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            bool? result;
            result = dialog.ShowDialog();
            if (result != null && result.Value)
            {
                string fileName = dialog.FileName;
                this.FilePath = fileName;
            }
        }

        public void ParseFile()
        {
            if (File.Exists(this.FilePath))
            {
                importManager.ParseFile(FilePath);
                this.HasChanged = true;
                FilePath = "";
            }
        }
    }
}
