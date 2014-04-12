using System;

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
        public void SetStringStateByName(string expected)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.WithStringProperty(expected);
            SampleType result = sut.Build();

            Assert.Equal(expected, result.StringProperty);
        }

        [Theory, AutoData]
        public void SetIntStateByName(int expected)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.WithIntProperty(expected);
            SampleType result = sut.Build();

            Assert.Equal(expected, result.IntProperty);
        }

        [Theory, AutoData]
        public void SetComplexStateByName(Exception expected)
        {
            var sut = A.BuilderFor<SampleType>();

            sut.WithComplexProperty(expected);
            SampleType result = sut.Build();

            Assert.Equal(expected, result.ComplexProperty);
        }

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

            var expected = new SampleType {StringProperty = expectedString, IntProperty = expectedInt};
            Assert.Equal(expected, result, new SampleTypeEqualityComparer());
        }

        [Theory, AutoData]
        public void ThrowAnArgumentExceptionWhenNoNameIsSuppliedForTheNamedArgumentsSyntax(string expected)
        {
            var sut = A.BuilderFor<SampleType>();

            var exception = Assert.Throws<ArgumentException>(() => sut.With(expected));
            Assert.Equal("No names were specified for the values provided. When using the named arguments (With()) syntax, you should specify the items to be set as argument names, such as With(customerId: customerId).", exception.Message);
        }
    }
}
