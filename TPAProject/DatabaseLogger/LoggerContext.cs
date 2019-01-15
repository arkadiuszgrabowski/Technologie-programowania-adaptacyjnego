using System.Data.Entity;

namespace DatabaseLogger
{
    public class TPALoggerContext : DbContext
    {
        public DbSet<DatabaseLogs> Logs { get; set; }
    }
}
