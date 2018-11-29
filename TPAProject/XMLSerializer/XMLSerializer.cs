using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Library.Serialization;
using System.IO;

namespace XMLSerializer
{
    public class XMLSerializer : ISerializer
    {
        public string path = @".";
        public XMLSerializer(string _path)
        {
            path = _path;
        }
        public void Serialize<T>(T _object)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, _object);
            }
        }

        public T Deserialize<T>()
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (T)dataContractSerializer.ReadObject(fileStream);
            }
        }

        public string GetPath()
        {
            return path;
        }

        public bool IsDeserializationPossible()
        {
            return File.Exists(path);
        }
    }
}
