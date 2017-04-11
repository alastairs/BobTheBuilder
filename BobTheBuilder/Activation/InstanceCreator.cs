using System.Linq;

using BobTheBuilder.ArgumentStore.Queries;
using BobTheBuilder.Extensions;

using JetBrains.Annotations;

namespace BobTheBuilder.Activation
{
    internal class InstanceCreator
    {
        private readonly IArgumentStoreQuery constructorArgumentsQuery;

        public InstanceCreator([NotNull]IArgumentStoreQuery constructorArgumentsQuery)
        {
            this.constructorArgumentsQuery = constructorArgumentsQuery;
        }

        public T CreateInstanceOf<T>() where T: class
        {
            var instanceType = typeof(T);
            var constructor = instanceType.GetConstructors().Single();
            var constructorArguments = constructorArgumentsQuery.Execute(instanceType);
            return constructor.Invoke(constructorArguments.Select(arg => arg.Value).ToArray()) as T;
        }
    }
}
