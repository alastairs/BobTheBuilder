using BobTheBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class ConstructorArgumentsQuery
    {
        private readonly IArgumentStore argumentStore;

        public ConstructorArgumentsQuery(IArgumentStore argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            var arguments = destinationType.GetConstructors().Single().GetParameters().ToLookup(p => p.Name);
            var constructorArguments = argumentStore.GetAllStoredMembers().Where(member => arguments.Select(a => a.Key.ToPascalCase()).Contains(member.Name)).ToList();
            foreach (var constructorArgument in constructorArguments)
            {
                argumentStore.RemoveMemberByName(constructorArgument.Name);
            }

            return constructorArguments;
        }
    }
}
