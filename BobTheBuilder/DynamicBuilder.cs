using BobTheBuilder.ArgumentStore;
using BobTheBuilder.Syntax;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject, IDynamicBuilder<T> where T : class
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
            EvaluateMissingMembers(destinationType);

            var constructorParameters = destinationType.GetConstructors().Single().GetParameters().ToLookup(p => p.Name);
            var constructorArguments = argumentStore.GetConstructorArguments(constructorParameters);

            var properties = destinationType.GetProperties().ToLookup(p => p.Name);
            var propertyValues = argumentStore.GetPropertyValues(properties);

            var instance = CreateInstanceOfType(constructorArguments);
            PopulatePublicSettableProperties(instance, propertyValues);
            return instance;
        }

        private void EvaluateMissingMembers(Type destinationType)
        {
            var missingArguments = GetMissingMembers(destinationType);
            if (missingArguments.Any())
            {
                var missingMember = missingArguments.First();
                throw new MissingMemberException(string.Format(@"The property ""{0}"" does not exist on ""{1}""",
                    missingMember.Name, destinationType.Name));
            }
        }

        private IEnumerable<MemberNameAndValue> GetMissingMembers(Type destinationType)
        {
            var properties = destinationType.GetProperties().ToLookup(p => p.Name);
            return argumentStore.GetMissingArguments(properties);
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