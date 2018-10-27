using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Library.MVVM;
using Library.Reflection;
using Library.TreeView.ReflectionTreeItems;
using Microsoft.Win32;

namespace Library.TreeView
{
    public class TreeViewModel : ViewModelBase
    {
        public TreeViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
        }

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public ICommand Click_Button { get; }
        public ICommand Click_Browse { get; }
        private AssemblyTI assemblyTi;
        private AssemblyMetadata assemblyMetadata;
        public IOpenDialogPath GetPath { get; set; }

        private void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            assemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(PathVariable));
            assemblyTi = new AssemblyTI(assemblyMetadata);
            TreeViewLoaded();
        }
        private void TreeViewLoaded()
        {
            TreeViewItem rootItem = new TreeViewItem(assemblyTi, PathVariable.Substring(PathVariable.LastIndexOf('\\') + 1));
            HierarchicalAreas.Add(rootItem);
        }

        private void Browse()
        {
            PathVariable = GetPath.GetPath();
            RaisePropertyChanged("PathVariable");
        }
    }
}
