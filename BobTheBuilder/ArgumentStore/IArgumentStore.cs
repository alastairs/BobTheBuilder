using System.Collections.Generic;

namespace BobTheBuilder.ArgumentStore
{
    internal interface IArgumentStore
    {
        void SetMemberNameAndValue(string name, object value);
        void RemoveMemberBy(string name);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
    }
}