using System;
using System.Collections.Generic;
using System.Linq;
using BobTheBuilder.ArgumentStore;
using BobTheBuilder.ArgumentStore.Queries;
using JetBrains.Annotations;

namespace BobTheBuilder.Activation
{
    internal class BuilderArgumentEvaluator : IArgumentStoreQuery
    {
        private readonly IArgumentStoreQuery argumentStoryQuery;

        internal BuilderArgumentEvaluator([NotNull]IArgumentStoreQuery argumentStoryQuery)
        {
            this.argumentStoryQuery = argumentStoryQuery;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            return argumentStoryQuery.Execute(destinationType).Select(arg => new MemberNameAndValue(arg.Name, EvaluateBuilder(arg.Value)));
        }

        private static object EvaluateBuilder(object arg)
        {
            var builder = arg as DynamicBuilder;
            return builder != null ? builder.Build() : arg;
        }
    }
}