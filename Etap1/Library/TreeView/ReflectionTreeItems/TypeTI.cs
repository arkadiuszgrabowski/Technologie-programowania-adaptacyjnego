using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Reflection;

namespace Library.TreeView.ReflectionTreeItems
{
    public class TypeTI : TreeViewItem
    {
        public TypeMetadata TypeData { get; set; }

        public TypeTI(TypeMetadata typeMetadata, ItemTypeEnum type) : base(GetModifiers(typeMetadata) + typeMetadata.m_typeName, type)
        {
            TypeData = typeMetadata;
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

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            //if (TypeData.m_BaseType != null)
            //{
            //    children.Add(new TreeViewItem(new TypeTI(TypeMetadata.TypeDictionary[TypeData.m_BaseType.m_typeName]), ItemTypeEnum.BaseType));
            //}
            //if (TypeData.DeclaringType != null)
            //{
            //    children.Add(new TypeTreeItem(TypeModel.TypeDictionary[TypeData.DeclaringType.Name], ItemTypeEnum.Type));
            //}
            //if (TypeData.Properties != null)
            //{
            //    foreach (PropertyModel propertyModel in TypeData.Properties)
            //    {
            //        children.Add(new PropertyTreeItem(propertyModel, GetModifiers(propertyModel.Type) + propertyModel.Type.Name + " " + propertyModel.Name));
            //    }
            //}
            //if (TypeData.Fields != null)
            //{
            //    foreach (ParameterModel parameterModel in TypeData.Fields)
            //    {
            //        children.Add(new ParameterTreeItem(parameterModel, ItemTypeEnum.Field));
            //    }
            //}
            //if (TypeData.GenericArguments != null)
            //{
            //    foreach (TypeModel typeModel in TypeData.GenericArguments)
            //    {
            //        children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name], ItemTypeEnum.GenericArgument));
            //    }
            //}
            //if (TypeData.ImplementedInterfaces != null)
            //{
            //    foreach (TypeModel typeModel in TypeData.ImplementedInterfaces)
            //    {
            //        children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name], ItemTypeEnum.InmplementedInterface));
            //    }
            //}
            //if (TypeData.NestedTypes != null)
            //{
            //    foreach (TypeModel typeModel in TypeData.NestedTypes)
            //    {
            //        ItemTypeEnum type = typeModel.Type == TypeEnum.Class ? ItemTypeEnum.NestedClass :
            //            typeModel.Type == TypeEnum.Struct ? ItemTypeEnum.NestedStructure :
            //            typeModel.Type == TypeEnum.Enum ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;
            //        children.Add(new TypeTreeItem(TypeModel.TypeDictionary[typeModel.Name], type));
            //    }
            //}
            //if (TypeData.Methods != null)
            //{
            //    foreach (MethodModel methodModel in TypeData.Methods)
            //    {
            //        children.Add(new MethodTreeItem(methodModel, methodModel.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method));
            //    }
            //}
            //if (TypeData.Constructors != null)
            //{
            //    foreach (MethodModel methodModel in TypeData.Constructors)
            //    {
            //        children.Add(new MethodTreeItem(methodModel, ItemTypeEnum.Constructor));
            //    }
            //}
        }

    }
}
