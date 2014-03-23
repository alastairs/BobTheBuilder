using System.Collections.Generic;

namespace BobTheBuilder.Tests
{
    internal class SampleTypeEqualityComparer : IEqualityComparer<SampleType>
    {
        public bool Equals(SampleType x, SampleType y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            var stringsAreEqual = x.StringProperty == y.StringProperty;
            return stringsAreEqual;
        }

        public int GetHashCode(SampleType obj)
        {
            return 1;
        }
    }
}