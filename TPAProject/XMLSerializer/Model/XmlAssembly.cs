using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.Model
{
    [DataContract(IsReference = true)]
    [Export(typeof(BaseAssembly))]
    public class XmlAssembly : BaseAssembly
    {

        [DataMember] public override string Name { get; set; }


        [DataMember] public new List<XmlNamespace> Namespaces { get; set; }

    }
}
