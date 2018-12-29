using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;

namespace Data
{
    public abstract class BaseAssembly
    {
        public virtual string Name { get; set; }

        public virtual List<BaseNamespace> NamespaceModels { get; set; }
    }
}
