using System;
using BobTheBuilder.Syntax;
using Ploeh.AutoFixture.Xunit;

using Xunit;
using Xunit.Extensions;

namespace BobTheBuilder.Tests
{
    public class ObjectLiteralSyntaxFacts
    {
        [Theory, AutoData]
        public void SetStringStateByNameUsingObjectLiteral(string expected)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.With(new { StringProperty = expected });
            SampleType result = sut.Build();

            Assert.Equal(expected, result.StringProperty);
        }

        [Theory, AutoData]
        public void SetStringAndIntStateByNameUsingNamedArgument(string expectedString, int expectedInt)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.With(new { StringProperty = expectedString, IntProperty = expectedInt });
            SampleType result = sut.Build();

            var expected = new SampleType { StringProperty = expectedString, IntProperty = expectedInt };
            Assert.Equal(expected, result, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void ThrowAnArgumentExceptionWhenMoreThanOneArgumentIsSuppliedForTheObjectLiteralSyntax()
        {
            var sut = A.BuilderFor<SampleType>();

            var exception = Assert.Throws<ParseException>(() => sut.With(1, 2));
            Assert.Contains("Expected a single object of an anonymous type, but was passed 2 arguments. Try replacing these arguments with an anonymous type composing the arguments.", exception.Message);
        }

        [Fact]
        public void ThrowAnArgumentExceptionWhenPassedSomethingOtherThanAnObjectLiteral()
        {
            var sut = A.BuilderFor<SampleType>();

            var exception = Assert.Throws<ParseException>(() => sut.With(5));
            Assert.Contains("Expected a single object of an anonymous type, but was passed an object of type System.Int32", exception.Message);
        }
    }
}
