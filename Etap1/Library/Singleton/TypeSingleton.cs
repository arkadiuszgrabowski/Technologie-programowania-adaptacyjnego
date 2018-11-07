using Library.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Singleton
{
    public sealed class TypeSingleton
    {
        private static TypeSingleton instance = new TypeSingleton();

        public static TypeSingleton Instance
        {
            get { return instance; }
        }

        private Dictionary<string, TypeMetadata> _data = new Dictionary<string, TypeMetadata>();
        private TypeSingleton()
        {

        }

        public void Add(string name, TypeMetadata type)
        {
            _data.Add(name, type);
        }

        public bool ContainsKey(string name)
        {
            return _data.ContainsKey(name);
        }

        public TypeMetadata Get(string key)
        {
            _data.TryGetValue(key, out TypeMetadata value);
            return value;
        }
    }
}
