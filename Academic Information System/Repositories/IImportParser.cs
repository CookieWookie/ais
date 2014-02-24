using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.Repositories
{
    public interface IImportParser<T>
    {
        IEnumerable<T> Parse();
    }
}
