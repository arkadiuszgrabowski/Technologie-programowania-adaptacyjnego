using Data;
using Library.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mappers
{
    public class NamespaceMapper
    {
        public NamespaceMetadata MapUp(BaseNamespace model)
        {
            NamespaceMetadata namespaceModel = new NamespaceMetadata();
            namespaceModel.m_NamespaceName = model.Name;
            Type type = model.GetType();
            PropertyInfo typesProperty = type.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseType> types = (List<BaseType>)Helper.ConvertList(typeof(BaseType), (IList)typesProperty?.GetValue(model));
            if (types != null)
                namespaceModel.m_Types = types.Select(n => TypeMapper.EmitType(n)).ToList();
            return namespaceModel;
        }

        public BaseNamespace MapDown(NamespaceMetadata model, Type namespaceModelType)
        {
            object namespaceModel = Activator.CreateInstance(namespaceModelType);
            PropertyInfo nameProperty = namespaceModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = namespaceModelType.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(namespaceModel, model.m_NamespaceName);
            namespaceModelsProperty?.SetValue(namespaceModel,
                Helper.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.m_Types.Select(t => new TypeMapper().MapDown(t, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));

            return (BaseNamespace)namespaceModel;
        }
    }
}
