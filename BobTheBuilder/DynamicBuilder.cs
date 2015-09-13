using BobTheBuilder.ArgumentStore;
using BobTheBuilder.ArgumentStore.Queries;
using BobTheBuilder.Syntax;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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

            var constructorArguments = new ConstructorArgumentsQuery(argumentStore).Execute(destinationType);

            var propertyValues = new PropertyValuesQuery(argumentStore).Execute(destinationType);

            var instance = CreateInstanceOfType(constructorArguments);
            PopulatePublicSettableProperties(instance, propertyValues);
            return instance;
        }

        private static T CreateInstanceOfType(IEnumerable<MemberNameAndValue> constructorArguments)
        {
            var constructor = typeof(T).GetConstructors().Single();
            return (T)constructor.Invoke(constructorArguments.Select(arg => arg.Value).ToArray());
        }

        private void PopulatePublicSettableProperties(T instance, IEnumerable<MemberNameAndValue> propertyValues)
        {
            foreach (var member in propertyValues)
            {
                var property = typeof(T).GetProperty(member.Name);
                property.SetValue(instance, member.Value);
            }
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