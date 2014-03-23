using Xunit;

namespace BobTheBuilder.Tests
{
    public class BuildFacts
    {
        [Fact]
        public void CreateADynamicInstanceOfTheRequestedType()
        {
            var sut = A.BuilderFor<SampleType>();

            var result = sut.Build();

            Assert.IsAssignableFrom<dynamic>(result);
            Assert.IsAssignableFrom<SampleType>(result);
        }

        [Fact]
        public void SetStateByName()
        {
            var sut = A.BuilderFor<SampleType>();
            const string expectedStringValue = "expected value";

            sut.WithStringProperty(expectedStringValue);
            var result = sut.Build();

            Assert.Equal(expectedStringValue, result.StringProperty);
        }

        private class SampleType
        {
            public string StringProperty { get; set; }
        }
    }
}
