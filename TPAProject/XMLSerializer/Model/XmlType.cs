using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.Model
{
    [DataContract(IsReference = true)]
    public class XmlType : BaseType
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public override string AssemblyName { get; set; }
        [DataMember] public override bool IsExternal { get; set; }
        [DataMember] public override bool IsGeneric { get; set; }

        [DataMember] public new XmlType BaseT { get; set; }
        [DataMember] public new List<XmlType> GenericArguments { get; set; }
        [DataMember] public override TypeModifiers Modifiers { get; set; }
        [DataMember] public override TypeEnum Type { get; set; }
        [DataMember] public new List<XmlType> ImplementedInterfaces { get; set; }
        [DataMember] public new List<XmlType> NestedTypes { get; set; }
        [DataMember] public new List<XmlProperty> Properties { get; set; }
        [DataMember] public new XmlType DeclaringType { get; set; }
        [DataMember] public new List<XmlMethod> Methods { get; set; }
        [DataMember] public new List<XmlMethod> Constructors { get; set; }
        [DataMember] public new List<XmlParameter> Fields { get; set; }
    }
}
