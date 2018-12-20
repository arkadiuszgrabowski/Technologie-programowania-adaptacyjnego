using Data;
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
    public class MethodTI : TreeViewItem
    {
        public MethodMetadata MethodMetadata { get; set; }
        public MethodTI(MethodMetadata methodMetadata, ItemTypeEnum type) : base(GetModifiers(methodMetadata) + methodMetadata.m_MethodName, type)
        {
            MethodMetadata = methodMetadata;
        }
        public static string GetModifiers(MethodMetadata model)
        {
            string type = null;
            type += model.Modifiers.Item1.ToString().ToLower() + " ";
            type += model.Modifiers.Item2 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
            type += model.Modifiers.Item3 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
            type += model.Modifiers.Item4 == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : String.Empty;
            return type;
        }

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (MethodMetadata.GenericArguments != null)
            {
                foreach (TypeMetadata genericArgument in MethodMetadata.GenericArguments)
                {
                    children.Add(new TypeTI(TypeSingleton.Instance.Get(genericArgument.TypeName), ItemTypeEnum.GenericArgument));
                }
            }
            if (MethodMetadata.Parameters != null)
            {
                foreach (ParameterMetadata parameter in MethodMetadata.Parameters)
                {
                    children.Add(new ParameterTI(parameter, ItemTypeEnum.Parameter));
                }
            }
            if (MethodMetadata.ReturnType != null)
            {
                children.Add(new TypeTI(TypeSingleton.Instance.Get(MethodMetadata.ReturnType.TypeName), ItemTypeEnum.ReturnType));
            }
        }
    }
}
