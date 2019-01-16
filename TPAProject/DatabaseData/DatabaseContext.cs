using DatabaseData.Model;
using System;
using System.Data.Entity;

namespace DatabaseData
{
    public class TPADatabaseContext : DbContext
    {
        public TPADatabaseContext() : base(GetString())
        {
        }

        private static String GetString()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            path = path.Remove(path.Length - 10);
            return "data source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=" + path + "\\TPASerializationDB.mdf;integrated security = True; MultipleActiveResultSets=True;App=EntityFramework";
        }

        public DbSet<DatabaseAssembly> AssemblyModel { get; set; }
        public DbSet<DatabaseMethod> MethodModel { get; set; }
        public DbSet<DatabaseNamespace> NamespaceModel { get; set; }
        public DbSet<DatabaseParameter> ParameterModel { get; set; }
        public DbSet<DatabaseProperty> PropertyModel { get; set; }
        public DbSet<DatabaseType> TypeModel { get; set; }

    }

}
