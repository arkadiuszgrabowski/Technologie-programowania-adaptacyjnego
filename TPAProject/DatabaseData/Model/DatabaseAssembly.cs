using Data;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace DatabaseData.Model
{
    [Export(typeof(BaseAssembly))]
    public class DatabaseAssembly : BaseAssembly
    {
        public int Id { get; set; }


        public override string Name { get; set; }
        public new List<DatabaseNamespace> NamespaceModels { get; set; }
    }
}
