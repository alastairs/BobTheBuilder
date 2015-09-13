using System;
using System.Collections.Generic;
using System.Linq;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class MissingArgumentsQuery
    {
        private readonly IArgumentStore argumentStore;

        public MissingArgumentsQuery(IArgumentStore argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            var properties = destinationType.GetProperties().Select(p => p.Name);
            return argumentStore.GetAllStoredMembers().Where(member => !properties.Contains(member.Name));
        }
    }
}
