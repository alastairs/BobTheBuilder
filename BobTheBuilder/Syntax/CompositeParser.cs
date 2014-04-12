using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder.Syntax
{
    class CompositeParser : IParser
    {
        private readonly IEnumerable<IParser> wrappedParsers;

        public CompositeParser(params IParser[] wrappedParsers)
        {
            if (wrappedParsers == null)
            {
                throw new ArgumentNullException("wrappedParsers");
            }

            this.wrappedParsers = wrappedParsers;
        }

        public bool Parse(InvokeMemberBinder binder, object[] callArguments)
        {
            return wrappedParsers.Any(p => p.Parse(binder, callArguments));
        }
    }
}
