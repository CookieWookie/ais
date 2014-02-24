using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.ViewModels
{
    class TestMainWindowViewModel : IMainWindowViewModel
    {
        public string Property1 { get; set; }

        public TestMainWindowViewModel()
        {
            Property1 = "This is default value.";
        }

        public void OpenImportView()
        {
            throw new NotImplementedException();
        }

        public void OpenSaveView()
        {
            throw new NotImplementedException();
        }
    }
}
