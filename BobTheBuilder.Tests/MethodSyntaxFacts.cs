using Microsoft.CSharp.RuntimeBinder;
using AutoFixture.Xunit2;
using System;
using Xunit;

namespace BobTheBuilder.Tests
{
    public class MethodSyntaxFacts
    {
        [Theory, AutoData]
        public void SetStringStateByName(string expected)
        {
            var sut = A.BuilderFor<Person>();

            sut.WithName(expected);
            Person result = sut.Build();

            Assert.Equal(expected, result.Name);
        }

        [Theory, AutoData]
        public void SetIntStateByName(int expected)
        {
            var sut = A.BuilderFor<Person>();

            sut.WithAgeInYears(expected);
            Person result = sut.Build();

            Assert.Equal(expected, result.AgeInYears);
        }

        [Theory, AutoData]
        public void SetComplexStateByName(Address expected)
        {
            var sut = A.BuilderFor<Person>();

            sut.WithAddress(expected);
            Person result = sut.Build();

            Assert.Equal(expected, result.Address);
        }

        [Theory, AutoData]
        public void CallingAMethodThatDoesNotBeginWithTheWordWithResultsInARuntimeBinderException(string anonymous)
        {
            var sut = A.BuilderFor<Person>();

            var exception = Assert.Throws<RuntimeBinderException>(() => sut.And(anonymous));
            Assert.EndsWith("does not contain a definition for 'And'", exception.Message);
        }
    }
}
