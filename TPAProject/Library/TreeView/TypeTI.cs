using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Library.Reflection;
using Library.Singleton;

namespace Library.TreeView
{
    public class TypeTI : TreeViewItem
    {
        public TypeMetadata TypeData { get; set; }

        public TypeTI(TypeMetadata typeMetadata, ItemTypeEnum type) : base(GetModifiers(typeMetadata) + typeMetadata.TypeName, type)
        {
            TypeData = typeMetadata;
        }

        public static string GetModifiers(TypeMetadata model)
        {
            if (model.Modifiers != null)
            {
                string type = null;
                type += model.Modifiers.AccessLevel.ToString().ToLower() + " ";
                type += model.Modifiers.SealedEnum == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : String.Empty;
                type += model.Modifiers.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
                type += model.Modifiers.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
                return type;
            }
            return null;
        }

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (TypeData.BaseType != null)
            {
                children.Add(new TypeTI(TypeSingleton.Instance.Get(TypeData.BaseType.TypeName), ItemTypeEnum.BaseType));
            }
            if (TypeData.DeclaringType != null)
            {
                children.Add(new TypeTI(TypeSingleton.Instance.Get(TypeData.DeclaringType.TypeName), ItemTypeEnum.Type));
            }
            if (TypeData.Properties != null)
            {
                foreach (PropertyMetadata propertyMetadata in TypeData.Properties)
                {
                    children.Add(new PropertyTI(propertyMetadata, GetModifiers(propertyMetadata.Type) + propertyMetadata.Type.TypeName + " " + propertyMetadata.m_PropertyName));
                }
            }
            if (TypeData.Fields != null)
            {
                foreach (ParameterMetadata parameterMetadata in TypeData.Fields)
                {
                    children.Add(new ParameterTI(parameterMetadata, ItemTypeEnum.Field));
                }
            }
            if (TypeData.GenericArguments != null)
            {
                foreach (TypeMetadata typeMetadata in TypeData.GenericArguments)
                {
                    children.Add(new TypeTI(TypeSingleton.Instance.Get(typeMetadata.TypeName), ItemTypeEnum.GenericArgument));
                }
            }
            if (TypeData.ImplementedInterfaces != null)
            {
                foreach (TypeMetadata typeMetadata in TypeData.ImplementedInterfaces)
                {
                    children.Add(new TypeTI(TypeSingleton.Instance.Get(typeMetadata.TypeName), ItemTypeEnum.InmplementedInterface));
                }
            }
            if (TypeData.NestedTypes != null)
            {
                foreach (TypeMetadata typeMetadata in TypeData.NestedTypes)
                {
                    ItemTypeEnum type = typeMetadata.Type == TypeEnum.Class ? ItemTypeEnum.NestedClass :
                        typeMetadata.Type == TypeEnum.Struct ? ItemTypeEnum.NestedStructure :
                        typeMetadata.Type == TypeEnum.Enum ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;
                    children.Add(new TypeTI(TypeSingleton.Instance.Get(typeMetadata.TypeName), type));
                }
            }
            if (TypeData.Methods != null)
            {
                foreach (MethodMetadata methodMetadata in TypeData.Methods)
                {
                    children.Add(new MethodTI(methodMetadata, methodMetadata.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method));
                }
            }
            if (TypeData.Constructors != null)
            {
                foreach (MethodMetadata methodMetadata in TypeData.Constructors)
                {
                    children.Add(new MethodTI(methodMetadata, ItemTypeEnum.Constructor));
                }
            }
        }

    }
}
