using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.Model
{
    public class XmlParameter : BaseParameter
    {
        public override string Name { get; set; }

        public new XmlType Type { get; set; }
    }
}
