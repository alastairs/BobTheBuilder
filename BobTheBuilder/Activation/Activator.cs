using BobTheBuilder.ArgumentStore.Queries;
using System;

namespace BobTheBuilder.Activation
{
    internal class Activator
    {
        private IArgumentStoreQuery constructorArgumentsQuery;
        private IArgumentStoreQuery missingArgumentsQuery;
        private IArgumentStoreQuery propertyValuesQuery;

        public Activator(IArgumentStoreQuery missingArgumentsQuery, IArgumentStoreQuery constructorArgumentsQuery, IArgumentStoreQuery propertyValuesQuery)
        {
            if (missingArgumentsQuery == null)
            {
                throw new ArgumentNullException("missingArgumentsQuery");
            }

            if (constructorArgumentsQuery == null)
            {
                throw new ArgumentNullException("constructorArgumentsQuery");
            }

            if (propertyValuesQuery == null)
            {
                throw new ArgumentNullException("propertyValuesQuery");
            }

            this.missingArgumentsQuery = missingArgumentsQuery;
            this.constructorArgumentsQuery = constructorArgumentsQuery;
            this.propertyValuesQuery = propertyValuesQuery;
        }

        public T Activate<T>() where T: class
        {
            new MissingArgumentsReporter(missingArgumentsQuery).Report(typeof(T));
            var instance = new InstanceCreator(constructorArgumentsQuery).CreateInstanceOf<T>();
            new PropertySetter(propertyValuesQuery).PopulatePropertiesOn(instance);

            return instance;
        }
    }
}
