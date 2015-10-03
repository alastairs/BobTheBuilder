using System;
using System.Dynamic;
using System.Linq;

using BobTheBuilder.ArgumentStore;
using JetBrains.Annotations;

namespace BobTheBuilder.Syntax
{
    internal class NamedArgumentsSyntaxParser : IParser
    {
        private readonly IArgumentStore argumentStore;

        internal NamedArgumentsSyntaxParser([NotNull]IArgumentStore argumentStore)
        {
            this.argumentStore = argumentStore;
        }

        public bool Parse(InvokeMemberBinder binder, object[] args)
        {
            if (binder.Name == "With")
            {
                ParseNamedArgumentValues(binder.CallInfo, args);
                return true;
            }

            return false;
        }

        private void ParseNamedArgumentValues(CallInfo callInfo, object[] args)
        {
            if (!callInfo.ArgumentNames.Any())
            {
                throw new ArgumentException("No names were specified for the values provided. When using the named arguments (With()) syntax, you should specify the items to be set as argument names, such as With(customerId: customerId).");
            }

            if (callInfo.ArgumentNames.Count() != args.Length)
            {
                throw new ArgumentException("One or more arguments are missing a name. Names should be specified with C# named argument syntax, e.g. With(customerId: customerId).");
            }

            var argumentIndex = 0;
            foreach (var argumentName in callInfo.ArgumentNames.Select(ToCamelCase))
            {
                argumentStore.Set(new MemberNameAndValue(argumentName, args[argumentIndex++]));
            }
        }

        private static string ToCamelCase(string name)
        {
            return name.First().ToString().ToUpper() + name.Substring(1);
        }
    }
}
