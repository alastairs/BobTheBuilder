namespace BobTheBuilder.Extensions
{
    public static class NameExtensions
    {
        public static string ToPascalCase(this string camelCasedName)
        {
            return char.ToUpper(camelCasedName[0]) + camelCasedName.Substring(1);
        }
    }
}
