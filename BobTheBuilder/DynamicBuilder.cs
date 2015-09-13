using BobTheBuilder.Activation;
using BobTheBuilder.ArgumentStore;
using BobTheBuilder.ArgumentStore.Queries;
using BobTheBuilder.Syntax;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject where T : class
    {
        private readonly IParser parser;
        private readonly IArgumentStore argumentStore;

        public DynamicBuilder(IParser parser, IArgumentStore argumentStore)
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
            var destinationType = typeof(T);
            new ReportingMissingArgumentsQuery(new MissingArgumentsQuery(argumentStore)).Execute(destinationType);

            var propertyValues = new PropertyValuesQuery(argumentStore).Execute(destinationType);

            var instance = new InstanceCreator(new ConstructorArgumentsQuery(argumentStore)).CreateInstanceOf<T>();
            PopulatePublicSettableProperties(instance, propertyValues);
            return instance;
        }

        private static void PopulatePublicSettableProperties(T instance, IEnumerable<MemberNameAndValue> propertyValues)
        {
            new PropertySetter().PopulatePropertiesOn(instance, propertyValues);
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