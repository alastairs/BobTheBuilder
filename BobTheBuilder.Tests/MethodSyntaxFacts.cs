﻿using System;

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
    }
}
