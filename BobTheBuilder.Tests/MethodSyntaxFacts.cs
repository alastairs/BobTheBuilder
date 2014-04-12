using System;

using BobTheBuilder.ArgumentStore;
using BobTheBuilder.Syntax;

using Microsoft.CSharp.RuntimeBinder;

using NSubstitute;

using Ploeh.AutoFixture.Xunit;

using Xunit;
using Xunit.Extensions;

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
        public void MissingTheMemberNameFromTheMethodResultsInARuntimeBinderException(string anonymous)
        {
            var argumentStore = Substitute.For<IArgumentStore>();
            dynamic sut = new DynamicBuilder<SampleType>(new MethodSyntaxParser(argumentStore), argumentStore);

            var exception = Assert.Throws<RuntimeBinderException>(() => sut.With(anonymous));
            Assert.True(exception.Message.EndsWith("does not contain a definition for 'With'"));
        }

        [Theory, AutoData]
        public void CallingAMethodThatDoesNotBeginWithTheWordWithResultsInARuntimeBinderException(string anonymous)
        {
            var argumentStore = Substitute.For<IArgumentStore>();
            dynamic sut = new DynamicBuilder<SampleType>(new MethodSyntaxParser(argumentStore), argumentStore);

            var exception = Assert.Throws<RuntimeBinderException>(() => sut.And(anonymous));
            Assert.True(exception.Message.EndsWith("does not contain a definition for 'And'"));
        }
    }
}
