using BobTheBuilder.Syntax;
using AutoFixture.Xunit2;
using Xunit;

namespace BobTheBuilder.Tests
{
    public class NamedArgumentsSyntaxFacts
    {
        [Theory, AutoData]
        public void SetStringStateByNamedUsingNamedArgument(string expected)
        {
            var sut = A.BuilderFor<Person>();

            sut.With(name: expected);
            Person result = sut.Build();

            Assert.Equal(expected, result.Name);
        }

        [Theory, AutoData]
        public void SetStringAndIntStateByNameUsingNamedArgument(string expectedString, int expectedInt)
        {
            var sut = A.BuilderFor<Person>();

            sut.With(name: expectedString, ageInYears: expectedInt);
            Person result = sut.Build();

            var expected = new Person { Name = expectedString, AgeInYears = expectedInt };
            Assert.Equal(expected, result, new PersonEqualityComparer());
        }

        [Theory, AutoData]
        public void ThrowAnArgumentExceptionWhenNoNameIsSuppliedForTheNamedArgumentsSyntax(string expected)
        {
            var sut = A.BuilderFor<Person>();

            var exception = Assert.Throws<ParseException>(() => sut.With(expected));
            Assert.Contains("No names were specified for the values provided. When using the named arguments (With()) syntax, you should specify the items to be set as argument names, such as With(customerId: customerId).", exception.Message);
        }

        [Theory, AutoData]
        public void ThrowAnArgumentExceptionWhenSomeArgumentsAreMissingNames(string expectedString, int expectedInt)
        {
            var sut = A.BuilderFor<Person>();

            var exception = Assert.Throws<ParseException>(() => sut.With(expectedString, intProperty: expectedInt));
            Assert.Contains("One or more arguments are missing a name. Names should be specified with C# named argument syntax, e.g. With(customerId: customerId).", exception.Message);
        }
    }
}
