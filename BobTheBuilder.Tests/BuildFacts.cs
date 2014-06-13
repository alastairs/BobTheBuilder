using System;
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
        public void FailsIfAPropertyCannotBeFound()
        {
            var sut = A.BuilderFor<SampleType>()
                .WithStringPurrrrrrroperty("Not a thing");

            var exception = Assert.Throws<MissingMemberException>(() => sut.Build());

            Assert.Equal(@"The property ""StringPurrrrrrroperty"" does not exist on ""SampleType""", exception.Message);
        }
    }
}
