using System;
using System.Collections.Generic;

namespace BobTheBuilder.ArgumentStore.Queries
{
    internal interface IArgumentStoreQuery
    {
        IEnumerable<MemberNameAndValue> Execute(Type destinationType);
    }
}