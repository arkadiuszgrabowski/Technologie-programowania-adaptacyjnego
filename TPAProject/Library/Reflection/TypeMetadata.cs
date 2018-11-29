using Library.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    [DataContract(IsReference = true)]
    public class TypeMetadata
    {
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public string AssemblyName { get; set; }
        [DataMember]
        public bool IsExternal { get; set; } = true;
        [DataMember]
        public bool IsGeneric { get; set; }
        [DataMember]
        public string NamespaceName { get; set; }
        [DataMember]
        public TypeMetadata BaseType { get; set; }
        [DataMember]
        public List<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public TypeEnum Type { get; set; }
        [DataMember]
        public List<TypeMetadata> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeMetadata> NestedTypes { get; set; }
        [DataMember]
        public List<PropertyMetadata> Properties { get; set; }
        [DataMember]
        public TypeMetadata DeclaringType { get; set; }
        [DataMember]
        public List<MethodMetadata> Methods { get; set; }
        [DataMember]
        public List<MethodMetadata> Constructors { get; set; }
        [DataMember]
        public List<ParameterMetadata> Fields { get; set; }

        public TypeMetadata(Type type)
        {
            TypeName = type.Name;
            //if (!TypeSingleton.Instance.ContainsKey(TypeName))
            //{
            //    TypeSingleton.Instance.Add(TypeName, this);
            //}
            IsGeneric = type.IsGenericParameter;
            AssemblyName = type.AssemblyQualifiedName;


        }

        private void Analyze(Type type)
        {
            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            Modifiers = EmitModifiers(type);

            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodMetadata.EmitConstructors(type);
            Methods = MethodMetadata.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = PropertyMetadata.EmitProperties(type);
            Fields = EmitFields(type);
            IsExternal = false;
            _isAnalyzed = true;
        }

        private TypeMetadata(string typeName, string namespaceName)
        {
            this.TypeName = typeName;
            this.NamespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            this.GenericArguments = genericArguments.ToList();
        }

        public static TypeMetadata EmitType(Type type)
        {
            if (!TypeSingleton.Instance.ContainsKey(type.Name))
            {
                TypeSingleton.Instance.Add(type.Name, new TypeMetadata(type));
            }

            if (!TypeSingleton.Instance.Get(type.Name)._isAnalyzed)
            {
                TypeSingleton.Instance.Get(type.Name).Analyze(type);
            }

            return TypeSingleton.Instance.Get(type.Name);
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
            if (!TypeSingleton.Instance.ContainsKey(type.Name))
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
        private bool _isAnalyzed = false;
    }
}
