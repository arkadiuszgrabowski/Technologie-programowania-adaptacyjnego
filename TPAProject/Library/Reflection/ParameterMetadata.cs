using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class ParameterMetadata : BaseParameter
    {
        public new TypeMetadata Type { get; set; }
        public override string Name { get; set; }

        public ParameterMetadata(string name, TypeMetadata typeModel)
        {
            Name = name;
            Type = typeModel;
        }

        public ParameterMetadata()
        {

        }
    }
}
