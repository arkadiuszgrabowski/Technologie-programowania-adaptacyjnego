using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class NamespaceMetadata
    {
        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = types.OrderBy(t => t.Name).Select(TypeMetadata.EmitType).ToList();
        }
        public NamespaceMetadata()
        {

        }
        public string m_NamespaceName { get; set; }
        public List<TypeMetadata> m_Types { get; set; }
    }
}
