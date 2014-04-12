using System;
using System.Dynamic;

using BobTheBuilder.ArgumentStore;

namespace BobTheBuilder.Syntax
{
    public class MethodSyntaxParser<T> : DynamicBuilder<T>, IParser where T: class
    {
        private readonly IArgumentStore argumentStore;

        public MethodSyntaxParser(IArgumentStore argumentStore) : base(argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

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