using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using BobTheBuilder.Extensions;

#if NETCOREAPP3_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#else
using JetBrains.Annotations;
#endif


namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class ConstructorArgumentsQuery : IArgumentStoreQuery
    {
        private readonly IArgumentStore argumentStore;

        public ConstructorArgumentsQuery([NotNull]IArgumentStore argumentStore)
        {
            this.argumentStore = argumentStore;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            var singleConstructor = destinationType.GetConstructors().Single();
            var parameterNames = singleConstructor.GetParameters().Select(p => p.Name.ToPascalCase());

            return argumentStore.Remove(parameterNames);
        }
    }
}
