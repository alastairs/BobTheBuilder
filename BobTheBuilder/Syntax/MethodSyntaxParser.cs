using System.Dynamic;
using BobTheBuilder.ArgumentStore;
using JetBrains.Annotations;

namespace BobTheBuilder.Syntax
{
    internal class MethodSyntaxParser : IParser
    {
        private readonly IArgumentStore argumentStore;

        public MethodSyntaxParser([NotNull]IArgumentStore argumentStore)
        {
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