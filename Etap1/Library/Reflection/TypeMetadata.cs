using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Reflection
{
    public class TypeMetadata
    {
        #region constructors
        public TypeMetadata(Type type)
        {
            m_typeName = type.Name;
            if (!TypeDictionary.ContainsKey(m_typeName))
            {
                TypeDictionary.Add(m_typeName, this);
            }
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitMethods(type);
            m_Methods = MethodMetadata.EmitMethods(type);
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Properties = PropertyMetadata.EmitProperties(type);
            //rekurencja property ma typ never ending story
            //Dictionary singleton albo property zarządzające
            //statyczne proprerty klucz nazwa typu, daj mnie ten typ emitreference, dopisac metode
            //jezeli nie ma obiektu w dictionary to tworzymy typ, pusta klasa bez szczególów i referencja do obiektu.
            m_Type = GetTypeEnum(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }
        #endregion

        #region API
        public enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }
        public static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.Namespace);
            //zmiana GetNamespace na namespace
            else
                return new TypeMetadata(type.Name, type.Namespace, EmitGenericArguments(type.GetGenericArguments()));
            //zmiana GetNamespace na namespace
        }
        public static void StoreType(Type type)
        {
            if (!TypeDictionary.ContainsKey(type.Name))
            {
                new TypeMetadata(type);
            }
        }
        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }
        #endregion

        #region private
        //vars
        public static Dictionary<string, TypeMetadata> TypeDictionary = new Dictionary<string, TypeMetadata>();
        public string m_typeName;
        private string m_NamespaceName;
        public TypeMetadata m_BaseType;
        private IEnumerable<TypeMetadata> m_GenericArguments;
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> m_Modifiers;
        public TypeEnum m_Type { get; set; }
        private IEnumerable<Attribute> m_Attributes;
        private IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        private IEnumerable<TypeMetadata> m_NestedTypes;
        private IEnumerable<PropertyMetadata> m_Properties;
        private TypeMetadata m_DeclaringType;
        private IEnumerable<MethodMetadata> m_Methods;
        private IEnumerable<MethodMetadata> m_Constructors;
        //constructors
        private TypeMetadata(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }
        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            m_GenericArguments = genericArguments;
        }
        //methods
        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }
        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.IsVisible
                // zmiana GetVisible na isvisible
                   select new TypeMetadata(_type);
        }
        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
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
            return EmitReference(baseType);
        }
        #endregion
    }
}
