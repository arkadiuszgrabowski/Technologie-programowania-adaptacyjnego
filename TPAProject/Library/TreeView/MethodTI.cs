﻿using Data;
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
        public MethodTI(MethodMetadata methodMetadata, ItemTypeEnum type) : base(GetModifiers(methodMetadata) + methodMetadata.Name, type)
        {
            MethodMetadata = methodMetadata;
        }
        public static string GetModifiers(MethodMetadata model)
        {
            string type = null;
            type += model.Modifiers.AccessLevel.ToString().ToLower() + " ";
            type += model.Modifiers.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
            type += model.Modifiers.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
            type += model.Modifiers.VirtualEnum == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : String.Empty;
            return type;
        }

        protected override void BuildMyself(ObservableCollection<TreeViewItem> children)
        {
            if (MethodMetadata.GenericArguments != null)
            {
                foreach (TypeMetadata genericArgument in MethodMetadata.GenericArguments)
                {
                    children.Add(new TypeTI(TypeSingleton.Instance.Get(genericArgument.Name), ItemTypeEnum.GenericArgument));
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
                children.Add(new TypeTI(TypeSingleton.Instance.Get(MethodMetadata.ReturnType.Name), ItemTypeEnum.ReturnType));
            }
        }
    }
}
