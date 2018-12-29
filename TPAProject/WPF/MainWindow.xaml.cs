using System;
using System.Windows;
using Library.TreeView;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace Presentation.WPF
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
            Compose(DataContext);
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Compose(object obj)
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
            container.ComposeParts(obj);
        }
    }
}
