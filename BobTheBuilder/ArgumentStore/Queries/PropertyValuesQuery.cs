using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class PropertyValuesQuery
    {
        private readonly IArgumentStore argumentStore;

        public PropertyValuesQuery(IArgumentStore argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

        public IEnumerable<MemberNameAndValue> Execute(ILookup<string, PropertyInfo> properties)
        {
            return argumentStore.GetAllStoredMembers().Where(member => properties.Contains(member.Name));
        }
    }
}
