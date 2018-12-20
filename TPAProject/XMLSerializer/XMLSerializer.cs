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
        public void Serialize(BaseAssembly _object)
        {
            XmlAssembly assembly = (XmlAssembly)_object;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XmlAssembly));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                dataContractSerializer.WriteObject(fileStream, assembly);
            }
        }

        public BaseAssembly Deserialize()
        {
            XmlAssembly model;
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(XmlAssembly));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                model = (XmlAssembly)dataContractSerializer.ReadObject(fileStream);
            }
            return model;
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
