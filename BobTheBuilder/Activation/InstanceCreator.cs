using BobTheBuilder.ArgumentStore;
using System.Collections.Generic;
using System.Linq;

namespace BobTheBuilder.Activation
{
    internal class InstanceCreator
    {
        public T CreateInstanceOf<T>(IEnumerable<MemberNameAndValue> constructorArguments) where T: class
        {
            var constructor = typeof(T).GetConstructors().Single();
            return (T)constructor.Invoke(constructorArguments.Select(arg => arg.Value).ToArray());
        }
    }
}
