using System.Collections.Generic;

namespace BobTheBuilder.ArgumentStore
{
    internal interface IArgumentStore
    {
        void Set(MemberNameAndValue member);
        IEnumerable<MemberNameAndValue> Remove(IEnumerable<string> names);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
    }
}
