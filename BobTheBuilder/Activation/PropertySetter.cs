using BobTheBuilder.ArgumentStore.Queries;
using System;

namespace BobTheBuilder.Activation
{
    internal class PropertySetter
    {
        private PropertyValuesQuery propertyValuesQuery;

        public PropertySetter(PropertyValuesQuery propertyValuesQuery)
        {
            if (propertyValuesQuery == null)
            {
                throw new ArgumentNullException("propertyValuesQuery");
            }

            this.propertyValuesQuery = propertyValuesQuery;
        }

        public void PopulatePropertiesOn<T>(T instance) where T: class
        {
            var destinationType = typeof(T);
            var propertyValues = propertyValuesQuery.Execute(destinationType);

            foreach (var member in propertyValues)
            {
                var property = destinationType.GetProperty(member.Name);
                property.SetValue(instance, member.Value);
            }
        }
    }
}
