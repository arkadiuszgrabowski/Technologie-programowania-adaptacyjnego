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
    public class PropertyMapper
    {
        public PropertyMetadata MapUp(BaseProperty model)
        {
            PropertyMetadata propertyModel = new PropertyMetadata();
            propertyModel.m_PropertyName = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseType typeModel = (BaseType)typeProperty?.GetValue(model);

            if (typeModel != null)
                propertyModel.Type = TypeMapper.EmitType(typeModel);

            return propertyModel;
        }

        public BaseProperty MapDown(PropertyMetadata model, Type propertyModelType)
        {
            object propertyModel = Activator.CreateInstance(propertyModelType);
            PropertyInfo nameProperty = propertyModelType.GetProperty("Name");
            PropertyInfo typeProperty = propertyModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(propertyModel, model.m_PropertyName);

            if (model.Type != null)
                typeProperty?.SetValue(propertyModel,
                    typeProperty.PropertyType.Cast(TypeMapper.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BaseProperty)propertyModel;
        }
    }
}
