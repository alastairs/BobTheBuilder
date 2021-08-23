using BobTheBuilder.Syntax;
using Ploeh.AutoFixture.Xunit2;

using Xunit;

namespace BobTheBuilder.Tests
{
    public class ObjectLiteralSyntaxFacts
    {
        [Theory, AutoData]
        public void SetStringStateByNameUsingObjectLiteral(string Name)
        {
            var sut = A.BuilderFor<Person>();

            sut.With(new { Name });
            Person result = sut.Build();

            Assert.Equal(Name, result.Name);
        }

        [Theory, AutoData]
        public void SetStringAndIntStateByNameUsingNamedArgument(string Name, int AgeInYears)
        {
            var sut = A.BuilderFor<Person>();

            sut.With(new { Name, AgeInYears });
            Person result = sut.Build();

            var expected = new Person { Name = Name, AgeInYears = AgeInYears };
            Assert.Equal(expected, result, new PersonEqualityComparer());
        }

        [Fact]
        public void ThrowAnArgumentExceptionWhenMoreThanOneArgumentIsSuppliedForTheObjectLiteralSyntax()
        {
            var sut = A.BuilderFor<Person>();

            var exception = Assert.Throws<ParseException>(() => sut.With(1, 2));
            Assert.Contains("Expected a single object of an anonymous type, but was passed 2 arguments. Try replacing these arguments with an anonymous type composing the arguments.", exception.Message);
        }

        [Fact]
        public void ThrowAnArgumentExceptionWhenPassedSomethingOtherThanAnObjectLiteral()
        {
            var sut = A.BuilderFor<Person>();

            var exception = Assert.Throws<ParseException>(() => sut.With(5));
            Assert.Contains("Expected a single object of an anonymous type, but was passed an object of type System.Int32", exception.Message);
        }
    }
}
