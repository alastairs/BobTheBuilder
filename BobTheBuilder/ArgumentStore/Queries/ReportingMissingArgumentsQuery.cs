using System;
using System.Collections.Generic;
using System.Linq;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class ReportingMissingArgumentsQuery : IArgumentStoreQuery
    {
        private readonly IArgumentStoreQuery wrappedQuery;

        public ReportingMissingArgumentsQuery(IArgumentStoreQuery wrappedQuery)
        {
            if (wrappedQuery == null)
            {
                throw new ArgumentNullException("wrappedQuery");
            }

            this.wrappedQuery = wrappedQuery;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            var missingArguments = wrappedQuery.Execute(destinationType);
            if (missingArguments.Any())
            {
                var missingMember = missingArguments.First();
                throw new MissingMemberException($"The property \"{missingMember.Name}\" does not exist on \"{destinationType.Name}\"");
            }

            return missingArguments;
        }
    }
}
