using BobTheBuilder.Syntax;
using System.Dynamic;
#if NETCOREAPP3_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#else
using JetBrains.Annotations;
#endif
using Activator = BobTheBuilder.Activation.Activator;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject, IBuilder where T : class
    {
        private readonly Activator activator;
        private readonly IParser parser;

        internal DynamicBuilder([NotNull]IParser parser, [NotNull]Activator activator)
        {
            this.parser = parser;
            this.activator = activator;
        }

        public T Build()
        {
            return activator.Activate<T>();
        }

        object IBuilder.Build()
        {
            return Build();
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

        public BuilderRecord UsingRecords()
        {
            return new BuilderRecord(this);
        }

        public record BuilderRecord : IBuilder
        {
            private readonly DynamicBuilder<T> _builder;

            internal BuilderRecord(DynamicBuilder<T> builder) => _builder = builder;

            protected BuilderRecord(BuilderRecord original)
            {

            }

            object IBuilder.Build()
            {
                return Build();
            }

            public T Build()
            {
                return _builder.Build();
            }
        }
    }
}