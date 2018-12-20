using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class PropertyMetadata
    {
        public TypeMetadata Type { get; set; }
        public string m_PropertyName { get; set; }

        public PropertyMetadata()
        {

        }
        public PropertyMetadata(string name, TypeMetadata propertyType)
        {
            m_PropertyName = name;
            Type = propertyType;
        }

        public static List<PropertyMetadata> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyMetadata(t.Name, TypeMetadata.EmitReference(t.PropertyType))).ToList();
        }
    }
}
