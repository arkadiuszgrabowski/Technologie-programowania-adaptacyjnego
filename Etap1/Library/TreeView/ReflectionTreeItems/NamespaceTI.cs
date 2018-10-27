using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Reflection;

namespace Library.TreeView.ReflectionTreeItems
{
    public class NamespaceTI : ITreeView
    {
        public string Name { get; set; }

        public List<TypeMetadata> TypeList { get; set; }

        public NamespaceTI(NamespaceMetadata namespaceMetadata)
        {
            Name = namespaceMetadata.m_NamespaceName;
        }

        public void BuiltMyself(ObservableCollection<TreeViewItem> children)
        {
            foreach (TypeMetadata types in TypeList)
            {
                children.Add(new TreeViewItem(new TypeTI(types), types.m_typeName));
            }
        }
    }
}
