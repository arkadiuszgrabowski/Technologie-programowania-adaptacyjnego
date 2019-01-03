using System.IO;
using Contracts;
using System.ComponentModel.Composition;
using Data;
using XMLSerializer.Model;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;

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
            XmlAssembly assembly = _object as XmlAssembly;
            string name = JsonConvert.SerializeObject(assembly, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            XNode node = JsonConvert.DeserializeXNode(name, "Root");
            using (StreamWriter file = new StreamWriter(path, false))
            {
                file.Write(node);
            }
        }

        public BaseAssembly Deserialize()
        {
            XmlAssembly model;
            using (StreamReader file = new StreamReader(path, false))
            {
                string reader = file.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader);
                string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
                model = JsonConvert.DeserializeObject<XmlAssembly>(json, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
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
