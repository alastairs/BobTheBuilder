using System;
using System.Dynamic;

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
            return wrappedBuilder.TryInvokeMember(binder, args, out result);
        }

        public T Build()
        {
            return wrappedBuilder.Build();
        }
    }
}