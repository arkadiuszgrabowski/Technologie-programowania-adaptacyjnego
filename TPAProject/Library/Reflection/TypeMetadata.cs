using Data;
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
    public class TypeMetadata : BaseType
    {
        public override string Name { get; set; }
        public override string AssemblyName { get; set; }
        public override bool IsExternal { get; set; } = true;
        public override bool IsGeneric { get; set; }
        public override string NamespaceName { get; set; }
        public new TypeMetadata BaseT { get; set; }
        public new List<TypeMetadata> GenericArguments { get; set; }
        public override TypeModifiers Modifiers { get; set; }
        public override TypeEnum Type { get; set; }
        public new List<TypeMetadata> ImplementedInterfaces { get; set; }
        public new List<TypeMetadata> NestedTypes { get; set; }
        public new List<PropertyMetadata> Properties { get; set; }
        public new TypeMetadata DeclaringType { get; set; }
        public new List<MethodMetadata> Methods { get; set; }
        public new List<MethodMetadata> Constructors { get; set; }
        public new List<ParameterMetadata> Fields { get; set; }

        public TypeMetadata(Type type)
        {
            Name = type.Name;
            IsGeneric = type.IsGenericParameter;
            AssemblyName = type.AssemblyQualifiedName;
        }

        public TypeMetadata()
        {

        }

        private void Analyze(Type type)
        {
            Type = GetTypeEnum(type);
            BaseT = EmitExtends(type.BaseType);
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
            this.Name = typeName;
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
            if (!TypeSingleton.Instance.ContainsKey(type.Name))
            {
                TypeSingleton.Instance.Add(type.Name, new TypeMetadata(type));
            }
            return TypeSingleton.Instance.Get(type.Name);
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
                TypeMetadata.EmitType(type);
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

        static TypeModifiers EmitModifiers(Type type)
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

            return new TypeModifiers()
            {
                AbstractEnum = _abstract,
                AccessLevel = _access,
                SealedEnum = _sealed,
                StaticEnum = _static
            };
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
