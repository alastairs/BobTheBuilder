using System;
using System.Linq;
using BobTheBuilder.ArgumentStore.Queries;
using JetBrains.Annotations;

namespace BobTheBuilder.Activation
{
    internal class MissingPropertiesReporter
    {
        private readonly IArgumentStoreQuery missingPropertiesQuery;

        public MissingPropertiesReporter([NotNull]IArgumentStoreQuery missingPropertiesQuery)
        {
            this.missingPropertiesQuery = missingPropertiesQuery;
        }

        public void Report(Type destinationType)
        {
            var missingProperties = missingPropertiesQuery.Execute(destinationType).ToList();
            if (missingProperties.Any())
            {
                var missingMember = missingProperties.First();
                throw new MissingMemberException($"The property \"{missingMember.Name}\" does not exist on \"{destinationType.Name}\"");
            }
        }
    }
}
