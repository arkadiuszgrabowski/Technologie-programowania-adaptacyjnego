using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData.Model
{
    [Table("Type")]
    public class DatabaseType : BaseType
    {
        public DatabaseType()
        {
            //MethodGenericArguments = new List<DatabaseMethod>();
        }
        [Key]
        public override string Name { get; set; }
        public override string AssemblyName { get; set; }
        public override bool IsExternal { get; set; }
        public override bool IsGeneric { get; set; }
        public int? NamespaceId { get; set; }
        public new DatabaseType BaseT { get; set; }
        public override TypeEnum Type { get; set; }
        public new DatabaseType DeclaringType { get; set; }
        public override TypeModifiers Modifiers { get; set; }
        public new List<DatabaseMethod> Constructors { get; set; }
        public new List<DatabaseParameter> Fields { get; set; }
        public new List<DatabaseType> GenericArguments { get; set; }
        public new List<DatabaseType> ImplementedInterfaces { get; set; }
        public new List<DatabaseMethod> Methods { get; set; }
        public new List<DatabaseType> NestedTypes { get; set; }
        public new List<DatabaseProperty> Properties { get; set; }



    }
}
