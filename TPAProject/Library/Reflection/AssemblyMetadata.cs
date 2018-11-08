using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class AssemblyMetadata
    {
        public AssemblyMetadata(Assembly assembly)
        {
            m_Name = assembly.ManifestModule.Name;
            m_Namespaces = (from Type _type in assembly.GetTypes()
                where _type.IsVisible
                group _type by _type.Namespace into _group
                orderby _group.Key
                select new NamespaceMetadata(_group.Key, _group)).ToList();
        }

        public string m_Name { get; set; }
        public List<NamespaceMetadata> m_Namespaces { get; set; }
    }
}
