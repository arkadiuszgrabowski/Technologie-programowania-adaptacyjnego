using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [DataContract(IsReference = true)]
    public abstract class BaseNamespace
    {
        [DataMember]
        public virtual string Name { get; set; }
        public virtual List<BaseType> Types { get; set; }
    }
}
