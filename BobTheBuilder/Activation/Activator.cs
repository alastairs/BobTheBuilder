using BobTheBuilder.ArgumentStore.Queries;
using JetBrains.Annotations;

namespace BobTheBuilder.Activation
{
    internal class Activator
    {
        private readonly IArgumentStoreQuery constructorArgumentsQuery;
        private readonly IArgumentStoreQuery missingArgumentsQuery;
        private readonly IArgumentStoreQuery propertyValuesQuery;

        public Activator([NotNull]IArgumentStoreQuery missingArgumentsQuery, 
                         [NotNull]IArgumentStoreQuery constructorArgumentsQuery, 
                         [NotNull]IArgumentStoreQuery propertyValuesQuery)
        {
            this.missingArgumentsQuery = missingArgumentsQuery;
            this.constructorArgumentsQuery = constructorArgumentsQuery;
            this.propertyValuesQuery = propertyValuesQuery;
        }

        public T Activate<T>() where T: class
        {
            new MissingArgumentsReporter(missingArgumentsQuery).Report(typeof(T));
            var instance = new InstanceCreator(constructorArgumentsQuery).CreateInstanceOf<T>();
            return new PropertySetter(propertyValuesQuery).PopulatePropertiesOn(instance);
        }
    }
}
