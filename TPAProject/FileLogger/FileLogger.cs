using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLogger
{
    [Export(typeof(ILogger))]
    public class FileLogger : ILogger
    {
        private TraceListener _traceListener;
       
        public FileLogger(String fileName, String name)
        {
            _traceListener = new TextWriterTraceListener(fileName, name);
        }

        public FileLogger()
        {
            _traceListener = new TextWriterTraceListener("Logs.txt", "Tracing");
        }

        public void Log(string message, LevelEnum level)
        {
            _traceListener.WriteLine(message + " (" + DateTime.Now + ")", level.ToString());
            _traceListener.Flush();
        }
    }
}
