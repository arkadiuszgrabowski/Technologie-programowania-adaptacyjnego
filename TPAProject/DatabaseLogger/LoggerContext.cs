using System;
using System.Data.Entity;

namespace DatabaseLogger
{
    public class TPALoggerContext : DbContext
    {
        public TPALoggerContext() : base(GetString())
        {
        }

        private static String GetString()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            path = path.Remove(path.Length - 10);
            return "data source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=" + path + "\\TPALoggerDB.mdf;integrated security = True; MultipleActiveResultSets=True;App=EntityFramework";
        }
        public DbSet<DatabaseLogs> Logs { get; set; }
    }
}
