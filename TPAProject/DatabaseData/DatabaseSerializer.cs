using Contracts;
using Data;
using DatabaseData.Model;
using System.ComponentModel.Composition;

namespace DatabaseData
{
    [Export(typeof(ISerializer))]
    class DatabaseSerializer : ISerializer
    {
        public BaseAssembly Deserialize()
        {
            throw new System.NotImplementedException();
        }

        public string GetPath()
        {
            throw new System.NotImplementedException();
        }

        public bool IsDeserializationPossible()
        {
            throw new System.NotImplementedException();
        }

        public void Serialize(BaseAssembly _object)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                DatabaseAssembly assemblyModel = (DatabaseAssembly)_object;
                context.AssemblyModel.Add(assemblyModel);
                context.SaveChanges();
            }
        }
    }
}
