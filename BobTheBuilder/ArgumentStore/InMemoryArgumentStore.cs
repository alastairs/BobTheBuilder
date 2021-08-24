using System.Collections.Generic;
using System.Linq;
using BobTheBuilder.Extensions;
#if NETCOREAPP3_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#else
using JetBrains.Annotations;
#endif


namespace BobTheBuilder.ArgumentStore
{
    internal class InMemoryArgumentStore : IArgumentStore
    {
        private readonly IDictionary<string, object> _members = new Dictionary<string, object>();

        public void Set([NotNull]MemberNameAndValue member)
        {
            _members[member.Name] = member.Value;
        }

        public IEnumerable<MemberNameAndValue> Remove(IEnumerable<string> names)
        {
            var members = GetAllStoredMembers().Where(member => names.Contains(member.Name)).ToList();
            foreach (var member in members)
            {
                _members.Remove(member.Name);
            }

            return members;
        }

        public IEnumerable<MemberNameAndValue> GetAllStoredMembers()
        {
            return _members.Select(m => new MemberNameAndValue(m.Key.ToPascalCase(), m.Value));
        }
    }
}
