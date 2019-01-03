using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerializer.Model
{
    public class XmlMethod : BaseMethod
    {
        public override string Name { get; set; }

        public new List<XmlType> GenericArguments { get; set; }

        public new XmlType ReturnType { get; set; }

        public override bool Extension { get; set; }

        public new List<XmlParameter> Parameters { get; set; }

        public override MethodModifiers Modifiers { get; set; }
    }
}
