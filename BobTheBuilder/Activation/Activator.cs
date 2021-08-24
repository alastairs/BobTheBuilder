using BobTheBuilder.ArgumentStore.Queries;
using JetBrains.Annotations;

namespace BobTheBuilder.Activation
{
    internal class Activator
    {
        private readonly IArgumentStoreQuery constructorArgumentsQuery;
        private readonly IArgumentStoreQuery missingPropertiesQuery;
        private readonly IArgumentStoreQuery propertyValuesQuery;

        public Activator([NotNull]IArgumentStoreQuery missingPropertiesQuery,
                         [NotNull]IArgumentStoreQuery constructorArgumentsQuery, 
                         [NotNull]IArgumentStoreQuery propertyValuesQuery)
        {
            this.missingPropertiesQuery = missingPropertiesQuery;
            this.constructorArgumentsQuery = constructorArgumentsQuery;
            this.propertyValuesQuery = propertyValuesQuery;
        }

        public T Activate<T>() where T: class
        {
            new MissingPropertiesReporter(missingPropertiesQuery).Report(typeof(T));
            var instance = new InstanceCreator(constructorArgumentsQuery).CreateInstanceOf<T>();
            return new PropertySetter(propertyValuesQuery).PopulatePropertiesOn(instance);
        }
    }
}
