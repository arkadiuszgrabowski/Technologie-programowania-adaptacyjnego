using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using Contracts;
using System.ComponentModel.Composition;
using Data;
using XMLSerializer.Model;
using System.Xml.Serialization;
using System.Diagnostics;

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
            XmlAssembly assembly = _object as XmlAssembly;
            Debug.WriteLine(assembly.Name);
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XmlAssembly));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assembly);
            }
        }
        // Deserializacja prawdopodobnie zwraca zły typ
        public T Deserialize<T>()
        {
            XmlAssembly model;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XmlAssembly));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                model = (XmlAssembly)dataContractSerializer.ReadObject(fileStream);
            }
            return (T) Convert.ChangeType(model, typeof(T));
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
