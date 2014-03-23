using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

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

        [Theory, AutoData]
        public void SetStateByName(string expected)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.WithStringProperty(expected);
            var result = sut.Build();

            Assert.Equal(expected, result.StringProperty);
        }
    }
}
