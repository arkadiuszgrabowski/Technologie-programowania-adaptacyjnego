using System.Windows;
using Library.Tracing;
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
                GetPath = new OpenDialogPath(),
                Logger = new FileLogger("Logs.txt", "WPF"),
                Serializer = new XMLSerializer.XMLSerializer(@"model.xml")
            };
        }
    }
}
