using System.Dynamic;

namespace BobTheBuilder.Syntax
{
    internal interface IParser
    {
        bool Parse(InvokeMemberBinder binder, object[] callArguments);
    }
}