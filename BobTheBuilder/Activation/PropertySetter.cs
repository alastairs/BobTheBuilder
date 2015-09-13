using BobTheBuilder.ArgumentStore;
using System.Collections.Generic;

namespace BobTheBuilder.Activation
{
    internal class PropertySetter
    {
        public void PopulatePropertiesOn<T>(T instance, IEnumerable<MemberNameAndValue> propertyValues) where T: class
        {
            foreach (var member in propertyValues)
            {
                var property = typeof(T).GetProperty(member.Name);
                property.SetValue(instance, member.Value);
            }
        }
    }
}
