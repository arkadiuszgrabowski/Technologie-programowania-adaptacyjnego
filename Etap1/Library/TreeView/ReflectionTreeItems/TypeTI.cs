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
            if (TypeData.m_BaseType != null)
            {
                children.Add(new TypeTI(TypeMetadata.TypeDictionary[TypeData.m_BaseType.m_typeName], ItemTypeEnum.BaseType));
            }
            if (TypeData.m_DeclaringType != null)
            {
                children.Add(new TypeTI(TypeMetadata.TypeDictionary[TypeData.m_DeclaringType.m_typeName], ItemTypeEnum.Type));
            }
            if (TypeData.m_Properties != null)
            {
                foreach (PropertyMetadata propertyMetadata in TypeData.m_Properties)
                {
                    children.Add(new PropertyTI(propertyMetadata, GetModifiers(propertyMetadata.Type) + propertyMetadata.Type.m_typeName + " " + propertyMetadata.m_PropertyName));
                }
            }
            if (TypeData.Fields != null)
            {
                foreach (ParameterMetadata parameterMetadata in TypeData.Fields)
                {
                    children.Add(new ParameterTI(parameterMetadata, ItemTypeEnum.Field));
                }
            }
            if (TypeData.m_GenericArguments != null)
            {
                foreach (TypeMetadata typeMetadata in TypeData.m_GenericArguments)
                {
                    children.Add(new TypeTI(TypeMetadata.TypeDictionary[typeMetadata.m_typeName], ItemTypeEnum.GenericArgument));
                }
            }
            if (TypeData.m_ImplementedInterfaces != null)
            {
                foreach (TypeMetadata typeMetadata in TypeData.m_ImplementedInterfaces)
                {
                    children.Add(new TypeTI(TypeMetadata.TypeDictionary[typeMetadata.m_typeName], ItemTypeEnum.InmplementedInterface));
                }
            }
            if (TypeData.m_NestedTypes != null)
            {
                foreach (TypeMetadata typeMetadata in TypeData.m_NestedTypes)
                {
                    ItemTypeEnum type = typeMetadata.m_Type == TypeEnum.Class ? ItemTypeEnum.NestedClass :
                        typeMetadata.m_Type == TypeEnum.Struct ? ItemTypeEnum.NestedStructure :
                        typeMetadata.m_Type == TypeEnum.Enum ? ItemTypeEnum.NestedEnum : ItemTypeEnum.NestedType;
                    children.Add(new TypeTI(TypeMetadata.TypeDictionary[typeMetadata.m_typeName], type));
                }
            }
            if (TypeData.m_Methods != null)
            {
                foreach (MethodMetadata methodMetadata in TypeData.m_Methods)
                {
                    children.Add(new MethodTI(methodMetadata, methodMetadata.Extension ? ItemTypeEnum.ExtensionMethod : ItemTypeEnum.Method));
                }
            }
            if (TypeData.m_Constructors != null)
            {
                foreach (MethodMetadata methodMetadata in TypeData.m_Constructors)
                {
                    children.Add(new MethodTI(methodMetadata, ItemTypeEnum.Constructor));
                }
            }
        }

    }
}
