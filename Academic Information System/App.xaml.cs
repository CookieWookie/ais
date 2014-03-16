using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AiS
{
    using Repositories;
    using Repositories.Database.Sql;
    using Repositories.Database.SqlCe;
    using ViewModels;
    using Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool sql = Convert.ToBoolean(ConfigurationManager.AppSettings["useSql"]);

            IRepositoryFactory repositoryFactory = sql ? (IRepositoryFactory)new SqlRepositoryFactory() : new SqlCeRepositoryFactory();
            IImportMangerFactory importManagerFactory = new ImportManagerFactory(repositoryFactory);
            IViewModelFactory viewModelFactory = new ViewModelFactory(repositoryFactory, importManagerFactory);

            ApplicationView view = new ApplicationView();
            ApplicationViewModel viewModel = new ApplicationViewModel(viewModelFactory.CreateMenuWindow(), viewModelFactory.CreateDefaultWindow());
            view.DataContext = viewModel;

            view.ShowDialog();
        }
    }
}
