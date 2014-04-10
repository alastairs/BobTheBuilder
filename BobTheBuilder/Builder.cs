﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicObject, IDynamicBuilder<T> where T: class
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

        private void ParseMembersFromMethodName(InvokeMemberBinder binder, object[] args)
        {
            var memberName = binder.Name.Replace("With", "");
            _members[memberName] = args[0];
        }

        private void ParseMembersFromNamedArguments(CallInfo callInfo, object[] args)
        {
            var memberName = callInfo.ArgumentNames.First();
            memberName = memberName.First().ToString().ToUpper() + memberName.Substring(1);
            _members[memberName] = args[0];
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
            return new NamedArgumentsDynamicBuilder<T>(new DynamicBuilder<T>());
        }
    }
}