using System;
using System.Collections.Generic;
using System.Linq;
#if NETSTANDARD1_1 || NETSTANDARD1_6
using BobTheBuilder.Extensions;
#endif
using JetBrains.Annotations;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class MissingPropertiesQuery : IArgumentStoreQuery
    {
        private readonly IArgumentStore argumentStore;

        public MissingPropertiesQuery([NotNull]IArgumentStore argumentStore)
        {
            this.argumentStore = argumentStore;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            var properties = destinationType.GetProperties().Select(p => p.Name);
            return argumentStore.GetAllStoredMembers().Where(member => !properties.Contains(member.Name));
        }
    }
}
