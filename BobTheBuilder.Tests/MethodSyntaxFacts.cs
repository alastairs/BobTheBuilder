using Microsoft.CSharp.RuntimeBinder;
using Ploeh.AutoFixture.Xunit2;
using System;
using Xunit;

namespace BobTheBuilder.Tests
{
    public class MethodSyntaxFacts
    {
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
        public void CallingAMethodThatDoesNotBeginWithTheWordWithResultsInARuntimeBinderException(string anonymous)
        {
            dynamic sut = A.BuilderFor<SampleType>();

            var exception = Assert.Throws<RuntimeBinderException>(() => sut.And(anonymous));
            Assert.True(exception.Message.EndsWith("does not contain a definition for 'And'"));
        }
    }
}
