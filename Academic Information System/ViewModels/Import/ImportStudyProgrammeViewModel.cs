// Assigned to: Adam Polák

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using AiS.Repositories;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public class ImportStudyProgrammeViewModel : BaseViewModel, IImportViewModel
    {
        private readonly IImportManager<StudyProgramme> importManager;
        private readonly ICommand findFileCommand;
        private readonly ICommand parseFileCommand;
        private readonly ICommand saveCommand;
        private bool hasChanged = false;
        private string filePath;

        public override string WindowName
        {
            get { return "Import: Študíjny program"; }
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
            get { throw new NotImplementedException(); }
        }

        public ImportStudyProgrammeViewModel(IImportManager<StudyProgramme> importManager)
        {
            importManager.ThrowIfNull("importManager");
            this.importManager = importManager;
            this.findFileCommand = new RelayCommand(x => this.FindFile());
            this.parseFileCommand = new RelayCommand(x => this.ParseFile(), x => this.CanParse);
            this.saveCommand = new RelayCommand(x => this.Save(), x => true);
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
