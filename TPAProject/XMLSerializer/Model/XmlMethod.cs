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
    public class XmlMethod : BaseMethod
    {
        [DataMember] public override string Name { get; set; }

        [DataMember] public new List<XmlType> GenericArguments { get; set; }

        [DataMember] public new XmlType ReturnType { get; set; }

        [DataMember] public override bool Extension { get; set; }

        [DataMember] public new List<XmlParameter> Parameters { get; set; }

        [DataMember] public override MethodModifiers Modifiers { get; set; }
    }
}
