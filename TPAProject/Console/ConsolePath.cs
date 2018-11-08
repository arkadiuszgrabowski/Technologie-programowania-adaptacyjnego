using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class ConsolePath : IOpenDialogPath
    {
        public string GetPath()
        {
            string path = System.Console.ReadLine();

            if (path != null && File.Exists(path) && path.Contains(".dll"))
            {
                return path;
            }
            System.Console.WriteLine("Wrong path!");
            return null;
        }
    }
}
