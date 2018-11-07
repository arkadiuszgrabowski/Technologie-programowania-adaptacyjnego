using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class TypeMetadata
    {
        public static Dictionary<string, TypeMetadata> TypeDictionary = new Dictionary<string, TypeMetadata>();

        public string m_typeName { get; set; }
        public string m_NamespaceName { get; set; }
        public TypeMetadata m_BaseType { get; set; }
        public List<TypeMetadata> m_GenericArguments { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> m_Modifiers { get; set; }
        public TypeEnum m_Type { get; set; }
        public List<TypeMetadata> m_ImplementedInterfaces { get; set; }
        public List<TypeMetadata> m_NestedTypes { get; set; }
        public List<PropertyMetadata> m_Properties { get; set; }
        public TypeMetadata m_DeclaringType { get; set; }
        public List<MethodMetadata> m_Methods { get; set; }
        public List<MethodMetadata> m_Constructors { get; set; }
        public List<ParameterMetadata> Fields { get; set; }

        public TypeMetadata(Type type)
        {
            m_typeName = type.Name;
            if (!TypeDictionary.ContainsKey(m_typeName))
            {
                TypeDictionary.Add(m_typeName, this);
            }

            m_Type = GetTypeEnum(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Modifiers = EmitModifiers(type);

            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitConstructors(type);
            m_Methods = MethodMetadata.EmitMethods(type);
            m_NestedTypes = EmitNestedTypes(type);
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            m_Properties = PropertyMetadata.EmitProperties(type);
            Fields = EmitFields(type);
        }

        private TypeMetadata(string typeName, string namespaceName)
        {
            this.m_typeName = typeName;
            this.m_NamespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            this.m_GenericArguments = genericArguments.ToList();
        }


        public static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.GetNamespace());

            return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type));
        }
        public static List<TypeMetadata> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }

            return arguments.Select(EmitReference).ToList();
        }

        public static void StoreType(Type type)
        {
            if (!TypeDictionary.ContainsKey(type.Name))
            {
                new TypeMetadata(type);
            }
        }

        private static List<ParameterMetadata> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterMetadata> parameters = new List<ParameterMetadata>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterMetadata(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }
        private List<TypeMetadata> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return nestedTypes.Select(t => new TypeMetadata(t)).ToList();
        }
        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            foreach (Type @interface in interfaces)
            {
                StoreType(@interface);
            }

            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeEnum GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeEnum.Enum :
                   type.IsValueType ? TypeEnum.Struct :
                   type.IsInterface ? TypeEnum.Interface :
                   TypeEnum.Class;
        }

        static Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Private;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }



            return new Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum>(_access, _sealed, _abstract, _static);
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            return EmitReference(baseType);
        }
    }
}
