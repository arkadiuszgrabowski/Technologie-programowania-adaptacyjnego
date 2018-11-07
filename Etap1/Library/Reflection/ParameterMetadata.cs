using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    class ParameterMetadata
    {
        public TypeMetadata Type { get; set; }
        public string m_ParameterName;

        public ParameterMetadata(string name, TypeMetadata typeModel)
        {
            m_ParameterName = name;
            Type = typeModel;
        }
    }
}
