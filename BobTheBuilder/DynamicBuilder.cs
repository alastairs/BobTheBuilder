using BobTheBuilder.Syntax;
using System.Dynamic;
using JetBrains.Annotations;
using Activator = BobTheBuilder.Activation.Activator;

namespace BobTheBuilder
{
    public abstract class DynamicBuilder : DynamicObject
    {
        public object Build()
        {
            return InnerBuild();
        }

        protected abstract object InnerBuild();
    }

    public class DynamicBuilder<T> : DynamicBuilder where T : class
    {
        private readonly Activator activator;
        private readonly IParser parser;
        
        internal DynamicBuilder([NotNull]IParser parser, [NotNull]Activator activator)
        {
            this.parser = parser;
            this.activator = activator;
        }

        public new T Build()
        {
            return (T) base.Build();
        }

        protected override object InnerBuild()
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