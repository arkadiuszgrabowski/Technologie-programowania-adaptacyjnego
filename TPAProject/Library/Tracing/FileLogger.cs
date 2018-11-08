using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tracing
{
    public class FileLogger : ILogger
    {
        private TraceListener _traceListener;
       
        public FileLogger(String fileName, String name)
        {
            _traceListener = new TextWriterTraceListener(fileName, name);
        }

        public void Log(string message, LevelEnum level)
        {
            _traceListener.WriteLine(message + " (" + DateTime.Now + ")", level.ToString());
            _traceListener.Flush();
        }
    }
}
