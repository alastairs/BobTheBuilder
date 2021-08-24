using System.Dynamic;
using BobTheBuilder.ArgumentStore;
#if NETCOREAPP3_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#else
using JetBrains.Annotations;
#endif

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

            argumentStore.Set(new MemberNameAndValue(memberName.Replace("With", ""), args[0]));
            return true;
        }
    }
}
