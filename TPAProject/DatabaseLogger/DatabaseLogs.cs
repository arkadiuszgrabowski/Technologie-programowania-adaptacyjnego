using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLogger
{
    [Table("Logs")]
    public class DatabaseLogs
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public virtual DateTime Time { get; set; }
        public string Level { get; set; }
    }
}
