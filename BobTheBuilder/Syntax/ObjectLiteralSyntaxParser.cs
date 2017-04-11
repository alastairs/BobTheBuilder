using System;
using System.Dynamic;
using System.Linq;

using BobTheBuilder.ArgumentStore;
using BobTheBuilder.Extensions;

using JetBrains.Annotations;

namespace BobTheBuilder.Syntax
{
    internal class ObjectLiteralSyntaxParser : IParser
    {
        private readonly IArgumentStore argumentStore;

        public ObjectLiteralSyntaxParser([NotNull]IArgumentStore argumentStore)
        {
            this.argumentStore = argumentStore;
        }

        public bool Parse(InvokeMemberBinder binder, object[] callArguments)
        {
            if (binder.Name != "With")
            {
                return false;
            }

            if (callArguments.Length > 1)
            {
                throw new ArgumentException(
                    $"Expected a single object of an anonymous type, but was passed {callArguments.Length} arguments. " +
                    "Try replacing these arguments with an anonymous type composing the arguments.");
            }

            var argType = callArguments.Single().GetType();
            if (!IsAnonymous(argType))
            {
                throw new ArgumentException($"Expected a single object of an anonymous type, but was passed an object of type {argType.FullName}.");
            }

            ParseObjectLiteral(callArguments.Single());

            return true;
        }

        private void ParseObjectLiteral(object arg)
        {
            var properties = arg.GetType().GetProperties();

            foreach (var property in properties)
            {
                argumentStore.Set(new MemberNameAndValue(property.Name, property.GetValue(arg)));
            }
        }

        private static bool IsAnonymous(Type type)
        {
            // This is a quick and dirty check.
            // See http://stackoverflow.com/a/15273117 for more details

            return type.Namespace == null;
        }
    }
}
