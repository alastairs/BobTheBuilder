using System;
using System.Dynamic;

using BobTheBuilder.ArgumentStore;

namespace BobTheBuilder.Syntax
{
    public class MethodSyntaxParser : IParser
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
            var memberName = binder.Name;
            if (!memberName.StartsWith("With") || memberName == "With")
            {
                return false;
            }

            argumentStore.SetMemberNameAndValue(memberName.Replace("With", ""), args[0]);
            return true;
        }
    }
}