using System.Collections.Generic;

namespace BobTheBuilder
{
    public interface IArgumentStore
    {
        void SetMemberNameAndValue(string name, object value);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
    }
}