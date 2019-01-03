using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class NamespaceMetadata : BaseNamespace
    {
        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(TypeMetadata.EmitType).ToList();
        }
        public NamespaceMetadata()
        {

        }
        public override string Name { get; set; }
        public new List<TypeMetadata> Types { get; set; }
    }
}
