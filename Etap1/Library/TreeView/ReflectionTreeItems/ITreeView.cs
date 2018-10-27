using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TreeView.ReflectionTreeItems
{
    public interface ITreeView
    {
        void BuiltMyself(ObservableCollection<TreeViewItem> children);
    }
}
