using DatabaseData.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseData
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DatabaseAssembly> AssemblyModel { get; set; }
        public DbSet<DatabaseMethod> MethodModel { get; set; }
        public DbSet<DatabaseNamespace> NamespaceModel { get; set; }
        public DbSet<DatabaseParameter> ParameterModel { get; set; }
        public DbSet<DatabaseProperty> PropertyModel { get; set; }
        public DbSet<DatabaseType> TypeModel { get; set; }
    }
}
