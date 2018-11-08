using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class NamespaceMetadata
    {
        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = (from type in types orderby type.Name select new TypeMetadata(type)).ToList();
        }
        public string m_NamespaceName;
        public List<TypeMetadata> m_Types;
    }
}
