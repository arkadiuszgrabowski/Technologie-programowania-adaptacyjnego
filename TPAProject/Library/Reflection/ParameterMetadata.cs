using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        [DataMember]
        public TypeMetadata Type { get; set; }
        [DataMember]
        public string m_ParameterName { get; set; }

        public ParameterMetadata(string name, TypeMetadata typeModel)
        {
            m_ParameterName = name;
            Type = typeModel;
        }
    }
}
