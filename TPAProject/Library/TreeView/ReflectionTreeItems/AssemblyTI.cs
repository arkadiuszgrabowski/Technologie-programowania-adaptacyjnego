using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Reflection;

namespace Library.TreeView.ReflectionTreeItems
{
    public class AssemblyTI : TreeViewItem
    {
        public List<NamespaceMetadata> NamespaceList { get; set; }
  
        public AssemblyTI(AssemblyMetadata assembly) : base(assembly.m_Name, ItemTypeEnum.Assembly)
        {
            NamespaceList = assembly.m_Namespaces;
        }

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (NamespaceList != null)
            {
                foreach (NamespaceMetadata namespaceMetadata in NamespaceList)
                {
                    children.Add(new NamespaceTI(namespaceMetadata));
                }
            }
        }
    }
}
