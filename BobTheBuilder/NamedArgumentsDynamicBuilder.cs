using System;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder
{
    public class NamedArgumentsDynamicBuilder<T> : DynamicBuilderBase<T> where T : class
    {
        private readonly IDynamicBuilder<T> wrappedBuilder;
        private readonly IArgumentStore argumentStore;

        internal NamedArgumentsDynamicBuilder(IDynamicBuilder<T> wrappedBuilder, IArgumentStore argumentStore)
        {
            if (wrappedBuilder == null)
            {
                throw new ArgumentNullException("wrappedBuilder");
            }

            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.wrappedBuilder = wrappedBuilder;
            this.argumentStore = argumentStore;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return InvokeBuilderMethod(binder, args, out result);
        }

        public override bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (binder.Name == "With")
            {
                ParseNamedArgumentValues(binder.CallInfo, args);
            }

            return wrappedBuilder.InvokeBuilderMethod(binder, args, out result);
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
                argumentStore.SetMemberNameAndValue(argumentName, args[argumentIndex++]);
            }
        }

        private static string ToCamelCase(string name)
        {
            return name.First().ToString().ToUpper() + name.Substring(1);
        }

        public override T Build()
        {
            return wrappedBuilder.Build();
        }
    }
}