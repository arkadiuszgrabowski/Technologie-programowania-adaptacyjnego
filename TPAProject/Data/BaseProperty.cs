using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BaseProperty
    {
        public virtual string Name { get; set; }
        public virtual BaseType Type { get; set; }
    }
}
