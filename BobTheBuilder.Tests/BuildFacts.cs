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

        private class SampleType { }
    }
}
