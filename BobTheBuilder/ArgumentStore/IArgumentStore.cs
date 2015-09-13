using System.Collections.Generic;

namespace BobTheBuilder.ArgumentStore
{
    internal interface IArgumentStore
    {
        void SetMemberNameAndValue(string name, object value);
        void RemoveMemberByName(string name);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
    }
}