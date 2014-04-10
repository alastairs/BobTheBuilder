using System;
using System.Dynamic;

namespace BobTheBuilder
{
    public class NamedArgumentsDynamicBuilder<T> : DynamicObject, IDynamicBuilder<T> where T : class
    {
        private readonly IDynamicBuilder<T> wrappedBuilder;

        public NamedArgumentsDynamicBuilder(IDynamicBuilder<T> wrappedBuilder)
        {
            if (wrappedBuilder == null)
            {
                throw new ArgumentNullException("wrappedBuilder");
            }

            this.wrappedBuilder = wrappedBuilder;
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