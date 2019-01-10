using Contracts;
using System;
using System.ComponentModel.Composition;

namespace DatabaseLogger
{
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        public void Log(string message, LevelEnum level, DateTime time)
        {
            using (LoggerContext context = new LoggerContext())
            {
                context.Logs.Add(new DatabaseLogs
                {
                    Message = message,
                    Time = time,
                    Level = level.ToString()
                });
                context.SaveChanges();
            }
        }
    }
}
