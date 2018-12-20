using System;
using System.Windows;
using Library.TreeView;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Contracts;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = new TreeViewModel()
            {
                GetPath = new OpenDialogPath()
            };
            Compose();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Compose()
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("plugins");
            string[] pluginsCatalogs = plugins.AllKeys;
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pluginsCatalog));
            }

            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);
            ((TreeViewModel)DataContext).Serializer = container.GetExportedValue<ISerializer>();
            ((TreeViewModel)DataContext).Logger = container.GetExportedValue<ILogger>();
        }
    }
}
