using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    [DataContract]
    public class AssemblyMetadata
    {
        public AssemblyMetadata(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            m_Namespaces = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceMetadata(t.Key, t.ToList())).ToList();
        }

        [DataMember]
        public string m_Name { get; set; }

        [DataMember]
        public List<NamespaceMetadata> m_Namespaces { get; set; }
    }
}
