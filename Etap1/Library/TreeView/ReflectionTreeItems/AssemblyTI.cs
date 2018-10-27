using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Reflection;

namespace Library.TreeView.ReflectionTreeItems
{
    public class AssemblyTI : ITreeView
    {
        public string Name { get; set; }
        public List<NamespaceMetadata> NamespaceList { get; set; }

        public AssemblyTI(AssemblyMetadata assembly)
        {
            Name = assembly.m_Name;
            NamespaceList = assembly.m_Namespaces;
        }

        public void BuiltMyself(ObservableCollection<TreeViewItem> children)
        {
            foreach(NamespaceMetadata namespaces in NamespaceList)
            {
                children.Add(new TreeViewItem(new NamespaceTI(namespaces),namespaces.m_NamespaceName));
            }
        }
    }
}
