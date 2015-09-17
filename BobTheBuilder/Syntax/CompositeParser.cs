using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using JetBrains.Annotations;

namespace BobTheBuilder.Syntax
{
    internal class CompositeParser : IParser
    {
        private readonly IEnumerable<IParser> wrappedParsers;

        public CompositeParser([NotNull]params IParser[] wrappedParsers)
        {
            this.wrappedParsers = wrappedParsers;
        }

        public bool Parse(InvokeMemberBinder binder, object[] callArguments)
        {
            return wrappedParsers.Any(p => p.Parse(binder, callArguments));
        }
    }
}
