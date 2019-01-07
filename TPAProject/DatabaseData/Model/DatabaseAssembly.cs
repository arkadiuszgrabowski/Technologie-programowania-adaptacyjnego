using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData.Model
{
    public class DatabaseAssembly : BaseAssembly
    {
        public int Id { get; set; }


        public override string Name { get; set; }
        public new List<DatabaseNamespace> NamespaceModels { get; set; }
    }
}
