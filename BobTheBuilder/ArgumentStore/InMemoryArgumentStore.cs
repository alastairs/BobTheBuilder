using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BobTheBuilder.Extensions;

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
            return GetAllStoredMembers().Where(member => !properties.Contains(member.Name));
        }

        public IEnumerable<MemberNameAndValue> GetConstructorArguments(ILookup<string, ParameterInfo> arguments)
        {
            var constructorArguments = GetAllStoredMembers().Where(member => arguments.Select(a => a.Key.ToPascalCase()).Contains(member.Name)).ToList();
            foreach (var constructorArgument in constructorArguments)
            {
                _members.Remove(constructorArgument.Name);
            }

            return constructorArguments;
        }

        public IEnumerable<MemberNameAndValue> GetPropertyValues(ILookup<string, PropertyInfo> properties)
        {
            return GetAllStoredMembers().Where(member => properties.Contains(member.Name));
        }
    }
}