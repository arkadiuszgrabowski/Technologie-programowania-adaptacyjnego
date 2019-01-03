using System;
using System.Windows;
using Library.TreeView;
using System.Collections.Specialized;
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
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("plugins");
            string[] pluginsCatalogs = plugins.AllKeys;
            ((TreeViewModel)DataContext).Compose(pluginsCatalogs);
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
