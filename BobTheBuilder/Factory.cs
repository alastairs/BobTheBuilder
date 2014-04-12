namespace BobTheBuilder
{
    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            var argumentStore = new InMemoryArgumentStore();
            return new NamedArgumentsDynamicBuilder<T>(new DynamicBuilder<T>(argumentStore), argumentStore);
        }
    }
}