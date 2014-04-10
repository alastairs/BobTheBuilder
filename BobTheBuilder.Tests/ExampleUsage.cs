using System;

using Xunit;

namespace BobTheBuilder.Tests
{
    public class ExampleUsage
    {
        [Fact]
        public void Usage()
        {
            const string stringProperty = "expected value";
            var expected = new SampleType
            {
                StringProperty = stringProperty
            };

            var built = A.BuilderFor<SampleType>()
                .WithStringProperty(stringProperty)
                .Build();

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithImplicitCast()
        {
            const string stringProperty = "expected value";
            var expected = new SampleType
            {
                StringProperty = stringProperty
            };

            SampleType built = A.BuilderFor<SampleType>()
                .WithStringProperty(stringProperty);

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithComplexType()
        {
            var complexProperty = new Exception();
            var expected = new SampleType
            {
                ComplexProperty = complexProperty
            };

            SampleType built = A.BuilderFor<SampleType>()
                .WithComplexProperty(complexProperty);

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithNamedArgument()
        {
            const string expectedStringValue = "expected value";
            const int expectedIntValue = 1;
            var expected = new SampleType
            {
                StringProperty = expectedStringValue,
                IntProperty = expectedIntValue
            };

            SampleType built = A.BuilderFor<SampleType>().With(stringProperty: expectedStringValue, 
                                                               intProperty: expectedIntValue);

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }
    }
}