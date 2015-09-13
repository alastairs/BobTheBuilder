using BobTheBuilder.ArgumentStore;
using BobTheBuilder.ArgumentStore.Queries;
using BobTheBuilder.Syntax;
using System;
using System.Dynamic;
using Activator=BobTheBuilder.Activation.Activator;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject where T : class
    {
        private readonly IParser parser;
        private readonly IArgumentStore argumentStore;

        internal DynamicBuilder(IParser parser, IArgumentStore argumentStore)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }

            if (argumentStore == null)
            {
                throw new ArgumentNullException("argumentStore");
            }

            this.parser = parser;
            this.argumentStore = argumentStore;
        }

        public T Build()
        {
            return new Activator(
                new MissingArgumentsQuery(argumentStore), 
                new ConstructorArgumentsQuery(argumentStore), 
                new PropertyValuesQuery(argumentStore)).Activate<T>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = this;
            return parser.Parse(binder, args);
        }

        public static implicit operator T(DynamicBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}