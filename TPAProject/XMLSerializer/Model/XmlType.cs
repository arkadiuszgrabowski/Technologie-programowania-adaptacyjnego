using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.Model
{
    public class XmlType : BaseType
    {
        public override string Name { get; set; }
        public override string AssemblyName { get; set; }
        public override bool IsExternal { get; set; }
        public override bool IsGeneric { get; set; }

        public new XmlType BaseT { get; set; }
        public new List<XmlType> GenericArguments { get; set; }
        public override TypeModifiers Modifiers { get; set; }
        public override TypeEnum Type { get; set; }
        public new List<XmlType> ImplementedInterfaces { get; set; }
        public new List<XmlType> NestedTypes { get; set; }
        public new List<XmlProperty> Properties { get; set; }
        public new XmlType DeclaringType { get; set; }
        public new List<XmlMethod> Methods { get; set; }
        public new List<XmlMethod> Constructors { get; set; }
        public new List<XmlParameter> Fields { get; set; }
    }
}
