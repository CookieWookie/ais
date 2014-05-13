﻿using AiS.Models;
using AiS.Repositories;

namespace AiS.ViewModels
{
    public class ShowSubjectViewModel:BaseShowViewModel<Subject>
    {
        public override string WindowName
        {
            get { return "Zobraz: Predmety"; }
        }

        public ShowSubjectViewModel(IRepository<Subject> repository)
            : base(repository)
        {
        }
    }
}
