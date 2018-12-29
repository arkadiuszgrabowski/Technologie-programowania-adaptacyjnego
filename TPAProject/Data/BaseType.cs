using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    
    public abstract class BaseType
    {
        public virtual string Name { get; set; }
        public virtual string AssemblyName { get; set; }
        public virtual bool IsExternal { get; set; }
        public virtual bool IsGeneric { get; set; }
        public virtual string NamespaceName { get; set; }
        public virtual BaseType BaseT { get; set; }
        public virtual List<BaseType> GenericArguments { get; set; }
        public virtual TypeModifiers Modifiers { get; set; }
        public virtual TypeEnum Type { get; set; }
        public virtual List<BaseType> ImplementedInterfaces { get; set; }
        public virtual List<BaseType> NestedTypes { get; set; }
        public virtual List<BaseProperty> Properties { get; set; }
        public virtual BaseType DeclaringType { get; set; }
        public virtual List<BaseMethod> Methods { get; set; }
        public virtual List<BaseMethod> Constructors { get; set; }
        public virtual List<BaseParameter> Fields { get; set; }
    }
}
