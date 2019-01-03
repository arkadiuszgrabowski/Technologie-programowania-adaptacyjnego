using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Contracts;
using Data;
using Library.Mappers;
using Library.MVVM;
using Library.Reflection;
using Library.Singleton;

namespace Library.TreeView
{
    public class TreeViewModel : ViewModelBase
    {
        public TreeViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Open = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
            Click_Serialize = new RelayCommand(SerializeTask);
            Click_Deserialize = new RelayCommand(DeserializeTask);
        }

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        System.Threading.SynchronizationContext uiContext { get; set; }
        public string PathVariable { get; set; }
        public ICommand Click_Open { get; }
        public ICommand Click_Browse { get; }
        public ICommand Click_Serialize { get; }
        public ICommand Click_Deserialize { get; }
        public AssemblyTI assemblyTi;
        public AssemblyMetadata assemblyMetadata;
        public IOpenDialogPath GetPath { get; set; }
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        [Import(typeof(ISerializer))]
        public ISerializer Serializer { get; set; }
        [Import(typeof(BaseAssembly))]
        public BaseAssembly InAssembly { get; set; }
        public object ConfigurationManager { get; private set; }

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
        public void Serialize()
        {
            Logger.Log("Serialize started...", LevelEnum.Information);
            try
            {
                Serializer.Serialize(AssemblyMapper.MapDown(assemblyMetadata, InAssembly.GetType()));
                Logger.Log("Serialize completed", LevelEnum.Success);
            }
            catch (Exception e)
            {
                Logger.Log("Serialization failed with exception: " + e.Message, LevelEnum.Error);
            }
            
        }
        private void SerializeTask()
        {
            Task task = new Task(() => Serialize());
            task.Start();
        }
        public void Deserialize()
        {
            Logger.Log("Deserialize started...", LevelEnum.Information);
            try
            {
                assemblyMetadata = AssemblyMapper.MapUp(Serializer.Deserialize());
                foreach (NamespaceMetadata x in assemblyMetadata.NamespaceModels)
                {
                    foreach (TypeMetadata y in x.Types)
                    {
                        if (!TypeSingleton.Instance.ContainsKey(y.Name))
                        {
                            TypeSingleton.Instance.Add(y.Name, y);
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
            if(uiContext != null)
            {
                uiContext.Send(x => TreeViewLoaded(), null);
            } 
        }
        private void DeserializeTask()
        {
            if(Serializer.IsDeserializationPossible())
            {
                uiContext = SynchronizationContext.Current;
                if (uiContext != null)
                {
                    Task task = new Task(() => Deserialize());
                    task.Start();
                }
                else
                {
                    Task task = new Task(() => Deserialize());
                    task.Start();
                    task.Wait();
                    TreeViewLoaded();
                }
            }
        }

        public bool DeserializationPossibility()
        {
            return Serializer.IsDeserializationPossible();
        }

        public void Compose(string[] pluginsCatalogs)
        {
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pluginsCatalog));
            }

            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

    }
}
