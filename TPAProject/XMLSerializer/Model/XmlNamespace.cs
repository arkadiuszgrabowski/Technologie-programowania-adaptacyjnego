using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.Model
{
    public class XmlNamespace : BaseNamespace
    {
        public override string Name { get; set; }

        public new List<XmlType> Types { get; set; }
    }
}
