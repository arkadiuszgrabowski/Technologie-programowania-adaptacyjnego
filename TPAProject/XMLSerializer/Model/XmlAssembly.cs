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
    [Export(typeof(BaseAssembly))]
    public class XmlAssembly : BaseAssembly
    {

        public XmlAssembly() { }
        public override string Name { get; set; }


        public new List<XmlNamespace> NamespaceModels { get; set; }

    }
}
