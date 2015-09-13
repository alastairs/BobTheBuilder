using System.Linq;
using BobTheBuilder.ArgumentStore.Queries;
using System;

namespace BobTheBuilder.Activation
{
    internal class InstanceCreator
    {
        private IArgumentStoreQuery constructorArgumentsQuery;

        public InstanceCreator(IArgumentStoreQuery constructorArgumentsQuery)
        {
            if (constructorArgumentsQuery == null)
            {
                throw new ArgumentNullException("constructorArgumentsQuery");
            }

            this.constructorArgumentsQuery = constructorArgumentsQuery;
        }

        public T CreateInstanceOf<T>() where T: class
        {
            var instanceType = typeof(T);
            var constructor = instanceType.GetConstructors().Single();
            var constructorArguments = constructorArgumentsQuery.Execute(instanceType);
            return (T)constructor.Invoke(constructorArguments.Select(arg => arg.Value).ToArray());
        }
    }
}
