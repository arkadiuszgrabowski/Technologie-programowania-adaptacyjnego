using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tracing
{
    public interface ILogger
    {
        void Log(String message, LevelEnum level);
    }
}
