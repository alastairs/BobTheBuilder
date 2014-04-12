using System.Dynamic;

namespace BobTheBuilder
{
    public interface IDynamicBuilder<T> where T : class
    {
        bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result);

        T Build();
    }
}