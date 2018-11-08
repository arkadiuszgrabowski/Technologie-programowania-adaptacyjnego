using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TreeView.ReflectionTreeItems
{
    public enum ItemTypeEnum
    {
        Assembly, Namespace, Method, Type, Parameter, Property, ReturnType,
        GenericArgument, InmplementedInterface, NestedClass, NestedStructure, NestedEnum, NestedType, Constructor, Field,
        BaseType, Class, Enum, Struct, Interface, ExtensionMethod
    }
}
