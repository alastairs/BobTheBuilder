using System;
using System.Dynamic;

using BobTheBuilder.ArgumentStore;

namespace BobTheBuilder.Syntax
{
    internal class MethodSyntaxParser : IParser
    {
        private readonly IArgumentStore argumentStore;

        public MethodSyntaxParser(IArgumentStore argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

        public bool Parse(InvokeMemberBinder binder, object[] args)
        {
            var memberName = binder.Name.Replace("With", "");
            argumentStore.SetMemberNameAndValue(memberName, args[0]);
            return true;
        }
    }
}