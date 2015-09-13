using BobTheBuilder.Syntax;
using System;
using System.Dynamic;
using Activator = BobTheBuilder.Activation.Activator;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject where T : class
    {
        private readonly Activator activator;
        private readonly IParser parser;
        
        internal DynamicBuilder(IParser parser, Activator activator)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }

            if (activator == null)
            {
                throw new ArgumentNullException("activator");
            }

            this.parser = parser;
            this.activator = activator;
        }

        public T Build()
        {
            return activator.Activate<T>();
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