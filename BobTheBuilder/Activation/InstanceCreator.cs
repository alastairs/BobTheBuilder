using System.Linq;
using System.Reflection;

using BobTheBuilder.ArgumentStore.Queries;

#if NETCOREAPP3_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#else
using JetBrains.Annotations;
#endif


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
