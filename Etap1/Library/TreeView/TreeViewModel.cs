using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Library.MVVM;
using Library.Reflection;
using Library.Tracing;
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
        public ILogger Logger { get; set; }

        private void LoadDLL()
        {
            if (PathVariable != null && PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            {
                Logger.Log("Path loaded", LevelEnum.Success);
                try
                {
                    Logger.Log("Starting reflection...", LevelEnum.Information);
                    assemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(PathVariable));
                    assemblyTi = new AssemblyTI(assemblyMetadata);
                    Logger.Log("Reflection seccess!", LevelEnum.Success);
                }
                catch (Exception e)
                {
                    Logger.Log("Reflection failed with exception: " + e.Message, LevelEnum.Error);
                }
                TreeViewLoaded();
            } else
            {
                Logger.Log("Path incorrect!", LevelEnum.Warning);
            }
        }
        private void TreeViewLoaded()
        {
             HierarchicalAreas.Add(assemblyTi);
        }

        private void Browse()
        {
            PathVariable = GetPath.GetPath();
            Logger.Log("Loading path...", LevelEnum.Information);
            RaisePropertyChanged("PathVariable");
        }
    }
}
