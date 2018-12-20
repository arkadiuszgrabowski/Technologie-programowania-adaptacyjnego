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
    public class TypeMapper
    {
        public static Dictionary<string, BaseType> BaseTypes = new Dictionary<string, BaseType>();
        public static Dictionary<string, TypeMetadata> Types = new Dictionary<string, TypeMetadata>();

        public static BaseType EmitBaseType(TypeMetadata model, Type type)
        {
            return new TypeMapper().MapDown(model, type);
        }

        public static TypeMetadata EmitType(BaseType model)
        {
            return new TypeMapper().MapUp(model);
        }

        private void FillBaseType(TypeMetadata model, BaseType typModel)
        {
            Type typeModelType = typModel.GetType();

            typeModelType.GetProperty("Name")?.SetValue(typModel, model.TypeName);
            typeModelType.GetProperty("IsExternal")?.SetValue(typModel, model.IsExternal);
            typeModelType.GetProperty("IsGeneric")?.SetValue(typModel, model.IsGeneric);
            typeModelType.GetProperty("Type")?.SetValue(typModel, model.Type);
            typeModelType.GetProperty("AssemblyName")?.SetValue(typModel, model.AssemblyName);
            typeModelType.GetProperty("Modifiers")?.SetValue(typModel, model.Modifiers ?? new TypeModifiers());

            if (model.BaseType != null)
            {
                typeModelType.GetProperty("BaseType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typModel, typeModelType.Cast(EmitBaseType(model.BaseType, typeModelType)));
            }

            if (model.DeclaringType != null)
            {
                typeModelType.GetProperty("DeclaringType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typModel, typeModelType.Cast(EmitBaseType(model.DeclaringType, typeModelType)));
            }

            if (model.NestedTypes != null)
            {
                PropertyInfo nestedTypesProperty = typeModelType.GetProperty("NestedTypes",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                nestedTypesProperty?.SetValue(typModel,
                    Helper.ConvertList(typeModelType,
                        model.NestedTypes?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.GenericArguments != null)
            {
                PropertyInfo genericArgumentsProperty = typeModelType.GetProperty("GenericArguments",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                genericArgumentsProperty?.SetValue(typModel,
                    Helper.ConvertList(typeModelType,
                        model.GenericArguments?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.ImplementedInterfaces != null)
            {
                PropertyInfo implementedInterfacesProperty = typeModelType.GetProperty("ImplementedInterfaces",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                implementedInterfacesProperty?.SetValue(typModel,
                    Helper.ConvertList(typeModelType,
                        model.ImplementedInterfaces?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.Fields != null)
            {
                PropertyInfo fieldsProperty = typeModelType.GetProperty("Fields",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                fieldsProperty?.SetValue(typModel,
                    Helper.ConvertList(fieldsProperty.PropertyType.GetGenericArguments()[0],
                        model.Fields?.Select(c =>
                            new ParameterMapper().MapDown(c,
                                fieldsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Methods != null)
            {
                PropertyInfo methodsProperty = typeModelType.GetProperty("Methods",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                methodsProperty?.SetValue(typModel,
                    Helper.ConvertList(methodsProperty.PropertyType.GetGenericArguments()[0],
                        model.Methods?.Select(m =>
                                new MethodMapper().MapDown(m,
                                    methodsProperty?.PropertyType.GetGenericArguments()[0]))
                            .ToList()));
            }

            if (model.Constructors != null)
            {
                PropertyInfo constructorsProperty = typeModelType.GetProperty("Constructors",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                constructorsProperty?.SetValue(typModel,
                    Helper.ConvertList(constructorsProperty.PropertyType.GetGenericArguments()[0],
                        model.Constructors?.Select(c =>
                            new MethodMapper().MapDown(c,
                                constructorsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Properties != null)
            {
                PropertyInfo propertiesProperty = typeModelType.GetProperty("Properties",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                propertiesProperty?.SetValue(typModel,
                    Helper.ConvertList(propertiesProperty.PropertyType.GetGenericArguments()[0],
                        model.Properties?.Select(c =>
                            new PropertyMapper().MapDown(c,
                                propertiesProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }
        }

        private void FillType(BaseType model, TypeMetadata typeModel)
        {
            typeModel.TypeName = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            typeModel.AssemblyName = model.AssemblyName;
            typeModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            Type type = model.GetType();
            PropertyInfo baseTypeProperty = type.GetProperty("BaseType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType baseType = (BaseType)baseTypeProperty?.GetValue(model);
            typeModel.BaseType = EmitType(baseType);

            PropertyInfo declaringTypeProperty = type.GetProperty("DeclaringType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType declaringType = (BaseType)declaringTypeProperty?.GetValue(model);
            typeModel.DeclaringType = EmitType(declaringType);

            PropertyInfo nestedTypesProperty = type.GetProperty("NestedTypes",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (nestedTypesProperty?.GetValue(model) != null)
            {
                List<BaseType> nestedTypes = (List<BaseType>)Helper.ConvertList(typeof(BaseType),
                    (IList)nestedTypesProperty?.GetValue(model));
                typeModel.NestedTypes = nestedTypes?.Select(n => EmitType(n)).ToList();
            }

            PropertyInfo genericArgumentsProperty = type.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (genericArgumentsProperty?.GetValue(model) != null)
            {
                List<BaseType> genericArguments =
                    (List<BaseType>)Helper.ConvertList(typeof(BaseType),
                        (IList)genericArgumentsProperty?.GetValue(model));
                typeModel.GenericArguments = genericArguments?.Select(g => EmitType(g)).ToList();
            }

            PropertyInfo implementedInterfacesProperty = type.GetProperty("ImplementedInterfaces",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (implementedInterfacesProperty?.GetValue(model) != null)
            {
                List<BaseType> implementedInterfaces =
                    (List<BaseType>)Helper.ConvertList(typeof(BaseType),
                        (IList)implementedInterfacesProperty?.GetValue(model));
                typeModel.ImplementedInterfaces =
                    implementedInterfaces?.Select(i => EmitType(i)).ToList();
            }

            PropertyInfo fieldsProperty = type.GetProperty("Fields",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (fieldsProperty?.GetValue(model) != null)
            {
                List<BaseParameter> fields =
                    (List<BaseParameter>)Helper.ConvertList(typeof(BaseParameter),
                        (IList)fieldsProperty?.GetValue(model));
                typeModel.Fields = fields?.Select(g => new ParameterMapper().MapUp(g))
                    .ToList();
            }

            PropertyInfo methodsProperty = type.GetProperty("Methods",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (methodsProperty?.GetValue(model) != null)
            {
                List<BaseMethod> methods = (List<BaseMethod>)Helper.ConvertList(typeof(BaseMethod),
                    (IList)methodsProperty?.GetValue(model));
                typeModel.Methods = methods?.Select(c => new MethodMapper().MapUp(c)).ToList();
            }

            PropertyInfo constructorsProperty = type.GetProperty("Constructors",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (constructorsProperty?.GetValue(model) != null)
            {
                List<BaseMethod> constructors =
                    (List<BaseMethod>)Helper.ConvertList(typeof(BaseMethod),
                        (IList)constructorsProperty?.GetValue(model));
                typeModel.Constructors = constructors?.Select(c => new MethodMapper().MapUp(c))
                    .ToList();
            }

            PropertyInfo propertiesProperty = type.GetProperty("Properties",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (propertiesProperty?.GetValue(model) != null)
            {
                List<BaseProperty> properties =
                    (List<BaseProperty>)Helper.ConvertList(typeof(BaseProperty),
                        (IList)propertiesProperty?.GetValue(model));
                typeModel.Properties = properties?.Select(g => new PropertyMapper().MapUp(g))
                    .ToList();
            }
        }


        public TypeMetadata MapUp(BaseType model)
        {
            TypeMetadata typeModel = new TypeMetadata();
            if (model == null)
                return null;

            if (!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, typeModel);
                FillType(model, typeModel);
            }
            return Types[model.Name];

        }

        public BaseType MapDown(TypeMetadata model, Type typeModelType)
        {

            object typeModel = Activator.CreateInstance(typeModelType);
            if (model == null)
                return null;
            if (!BaseTypes.ContainsKey(model.TypeName))
            {
                BaseTypes.Add(model.TypeName, (BaseType)typeModel);
                FillBaseType(model, (BaseType)typeModel);
            }
            return BaseTypes[model.TypeName];
        }
    }
}
