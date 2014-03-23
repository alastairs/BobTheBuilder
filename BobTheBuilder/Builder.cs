using System;
using System.Dynamic;

namespace BobTheBuilder
{
    public class DynamicBuilder : DynamicObject
    {
        private readonly Type _destinationType;
        private object _property;

        internal DynamicBuilder(Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            _destinationType = destinationType;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            _property = args[0];
            result = this;
            return true;
        }

        public object Build()
        {
            object instance = Activator.CreateInstance(_destinationType);
            instance.GetType().GetProperty("StringProperty").SetValue(instance, _property);
            return instance;
        }
    }

    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            return new DynamicBuilder(typeof(T));
        }
    }
}