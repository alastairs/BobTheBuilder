using System;
using System.Dynamic;

namespace BobTheBuilder
{
    public abstract class DynamicBuilderBase<T> : DynamicObject, IDynamicBuilder<T> where T : class
    {
        protected internal readonly IArgumentStore argumentStore;

        protected DynamicBuilderBase(IArgumentStore argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return InvokeBuilderMethod(binder, args, out result);
        }

        public abstract bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result);

        public abstract T Build();

        public static implicit operator T(DynamicBuilderBase<T> builder)
        {
            return builder.Build();
        }
    }
}