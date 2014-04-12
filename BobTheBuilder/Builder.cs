using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject, IDynamicBuilder<T>, IArgumentStore where T: class
    {
        private readonly IDictionary<string, object> _members = new Dictionary<string, object>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (binder.Name == "With")
            {
                ParseMembersFromNamedArguments(binder.CallInfo, args);
            }
            else
            {
                ParseMembersFromMethodName(binder, args);
            }

            result = this;
            return true;
        }

        public void SetMemberNameAndValue(string name, object value)
        {
            _members[name] = value;
        }

        private void ParseMembersFromMethodName(InvokeMemberBinder binder, object[] args)
        {
            var memberName = binder.Name.Replace("With", "");
            SetMemberNameAndValue(memberName, args[0]);
        }

        private void ParseMembersFromNamedArguments(CallInfo callInfo, object[] args)
        {
            if (!callInfo.ArgumentNames.Any())
            {
                throw new ArgumentException("No names were specified for the values provided. When using the named arguments (With()) syntax, you should specify the items to be set as argument names, such as With(customerId: customerId).");
            }

            if (callInfo.ArgumentNames.Count() != args.Length)
            {
                throw new ArgumentException("One or more arguments are missing a name. Names should be specified with C# named argument syntax, e.g. With(customerId: customerId).");
            }

            var memberName = callInfo.ArgumentNames.First();
            memberName = memberName.First().ToString().ToUpper() + memberName.Substring(1);
            SetMemberNameAndValue(memberName, args[0]);
        }

        public T Build()
        {
            var instance = CreateInstanceOfType();
            PopulatePublicSettableProperties(instance);
            return instance;
        }

        private void PopulatePublicSettableProperties(T instance)
        {
            foreach (var memberInfo in _members.Select(m => new {memberName = m.Key, desiredValue = m.Value}))
            {
                var property = typeof (T).GetProperty(memberInfo.memberName);
                property.SetValue(instance, memberInfo.desiredValue);
            }
        }

        private static T CreateInstanceOfType()
        {
            var instance = Activator.CreateInstance<T>();
            return instance;
        }

        public static implicit operator T(DynamicBuilder<T> builder)
        {
            return builder.Build();
        }
    }

    public class A
    {
        public static dynamic BuilderFor<T>() where T: class
        {
            var dynamicBuilder = new DynamicBuilder<T>();
            return new NamedArgumentsDynamicBuilder<T>(dynamicBuilder, dynamicBuilder);
        }
    }
}