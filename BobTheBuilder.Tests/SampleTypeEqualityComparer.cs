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
            var intsAreEqual = x.IntProperty == y.IntProperty;
            var complexesAreEqual = x.ComplexProperty == y.ComplexProperty;

            return stringsAreEqual && intsAreEqual && complexesAreEqual;
        }

        public int GetHashCode(SampleType obj)
        {
            return 1;
        }
    }
}