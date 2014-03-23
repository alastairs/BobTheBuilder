using System;
using System.Dynamic;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject where T: class
    {
        private object _property;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            _property = args[0];
            result = this;
            return true;
        }

        public T Build()
        {
            var instance = Activator.CreateInstance<T>();
            instance.GetType().GetProperty("StringProperty").SetValue(instance, _property);
            return instance;
        }

        public static implicit operator T(DynamicBuilder<T> builder)
        {
            return builder.Build();
        }
    }

    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            return new DynamicBuilder<T>();
        }
    }
}