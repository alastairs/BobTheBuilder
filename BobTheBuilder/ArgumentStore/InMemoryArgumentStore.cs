using System.Collections.Generic;
using System.Linq;
using BobTheBuilder.Extensions;
using JetBrains.Annotations;

namespace BobTheBuilder.ArgumentStore
{
    internal class InMemoryArgumentStore : IArgumentStore
    {
        private readonly IDictionary<string, object> _members = new Dictionary<string, object>();

        public void SetMemberNameAndValue([NotNull]string name, [NotNull]object value)
        {
            _members[name] = value;
        }

        public IEnumerable<MemberNameAndValue> GetAllStoredMembers()
        {
            return _members.Select(m => new MemberNameAndValue(m.Key.ToPascalCase(), m.Value));
        }

        public void RemoveMemberBy(string name)
        {
            _members.Remove(name);
        }
    }
}