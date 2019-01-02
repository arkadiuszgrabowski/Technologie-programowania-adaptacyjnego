using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;

namespace Data
{
    [DataContract(IsReference = true)]
    public abstract class BaseAssembly
    {
        [DataMember]
        public virtual string Name { get; set; }

        public virtual List<BaseNamespace> NamespaceModels { get; set; }
    }
}
