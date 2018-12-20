using System;
using System.Windows;
using Library.TreeView;

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
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
