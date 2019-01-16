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
    [Table("ParameterModel")]
    public class DatabaseParameter : BaseParameter
    {
        public DatabaseParameter()
        {
            MethodParameters = new HashSet<DatabaseMethod>();
            TypeFields = new HashSet<DatabaseType>();
        }
        public int Id { get; set; }
        public override string Name { get; set; }
        public new DatabaseType Type { get; set; }

        public virtual ICollection<DatabaseMethod> MethodParameters { get; set; }

        public virtual ICollection<DatabaseType> TypeFields { get; set; }
    }
}
