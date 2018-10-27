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
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            //m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            //m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            //m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            //m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            //rekurencja property ma typ never ending story
            //Dictionary singleton albo property zarządzające
            //statyczne proprerty klucz nazwa typu, daj mnie ten typ emitreference, dopisac metode
            //jezeli nie ma obiektu w dictionary to tworzymy typ, pusta klasa bez szczególów i referencja do obiektu.
            m_TypeKind = GetTypeKind(type);
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
        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }
        #endregion

        #region private
        //vars
        public string m_typeName;
        private string m_NamespaceName;
        private TypeMetadata m_BaseType;
        private IEnumerable<TypeMetadata> m_GenericArguments;
        //private Tuple<AccessLevel, SealedEnum, AbstractENum> m_Modifiers;
        private TypeKind m_TypeKind;
        private IEnumerable<Attribute> m_Attributes;
        private IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        private IEnumerable<TypeMetadata> m_NestedTypes;
        //private IEnumerable<PropertyMetadata> m_Properties;
        private TypeMetadata m_DeclaringType;
        //private IEnumerable<MethodMetadata> m_Methods;
        //private IEnumerable<MethodMetadata> m_Constructors;
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
        private static TypeKind GetTypeKind(Type type) //#80 TPA: Reflection - Invalid return value of GetTypeKind() 
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }
        //static Tuple<AccessLevel, SealedEnum, AbstractENum> EmitModifiers(Type type)
        //{
        //    //set defaults 
        //    AccessLevel _access = AccessLevel.IsPrivate;
        //    AbstractENum _abstract = AbstractENum.NotAbstract;
        //    SealedEnum _sealed = SealedEnum.NotSealed;
        //    // check if not default 
        //    if (type.IsPublic)
        //        _access = AccessLevel.IsPublic;
        //    else if (type.IsNestedPublic)
        //        _access = AccessLevel.IsPublic;
        //    else if (type.IsNestedFamily)
        //        _access = AccessLevel.IsProtected;
        //    else if (type.IsNestedFamANDAssem)
        //        _access = AccessLevel.IsProtectedInternal;
        //    if (type.IsSealed)
        //        _sealed = SealedEnum.Sealed;
        //    if (type.IsAbstract)
        //        _abstract = AbstractENum.Abstract;
        //    return new Tuple<AccessLevel, SealedEnum, AbstractENum>(_access, _sealed, _abstract);
        //}
        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        #endregion
    }
}
