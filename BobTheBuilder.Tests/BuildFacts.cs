using System;
using Xunit;

namespace BobTheBuilder.Tests
{
    public class BuildFacts
    {
        [Fact]
        public void CreateADynamicInstanceOfTheRequestedType()
        {
            var sut = A.BuilderFor<Person>();

            var result = sut.Build();

            Assert.IsAssignableFrom<dynamic>(result);
            Assert.IsAssignableFrom<Person>(result);
        }

        [Fact]
        public void FailsIfAPropertyCannotBeFound()
        {
            var sut = A.BuilderFor<Person>()
                .WithStringPurrrrrrroperty("Not a thing");

            var exception = Assert.Throws<MissingMemberException>(() => sut.Build());

            Assert.Equal(@"The property ""StringPurrrrrrroperty"" does not exist on ""Person""", exception.Message);
        }
    }
}
