using Data;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseData.Model
{
    [Export(typeof(BaseAssembly))]
    [Table("AssemblyModel")]
    public class DatabaseAssembly : BaseAssembly
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Name { get; set; }
        public new List<DatabaseNamespace> NamespaceModels { get; set; }
    }
}
