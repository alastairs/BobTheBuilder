using System.Dynamic;
using BobTheBuilder.ArgumentStore;
using BobTheBuilder.Syntax;

namespace BobTheBuilder;

internal class RecordSyntaxParser : IParser
{
    private readonly InMemoryArgumentStore _argumentStore;

    public RecordSyntaxParser(InMemoryArgumentStore argumentStore) =>
        _argumentStore = argumentStore;

    public bool Parse(InvokeMemberBinder binder, object[] args)
    {
        var memberName = binder.Name;
        if (!memberName.StartsWith("With") || memberName == "With")
        {
            return false;
        }

        _argumentStore.Set(new MemberNameAndValue(memberName.Replace("With", ""), args[0]));
        return true;
    }
}