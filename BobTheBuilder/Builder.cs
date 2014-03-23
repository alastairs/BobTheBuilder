using System;
using System.Dynamic;

namespace BobTheBuilder
{
    public class DynamicBuilder : DynamicObject
    {
        private readonly Type _destinationType;

        internal DynamicBuilder(Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            _destinationType = destinationType;
        }

        public object Build()
        {
            return Activator.CreateInstance(_destinationType);
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