using Library.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class ConsoleTreeView
    {
        public ObservableCollection<ConsoleTreeViewItem> DataCollection { get; set; }

        public ConsoleTreeView(ObservableCollection<ConsoleTreeViewItem> hierarchicalDataCollection)
        {
            DataCollection = hierarchicalDataCollection;
        }

        public void Expand(int index)
        {
            ConsoleTreeViewItem item = DataCollection[index];
            if (!item.IsExpanded)
            {
                int i = 1;
                ObservableCollection<TreeViewItem> items = item.Expand();
                foreach (TreeViewItem treeViewItem in items)
                {
                    DataCollection.Insert(index + i, new ConsoleTreeViewItem(treeViewItem, item.Indent + 1));
                    i++;

                }

            }
            else
            {
                for (int i = item.TreeItem.Children.Count; i > 0; i--)
                {
                    if (DataCollection[index + i].IsExpanded)
                    {
                        Expand(index + i);
                        DataCollection.RemoveAt(index + i);
                    }
                    else
                    {
                        DataCollection.RemoveAt(index + i);
                    }

                }

                item.IsExpanded = false;
            }
        }

    }
}
