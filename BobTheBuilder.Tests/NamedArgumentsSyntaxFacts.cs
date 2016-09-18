using System;
using BobTheBuilder.Syntax;
using Ploeh.AutoFixture.Xunit;

using Xunit;
using Xunit.Extensions;

namespace BobTheBuilder.Tests
{
    public class NamedArgumentsSyntaxFacts
    {
        [Theory, AutoData]
        public void SetStringStateByNamedUsingNamedArgument(string expected)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.With(stringProperty: expected);
            SampleType result = sut.Build();

            Assert.Equal(expected, result.StringProperty);
        }

        [Theory, AutoData]
        public void SetStringAndIntStateByNameUsingNamedArgument(string expectedString, int expectedInt)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.With(stringProperty: expectedString, intProperty: expectedInt);
            SampleType result = sut.Build();

            var expected = new SampleType { StringProperty = expectedString, IntProperty = expectedInt };
            Assert.Equal(expected, result, new SampleTypeEqualityComparer());
        }

        [Theory, AutoData]
        public void ThrowAnArgumentExceptionWhenNoNameIsSuppliedForTheNamedArgumentsSyntax(string expected)
        {
            var sut = A.BuilderFor<SampleType>();

            var exception = Assert.Throws<ParseException>(() => sut.With(expected));
            Assert.Contains("No names were specified for the values provided. When using the named arguments (With()) syntax, you should specify the items to be set as argument names, such as With(customerId: customerId).", exception.Message);
        }

        [Theory, AutoData]
        public void ThrowAnArgumentExceptionWhenSomeArgumentsAreMissingNames(string expectedString, int expectedInt)
        {
            var sut = A.BuilderFor<SampleType>();

            var exception = Assert.Throws<ParseException>(() => sut.With(expectedString, intProperty: expectedInt));
            Assert.Contains("One or more arguments are missing a name. Names should be specified with C# named argument syntax, e.g. With(customerId: customerId).", exception.Message);
        }
    }
}
