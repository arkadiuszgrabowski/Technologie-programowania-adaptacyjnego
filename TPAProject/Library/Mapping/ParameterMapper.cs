using Data;
using Library.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Mappers
{
    public class ParameterMapper
    {
        public ParameterMetadata MapUp(BaseParameter model)
        {
            ParameterMetadata parameterModel = new ParameterMetadata();
            parameterModel.m_ParameterName = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType typeModel = (BaseType)typeProperty?.GetValue(model);
            if (typeModel != null)
                parameterModel.Type = TypeMapper.EmitType(typeModel);
            return parameterModel;
        }

        public BaseParameter MapDown(ParameterMetadata model, Type parameterModelType)
        {
            object parameterModel = Activator.CreateInstance(parameterModelType);
            PropertyInfo nameProperty = parameterModelType.GetProperty("Name");
            PropertyInfo typeProperty = parameterModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(parameterModel, model.m_ParameterName);
            if (model.Type != null)
                typeProperty?.SetValue(parameterModel,
                    typeProperty.PropertyType.Cast(TypeMapper.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BaseParameter)parameterModel;
        }
    }
}
