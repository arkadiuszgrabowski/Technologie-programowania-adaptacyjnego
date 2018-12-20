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
    public class XmlParameter : BaseParameter
    {
        [DataMember] public override string Name { get; set; }

        [DataMember] public new XmlType Type { get; set; }
    }
}
