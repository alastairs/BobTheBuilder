using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using BobTheBuilder.ArgumentStore;
using BobTheBuilder.Syntax;

namespace BobTheBuilder
{
    public enum ArgumentLocation
    {
        Property,
        Missing
    }

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
            var properties = typeof (T).GetProperties().ToLookup(p => p.Name);
            var missingArguments = argumentStore.GetMissingArguments(properties);

            if (missingArguments.Any())
            {
                var missingMember = missingArguments.First();
                throw new MissingMemberException(string.Format(@"The property ""{0}"" does not exist on ""{1}""",
                        missingMember.Name, typeof (T).Name));
            }

            var instance = CreateInstanceOfType();
            PopulatePublicSettableProperties(instance);
            return instance;
        }

        private static T CreateInstanceOfType()
        {
            var instance = Activator.CreateInstance<T>();
            return instance;
        }

        private void PopulatePublicSettableProperties(T instance)
        {
            var knownMembers = argumentStore.GetAllStoredMembers();

            foreach (var member in knownMembers)
            {
                var property = typeof (T).GetProperty(member.Name);
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