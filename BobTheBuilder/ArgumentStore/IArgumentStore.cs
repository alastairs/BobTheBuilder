using System.Collections.Generic;

namespace BobTheBuilder.ArgumentStore
{
    internal interface IArgumentStore
    {
        void Set(MemberNameAndValue member);
        void RemoveMemberBy(string name);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
    }
}
