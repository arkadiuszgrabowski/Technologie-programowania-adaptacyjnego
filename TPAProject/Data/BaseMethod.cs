using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Data
{
    [DataContract(IsReference = true)]
    public abstract class BaseMethod
    {
        [DataMember]
        public virtual string Name { get; set; }
        public virtual List<BaseType> GenericArguments { get; set; }
        [DataMember]
        public virtual MethodModifiers Modifiers { get; set; }
        public virtual BaseType ReturnType { get; set; }
        [DataMember]
        public virtual bool Extension { get; set; }
        public virtual List<BaseParameter> Parameters { get; set; }
    }
}
