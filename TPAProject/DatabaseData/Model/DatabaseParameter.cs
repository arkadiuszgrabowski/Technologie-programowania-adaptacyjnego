using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData.Model
{
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
