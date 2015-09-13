using BobTheBuilder.ArgumentStore.Queries;
using System;
using System.Linq;

namespace BobTheBuilder.Activation
{
    internal class MissingArgumentsReporter
    {
        private readonly IArgumentStoreQuery wrappedQuery;

        public MissingArgumentsReporter(IArgumentStoreQuery wrappedQuery)
        {
            if (wrappedQuery == null)
            {
                throw new ArgumentNullException("wrappedQuery");
            }

            this.wrappedQuery = wrappedQuery;
        }

        public void Report(Type destinationType)
        {
            var missingArguments = wrappedQuery.Execute(destinationType);
            if (missingArguments.Any())
            {
                var missingMember = missingArguments.First();
                throw new MissingMemberException($"The property \"{missingMember.Name}\" does not exist on \"{destinationType.Name}\"");
            }
        }
    }
}
