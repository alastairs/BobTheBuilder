﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal class PropertyValuesQuery : IArgumentStoreQuery
    {
        private readonly IArgumentStore argumentStore;

        public PropertyValuesQuery([NotNull]IArgumentStore argumentStore)
        {
            this.argumentStore = argumentStore;
        }

        public IEnumerable<MemberNameAndValue> Execute(Type destinationType)
        {
            var properties = destinationType.GetProperties().Select(p => p.Name);
            return argumentStore.GetAllStoredMembers().Where(member => properties.Contains(member.Name));
        }
    }
}
