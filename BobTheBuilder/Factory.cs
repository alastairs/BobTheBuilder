using BobTheBuilder.ArgumentStore;
using BobTheBuilder.Syntax;

namespace BobTheBuilder
{
    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            var argumentStore = new InMemoryArgumentStore();
            return new NamedArgumentsSyntaxParser<T>(new MethodSyntaxParser<T>(argumentStore), argumentStore);
        }
    }
}