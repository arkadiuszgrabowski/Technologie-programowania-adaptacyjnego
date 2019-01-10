using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData.Model
{
    [Table("Namespace")]
    public class DatabaseNamespace : BaseNamespace
    {
        public int Id { get; set; }
        public override string Name { get; set; }
        public new List<DatabaseType> Types { get; set; }
    }
}
