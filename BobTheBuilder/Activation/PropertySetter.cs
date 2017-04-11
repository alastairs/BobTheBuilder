using BobTheBuilder.ArgumentStore.Queries;
using BobTheBuilder.Extensions;

using JetBrains.Annotations;

namespace BobTheBuilder.Activation
{
    internal class PropertySetter
    {
        private readonly IArgumentStoreQuery propertyValuesQuery;

        public PropertySetter([NotNull]IArgumentStoreQuery propertyValuesQuery)
        {
            this.propertyValuesQuery = propertyValuesQuery;
        }

        public T PopulatePropertiesOn<T>(T instance) where T: class
        {
            var destinationType = typeof(T);
            var propertyValues = propertyValuesQuery.Execute(destinationType);

            foreach (var member in propertyValues)
            {
                var property = destinationType.GetProperty(member.Name);
                property.SetValue(instance, member.Value);
            }

            return instance;
        }
    }
}
