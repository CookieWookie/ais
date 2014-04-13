﻿using System;
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
        private static IImportManagerFactory imFactory;
        private static IRepositoryFactory rFactory;
        private static ApplicationViewModel appVM;

        public static IViewModelFactory ViewModelFactory
        {
            get { return App.vmFactory; }
        }
        public static IImportManagerFactory ImportManagerFactory
        {
            get { return App.imFactory; }
        }
        public static IRepositoryFactory RepositoryFactory
        {
            get { return App.rFactory; }
        }
        public static ApplicationViewModel ApplicationViewModel
        {
            get { return App.appVM; }
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

            ApplicationViewModel viewModel = App.appVM = new ApplicationViewModel();
            ApplicationView view = new ApplicationView
            {
                DataContext = viewModel
            };

            view.ShowDialog();
        }
    }
}
