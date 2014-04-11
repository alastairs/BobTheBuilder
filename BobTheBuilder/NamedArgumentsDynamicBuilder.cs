using System;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder
{
    public class NamedArgumentsDynamicBuilder<T> : DynamicObject, IDynamicBuilder<T> where T : class
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
            if (binder.Name == "With")
            {
                ParseNamedArgumentValues(binder.CallInfo, args);
            }

            return wrappedBuilder.TryInvokeMember(binder, args, out result);
        }

        private void ParseNamedArgumentValues(CallInfo callInfo, object[] args)
        {
            var argumentName = ToCamelCase(callInfo.ArgumentNames.First());
            argumentStore.SetMemberNameAndValue(argumentName, args.First());
        }

        private static string ToCamelCase(string name)
        {
            return name.First().ToString().ToUpper() + name.Substring(1);
        }

        public T Build()
        {
            return wrappedBuilder.Build();
        }
    }
}