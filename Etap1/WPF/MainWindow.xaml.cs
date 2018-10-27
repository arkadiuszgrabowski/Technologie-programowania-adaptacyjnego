using System.Windows;
using Library.TreeView;

namespace WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TreeViewModel()
            {
                GetPath = new OpenDialogPath()
            };
        }
    }
}
