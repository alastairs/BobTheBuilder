using System.Dynamic;

using BobTheBuilder.ArgumentStore;

namespace BobTheBuilder.Syntax
{
    public class MethodSyntaxParser<T> : DynamicBuilderBase<T>, IParser where T: class
    {
        public MethodSyntaxParser(IArgumentStore argumentStore) : base(argumentStore) { }

        public override bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result)
        {
            Parse(binder, args);

            result = this;
            return true;
        }

        public bool Parse(InvokeMemberBinder binder, object[] args)
        {
            var memberName = binder.Name.Replace("With", "");
            argumentStore.SetMemberNameAndValue(memberName, args[0]);
            return true;
        }
    }
}