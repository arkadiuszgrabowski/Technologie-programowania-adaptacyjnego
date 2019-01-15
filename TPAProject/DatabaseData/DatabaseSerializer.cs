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
            Clear();
            using (DatabaseContext context = new DatabaseContext())
            {
                DatabaseAssembly assemblyModel = (DatabaseAssembly)_object;
                context.AssemblyModel.Add(assemblyModel);
                context.SaveChanges();
            }
        }
        private void Clear()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ParameterModel WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM PropertyModel WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM MethodModel WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM TypeModel ");
                context.Database.ExecuteSqlCommand("DELETE FROM NamespaceModel WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM AssemblyModel WHERE ID != -1");
                context.SaveChanges();
            }
        }
    }
}
