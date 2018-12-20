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
    public class MethodMapper
    {
        public MethodMetadata MapUp(BaseMethod model)
        {
            MethodMetadata methodModel = new MethodMetadata();
            methodModel.m_MethodName = model.Name;
            methodModel.Extension = model.Extension;
            Type type = model.GetType();
            PropertyInfo genericArgumentsProperty = type.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (genericArgumentsProperty?.GetValue(model) != null)
            {
                List<BaseType> genericArguments =
                    (List<BaseType>)Helper.ConvertList(typeof(BaseType),
                        (IList)genericArgumentsProperty?.GetValue(model));
                methodModel.GenericArguments =
                    genericArguments.Select(g => TypeMapper.EmitType(g)).ToList();
            }

            methodModel.Modifiers = model.Modifiers;

            PropertyInfo parametersProperty = type.GetProperty("Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (parametersProperty?.GetValue(model) != null)
            {
                List<BaseParameter> parameters =
                    (List<BaseParameter>)Helper.ConvertList(typeof(BaseParameter),
                        (IList)parametersProperty?.GetValue(model));

                methodModel.Parameters = parameters
                    .Select(p => new ParameterMapper().MapUp(p)).ToList();
            }

            PropertyInfo returnTypeProperty = type.GetProperty("ReturnType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType returnType = (BaseType)returnTypeProperty?.GetValue(model);
            if (returnType != null)
                methodModel.ReturnType = TypeMapper.EmitType(returnType);
            return methodModel;
        }

        public BaseMethod MapDown(MethodMetadata model, Type methodModelType)
        {
            object methodModel = Activator.CreateInstance(methodModelType);
            PropertyInfo nameProperty = methodModelType.GetProperty("Name");
            PropertyInfo extensionProperty = methodModelType.GetProperty("Extension");
            PropertyInfo genericArgumentsProperty = methodModelType.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo modifiersProperty = methodModelType.GetProperty("Modifiers");
            PropertyInfo parametersProperty = methodModelType.GetProperty("Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo returnTypeProperty = methodModelType.GetProperty("ReturnType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            nameProperty?.SetValue(methodModel, model.m_MethodName);
            extensionProperty?.SetValue(methodModel, model.Extension);
            if (model.GenericArguments != null)
                genericArgumentsProperty?.SetValue(methodModel,
                    Helper.ConvertList(genericArgumentsProperty.PropertyType.GetGenericArguments()[0],
                        model.GenericArguments.Select(t => TypeMapper.EmitBaseType(t, genericArgumentsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            modifiersProperty?.SetValue(methodModel, model.Modifiers);
            if (model.Parameters != null)
                parametersProperty?.SetValue(methodModel,
                    Helper.ConvertList(parametersProperty.PropertyType.GetGenericArguments()[0],
                        model.Parameters.Select(p => new ParameterMapper().MapDown(p, parametersProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            if (model.ReturnType != null)
                returnTypeProperty?.SetValue(methodModel,
                    returnTypeProperty.PropertyType.Cast(TypeMapper.EmitBaseType(model.ReturnType, returnTypeProperty.PropertyType)));

            return (BaseMethod)methodModel;
        }
    }
}
