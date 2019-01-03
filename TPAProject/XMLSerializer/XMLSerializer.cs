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
            XDocument node = JsonConvert.DeserializeXNode(name, "Root", true);

            node.Save(path);
        }

        public BaseAssembly Deserialize()
        {
            XmlAssembly model;
            XDocument doc = XDocument.Load(path);
            string json = JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.Indented, true);
            json = json.Remove(0, 58);
            model = JsonConvert.DeserializeObject<XmlAssembly>(json, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
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
