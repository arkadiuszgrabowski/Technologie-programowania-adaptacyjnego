using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Reflection;

namespace Library.TreeView.ReflectionTreeItems
{
    public class NamespaceTI : TreeViewItem
    {

        public List<TypeMetadata> TypeList { get; set; }

        public NamespaceTI(NamespaceMetadata namespaceMetadata) : base(namespaceMetadata.m_NamespaceName, ItemTypeEnum.Namespace)
        {
            TypeList = namespaceMetadata.m_Types;
        }

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (TypeList != null)
            {
                foreach (TypeMetadata typeMetadata in TypeList)
                {
                    ItemTypeEnum typeEnum = typeMetadata.m_Type == TypeEnum.Class ?
                        ItemTypeEnum.Class : typeMetadata.m_Type == TypeEnum.Enum ?
                            ItemTypeEnum.Enum : typeMetadata.m_Type == TypeEnum.Interface ?
                                ItemTypeEnum.Interface : ItemTypeEnum.Struct;

                    children.Add(new TypeTI(TypeMetadata.TypeDictionary[typeMetadata.m_typeName], typeEnum));
                }
            }
        }
    }
}
