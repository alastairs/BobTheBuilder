using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BobTheBuilder.Extensions;
using BobTheBuilder.ArgumentStore.Queries;

namespace BobTheBuilder.ArgumentStore
{
    internal class InMemoryArgumentStore : IArgumentStore
    {
        private readonly IDictionary<string, object> _members = new Dictionary<string, object>();

        public void SetMemberNameAndValue(string name, object value)
        {
            _members[name] = value;
        }

        public IEnumerable<MemberNameAndValue> GetAllStoredMembers()
        {
            return _members.Select(m => new MemberNameAndValue(m.Key.ToPascalCase(), m.Value));
        }

        public IEnumerable<MemberNameAndValue> GetMissingArguments(ILookup<string, PropertyInfo> properties)
        {
            return new MissingArgumentsQuery(this).Execute(properties);
        }

        public IEnumerable<MemberNameAndValue> GetConstructorArguments(ILookup<string, ParameterInfo> arguments)
        {
            var constructorArguments = GetAllStoredMembers().Where(member => arguments.Select(a => a.Key.ToPascalCase()).Contains(member.Name)).ToList();
            foreach (var constructorArgument in constructorArguments)
            {
                RemoveMemberByName(constructorArgument.Name);
            }

            return constructorArguments;
        }

        public IEnumerable<MemberNameAndValue> GetPropertyValues(ILookup<string, PropertyInfo> properties)
        {
            return new PropertyValuesQuery(this).Execute(properties);
        }

        public void RemoveMemberByName(string name)
        {
            _members.Remove(name);
        }
    }
}