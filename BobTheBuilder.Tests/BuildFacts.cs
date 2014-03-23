using Xunit;

namespace BobTheBuilder.Tests
{
    public class BuildFacts
    {
        [Fact]
        public void CreateAnInstanceOfTheRequestedType()
        {
            var sut = A.BuilderFor<SampleType>();

            var result = sut.Build();

            Assert.IsAssignableFrom<SampleType>(result);
        }

        private class SampleType { }
    }
}
