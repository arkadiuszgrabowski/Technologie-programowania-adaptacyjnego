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
            Name = GetModifiers(typeMetadata) + typeMetadata.m_typeName;
        }

        public static string GetModifiers(TypeMetadata model)
        {
            if (model.m_Modifiers != null)
            {
                string type = null;
                type += model.m_Modifiers.Item1.ToString().ToLower() + " ";
                type += model.m_Modifiers.Item2 == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : String.Empty;
                type += model.m_Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
                type += model.m_Modifiers.Item4 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
                return type;
            }

            return null;
        }

        public void BuiltMyself(ObservableCollection<TreeViewItem> children)
        {
            throw new NotImplementedException();
        }
    }
}
