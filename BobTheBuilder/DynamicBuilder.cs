using System;
using System.Dynamic;

using BobTheBuilder.ArgumentStore;

namespace BobTheBuilder
{
    public abstract class DynamicBuilder<T> : DynamicObject, IDynamicBuilder<T> where T : class
    {
        protected internal readonly IArgumentStore argumentStore;
         
        protected DynamicBuilder(IArgumentStore argumentStore)
        {
            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.argumentStore = argumentStore;
        }

        public abstract bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result);

        public T Build()
        {
            var instance = CreateInstanceOfType();
            PopulatePublicSettableProperties(instance);
            return instance;
        }

        private static T CreateInstanceOfType()
        {
            var instance = Activator.CreateInstance<T>();
            return instance;
        }

        private void PopulatePublicSettableProperties(T instance)
        {
            var knownMembers = argumentStore.GetAllStoredMembers();

            foreach (var member in knownMembers)
            {
                var property = typeof (T).GetProperty(member.Name);
                property.SetValue(instance, member.Value);
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return InvokeBuilderMethod(binder, args, out result);
        }

        public static implicit operator T(DynamicBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}