using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BobTheBuilder.ArgumentStore
{
    public interface IArgumentStore
    {
        void SetMemberNameAndValue(string name, object value);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
        IEnumerable<MemberNameAndValue> GetMissingArguments(ILookup<string, PropertyInfo> properties);
    }
}