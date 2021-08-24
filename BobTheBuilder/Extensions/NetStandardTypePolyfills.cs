using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BobTheBuilder.Extensions
{
    internal static class NetStandardTypePolyfills
    {
        internal static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var declaredConstructors = typeInfo.DeclaredConstructors;

            return declaredConstructors;
        }

        internal static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var declaredProperties = typeInfo.DeclaredProperties;

            var baseTypeProperties = typeInfo.BaseType == null
                ? Enumerable.Empty<PropertyInfo>()
                : typeInfo.BaseType.GetProperties();

            return declaredProperties.Concat(baseTypeProperties);
        }

        internal static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetProperties().Single(p => p.Name == name);
        }
    }
}
