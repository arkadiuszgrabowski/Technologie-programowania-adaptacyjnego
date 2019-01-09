using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData.Model
{
    public class DatabaseMethod : BaseMethod
    {
        public DatabaseMethod()
        {
            GenericArguments = new List<DatabaseType>();
            Parameters = new List<DatabaseParameter>();
        }

        public int Id { get; set; }
        public override string Name { get; set; }
        public override bool Extension { get; set; }
        public override MethodModifiers Modifiers { get; set; }
        public new DatabaseType ReturnType { get; set; }
        public new List<DatabaseType> GenericArguments { get; set; }
        public new List<DatabaseParameter> Parameters { get; set; }


    }
}
