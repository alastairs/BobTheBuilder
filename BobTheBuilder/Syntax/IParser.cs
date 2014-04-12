using System.Dynamic;

namespace BobTheBuilder.Syntax
{
    public interface IParser
    {
        bool Parse(InvokeMemberBinder binder, object[] callArguments);
    }
}