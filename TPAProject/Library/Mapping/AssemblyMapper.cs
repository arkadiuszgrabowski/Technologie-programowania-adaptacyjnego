using Data;
using Library.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Library.Mappers
{
    public class AssemblyMapper
    {
        public static AssemblyMetadata MapUp(BaseAssembly model)
        {
            AssemblyMetadata assemblyModel = new AssemblyMetadata();
            Type type = model.GetType();
            assemblyModel.Name = model.Name;
            PropertyInfo namespaceModelsProperty = type.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseNamespace> namespaceModels = (List<BaseNamespace>)Helper.ConvertList(typeof(BaseNamespace), (IList)namespaceModelsProperty?.GetValue(model));
            if (namespaceModels != null)
                assemblyModel.NamespaceModels = namespaceModels.Select(n => new NamespaceMapper().MapUp(n)).ToList();
            return assemblyModel;
        }

        public static BaseAssembly MapDown(AssemblyMetadata model, Type assemblyModelType)
        {
            object assemblyModel = Activator.CreateInstance(assemblyModelType);
            PropertyInfo nameProperty = assemblyModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = assemblyModelType.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(assemblyModel, model.Name);
            namespaceModelsProperty?.SetValue(
                assemblyModel,
                Helper.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.NamespaceModels.Select(n => new NamespaceMapper().MapDown(n, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            return (BaseAssembly)assemblyModel;
        }
    }
}
