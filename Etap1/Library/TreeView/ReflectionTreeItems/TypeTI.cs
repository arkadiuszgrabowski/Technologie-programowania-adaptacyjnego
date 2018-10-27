using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Reflection;

namespace Library.TreeView.ReflectionTreeItems
{
    public class TypeTI : ITreeView
    {
        public string Name { get; set; }

        public TypeTI(TypeMetadata typeMetadata)
        {
            Name = typeMetadata.m_typeName;
        }

        public void BuiltMyself(ObservableCollection<TreeViewItem> children)
        {
            throw new NotImplementedException();
        }
    }
}
