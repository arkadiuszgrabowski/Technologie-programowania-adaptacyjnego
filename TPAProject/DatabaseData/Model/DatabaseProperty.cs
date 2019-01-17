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
    [Table("PropertyModel")]
    public class DatabaseProperty : BaseProperty
    {
        public DatabaseProperty()
        {
            TypeProperties = new HashSet<DatabaseType>();
        }
        public int Id { get; set; }
        public override string Name { get; set; }
        public new DatabaseType Type { get; set; }

        public virtual ICollection<DatabaseType> TypeProperties { get; set; }

    }
}
