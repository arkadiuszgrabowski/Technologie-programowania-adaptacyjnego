using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using Contracts;
using System.ComponentModel.Composition;

namespace XMLSerializer
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        public string path = @"model.xml";
        public XMLSerializer(string _path)
        {
            path = _path;
        }
        public XMLSerializer()
        {
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
