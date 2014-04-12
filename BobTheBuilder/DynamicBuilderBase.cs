using System.Collections.Generic;
using System.Dynamic;

namespace BobTheBuilder
{
    public abstract class DynamicBuilderBase<T> : DynamicObject, IDynamicBuilder<T>, IArgumentStore where T : class
    {
        protected readonly IDictionary<string, object> _members = new Dictionary<string, object>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return InvokeBuilderMethod(binder, args, out result);
        }

        public abstract bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result);

        public abstract T Build();

        public void SetMemberNameAndValue(string name, object value)
        {
            _members[name] = value;
        }

        public static implicit operator T(DynamicBuilderBase<T> builder)
        {
            return builder.Build();
        }
    }
}