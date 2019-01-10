using System.Data.Entity;

namespace DatabaseLogger
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base("name=TPAProjectLoggerDB")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<LoggerContext>());
        }
        public DbSet<DatabaseLogs> Logs { get; set; }
    }
}
