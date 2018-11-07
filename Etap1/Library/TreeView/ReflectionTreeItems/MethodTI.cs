using Library.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TreeView.ReflectionTreeItems
{
    class MethodTI
    {
        public MethodMetadata MethodMetadata { get; set; }
        public string Name { get; set; }
        public MethodTI(MethodMetadata methodMetadata, ItemTypeEnum type)
        {
            MethodMetadata = methodMetadata;
            Name = GetModifiers(methodMetadata) + methodMetadata.m_MethodName;
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
    }
}
