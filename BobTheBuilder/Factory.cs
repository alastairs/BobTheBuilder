namespace BobTheBuilder
{
    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            var dynamicBuilder = new DynamicBuilder<T>();
            return new NamedArgumentsDynamicBuilder<T>(dynamicBuilder, dynamicBuilder);
        }
    }
}