using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.ViewModels
{
    public class DefaultViewModel : BaseViewModel
    {
        public override string WindowName
        {
            get { return "X"; }
        }

        public DefaultViewModel()
            : base()
        {
        }

        public override void Close()
        {
        }
    }
}
