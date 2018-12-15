using Library.Reflection;
using Library.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TreeView
{
    public class PropertyTI : TreeViewItem
    {
        public PropertyMetadata PropertyMetadata { get; set; }
        public PropertyTI(PropertyMetadata type, string name) : base(name, ItemTypeEnum.Property)
        {
            PropertyMetadata = type;
        }
        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (PropertyMetadata.Type != null)
            {
                children.Add(new TypeTI(TypeSingleton.Instance.Get(PropertyMetadata.Type.TypeName), ItemTypeEnum.Type));
            }
        }
    }
}
