using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.Repositories
{
    public interface IImportManager<T>
    {
        /// <summary>
        /// Repozitár, do ktorého sa uložia dáta z importu.
        /// </summary>
        IRepository<T> Repository { get; }

        /// <summary>
        /// Rozparsuje súbor na objekty typu T a uloží ich do pamäte.
        /// </summary>
        /// <param name="file">Cesta k súboru, ktorý sa má importovať.</param>
        void ParseFile(string file);
        /// <summary>
        /// Uloží objekty v pamäti do repozitára (vlastnosť Repository) a vyprázdni lokálne úložisko.
        /// </summary>
        void Save();
    }
}
