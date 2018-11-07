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
            TypeList = namespaceMetadata.m_Types;
        }

        public void BuiltMyself(ObservableCollection<TreeViewItem> children)
        {
            TypeTI tmp;
            foreach (TypeMetadata types in TypeList)
            {
                tmp = new TypeTI(types);
                children.Add(new TreeViewItem(tmp, tmp.Name));
            }
        }
    }
}
