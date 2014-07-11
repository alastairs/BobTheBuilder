using System;
using System.Linq;

namespace BobTheBuilder.Extensions
{
    public static class NameExtensions
    {
        public static string ToPascalCase(this string camelCasedName)
        {
            return Char.ToUpper(camelCasedName.First()) + camelCasedName.Substring(1);
        }
    }
}
