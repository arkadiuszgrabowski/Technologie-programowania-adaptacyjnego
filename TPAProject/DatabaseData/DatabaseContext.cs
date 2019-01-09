using DatabaseData.Model;
using System.Data.Entity;

namespace DatabaseData
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
        }
        public DbSet<DatabaseAssembly> AssemblyModel { get; set; }
        public DbSet<DatabaseMethod> MethodModel { get; set; }
        public DbSet<DatabaseNamespace> NamespaceModel { get; set; }
        public DbSet<DatabaseParameter> ParameterModel { get; set; }
        public DbSet<DatabaseProperty> PropertyModel { get; set; }
        public DbSet<DatabaseType> TypeModel { get; set; }
        public DbSet<DatabaseLogs> Logs { get; set; }
    }
}
