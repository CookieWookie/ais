﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AiS.ViewModels
{
    public interface IImportViewModel : ISaveViewModel
    {
        ICommand FindFileCommand { get; }
        ICommand ParseFileCommand { get; }
        IEnumerable<Encoding> Encodings { get; }
        Encoding SelectedEncoding { get; set; }
        string FilePath { get; set; }
        bool CanParse { get; }

        void FindFile();
        void ParseFile();
    }
}
