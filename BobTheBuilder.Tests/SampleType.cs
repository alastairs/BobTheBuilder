using System;

namespace BobTheBuilder.Tests
{
    internal class SampleType
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public Exception ComplexProperty { get; set; }
    }

    internal class ExtendedSampleType : SampleType
    {
        public string NewStringProperty { get; set; }
    }
}
