using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Library.MVVM;
using Library.Reflection;
using Library.Serialization;
using Library.Singleton;
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
            Click_Open = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
            Click_Serialize = new RelayCommand(Serialize);
            Click_Deserialize = new RelayCommand(Deserialize);
        }

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public ICommand Click_Open { get; }
        public ICommand Click_Browse { get; }
        public ICommand Click_Serialize { get; }
        public ICommand Click_Deserialize { get; }
        public AssemblyTI assemblyTi;
        public AssemblyMetadata assemblyMetadata;
        public IOpenDialogPath GetPath { get; set; }
        public ILogger Logger { get; set; }
        public ISerializer Serializer { get; set; }

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
                    Logger.Log("Reflection success!", LevelEnum.Success);
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
        private void Serialize()
        {
            Logger.Log("Serialize started...", LevelEnum.Information);
            try
            {
                Serializer.Serialize(assemblyMetadata);
                Logger.Log("Serialize completed", LevelEnum.Success);
            }
            catch (Exception e)
            {
                Logger.Log("Serialization failed with exception: " + e.Message, LevelEnum.Error);
            }
            
        }
        private void Deserialize()
        {
            Logger.Log("Deserialize started...", LevelEnum.Information);
            try
            {
                assemblyMetadata = Serializer.Deserialize<AssemblyMetadata>();
                foreach(NamespaceMetadata x in assemblyMetadata.m_Namespaces)
                {
                    foreach (TypeMetadata y in x.m_Types)
                    {
                        if (!TypeSingleton.Instance.ContainsKey(y.TypeName))
                        {
                            TypeSingleton.Instance.Add(y.TypeName, y);
                        }
                    }
                }
                assemblyTi = new AssemblyTI(assemblyMetadata);
                Logger.Log("Reflection success!", LevelEnum.Success);
            }
            catch (Exception e)
            {
                Logger.Log("Deserialization failed with exception: " + e.Message, LevelEnum.Error);
            }
            TreeViewLoaded();
        }
    }
}
