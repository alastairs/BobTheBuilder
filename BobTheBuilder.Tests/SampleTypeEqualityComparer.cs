using System;
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

    internal class ExtendedSampleTypeEqualityComparer : IEqualityComparer<ExtendedSampleType>
    {
        private readonly IEqualityComparer<SampleType> wrappedComparer;

        internal ExtendedSampleTypeEqualityComparer(SampleTypeEqualityComparer wrappedComparer)
        {
            if (wrappedComparer == null)
            {
                throw new ArgumentNullException("wrappedComparer");
            }

            this.wrappedComparer = wrappedComparer;
        }

        public bool Equals(ExtendedSampleType x, ExtendedSampleType y)
        {
            if (!wrappedComparer.Equals(x, y))
            {
                return false;
            }

            var newStringsAreEqual = x.NewStringProperty == y.NewStringProperty;
            return newStringsAreEqual;
        }

        public int GetHashCode(ExtendedSampleType obj)
        {
            return 1;
        }
    }
}
