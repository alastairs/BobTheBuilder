using System.Collections.Generic;

namespace BobTheBuilder.ArgumentStore
{
    public interface IArgumentStore
    {
        void SetMemberNameAndValue(string name, object value);

        IEnumerable<MemberNameAndValue> GetAllStoredMembers();
    }
}