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
    }
}