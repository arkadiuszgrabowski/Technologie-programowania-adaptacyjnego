using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData.Model
{
    [Table("Parameter")]
    public class DatabaseParameter : BaseParameter
    {
        public DatabaseParameter()
        {

        }
        public int Id { get; set; }
        public override string Name { get; set; }
        public new DatabaseType Type { get; set; }

    }
}
