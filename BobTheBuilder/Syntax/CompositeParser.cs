using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using JetBrains.Annotations;

namespace BobTheBuilder.Syntax
{
    internal class CompositeParser : IParser
    {
        private readonly IEnumerable<ErrorRecorder> wrappedParsers;
        private readonly ICollection<string> parseErrors = new List<string>();

        public CompositeParser([NotNull]params IParser[] wrappedParsers)
        {
            this.wrappedParsers = wrappedParsers.Select(p => new ErrorRecorder(p, parseErrors));
        }

        public bool Parse(InvokeMemberBinder binder, object[] callArguments)
        {
            var successfullyParsed = wrappedParsers.Any(p => p.Parse(binder, callArguments));
            if (!successfullyParsed && parseErrors.Any())
            {
                throw new ParseException(string.Join(Environment.NewLine, parseErrors));
            }

            return successfullyParsed;
        }

        private class ErrorRecorder : IParser
        {
            private readonly IParser wrappedParser;
            private readonly ICollection<string> parseErrors;

            internal ErrorRecorder([NotNull] IParser wrappedParser, [NotNull] ICollection<string> parseErrors)
            {
                this.wrappedParser = wrappedParser;
                this.parseErrors = parseErrors;
            }

            public bool Parse(InvokeMemberBinder binder, object[] callArguments)
            {
                try
                {
                    return wrappedParser.Parse(binder, callArguments);
                }
                catch (Exception ex)
                {
                    parseErrors.Add(ex.Message);
                    return false;
                }
            }
        }
    }
}
