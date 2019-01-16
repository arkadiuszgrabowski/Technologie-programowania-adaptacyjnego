using DatabaseData.Model;
using System.Data.Entity;

namespace DatabaseData
{
    public class TPADatabaseContext17 : DbContext
    {
        //public DatabaseContext() : base("TPAProjectSerializationDB") {
        //    //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
        //}
        public DbSet<DatabaseAssembly> AssemblyModel { get; set; }
        public DbSet<DatabaseMethod> MethodModel { get; set; }
        public DbSet<DatabaseNamespace> NamespaceModel { get; set; }
        public DbSet<DatabaseParameter> ParameterModel { get; set; }
        public DbSet<DatabaseProperty> PropertyModel { get; set; }
        public DbSet<DatabaseType> TypeModel { get; set; }

    }

}
