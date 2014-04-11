using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Windows.Markup;

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
        private static IViewModelFactory vmFactory;
        private static IImportMangerFactory imFactory;
        private static IRepositoryFactory rFactory;

        public static IViewModelFactory ViewModelFactory
        {
            get { return App.vmFactory; }
        }
        public static IImportMangerFactory ImportManagerFactory
        {
            get { return App.imFactory; }
        }
        public static IRepositoryFactory RepositoryFactory
        {
            get { return App.rFactory; }
        }

        static App()
        {
            bool sql = Convert.ToBoolean(ConfigurationManager.AppSettings["useSql"]);
            App.rFactory = sql ? (IRepositoryFactory)new SqlRepositoryFactory() : new SqlCeRepositoryFactory();
            App.imFactory = new ImportManagerFactory(App.RepositoryFactory);
            App.vmFactory = new ViewModelFactory(App.RepositoryFactory, App.ImportManagerFactory);
        }

        public App()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationView view = new ApplicationView();
            ApplicationViewModel viewModel = new ApplicationViewModel(
                App.ViewModelFactory.CreateMenuWindow(), 
                App.ViewModelFactory.CreateDefaultWindow());
            view.DataContext = viewModel;

            view.ShowDialog();
        }
    }
}
