using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.ViewModels
{
    public interface IImportWindowViewModel
    {
        void LoadFile(string file);

        void Save();
        void Close();
        void SaveAndClose();
    }
}
