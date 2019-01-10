using System.Data.Entity;

namespace DatabaseLogger
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base("TPAProjectLoggerDB")
        {
        }
        public DbSet<DatabaseLogs> Logs { get; set; }
    }
}
