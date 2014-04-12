using System;
using System.Dynamic;
using System.Linq;

namespace BobTheBuilder
{
    public class DynamicBuilder<T> : DynamicBuilderBase<T> where T: class
    {
        public override bool InvokeBuilderMethod(InvokeMemberBinder binder, object[] args, out object result)
        {
            ParseMembersFromMethodName(binder, args);

            result = this;
            return true;
        }

        private void ParseMembersFromMethodName(InvokeMemberBinder binder, object[] args)
        {
            var memberName = binder.Name.Replace("With", "");
            SetMemberNameAndValue(memberName, args[0]);
        }

        public override T Build()
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
    }
}