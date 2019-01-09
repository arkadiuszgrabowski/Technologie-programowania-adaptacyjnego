using Contracts;
using DatabaseData;
using DatabaseData.Model;
using System;
using System.ComponentModel.Composition;

namespace DatabaseLogger
{
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        public void Log(string message, LevelEnum level)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Logs.Add(new DatabaseLogs
                {
                    Message = message,
                    Time = DateTime.Now,
                    Level = level.ToString()
                });
            }
        }
    }
}
