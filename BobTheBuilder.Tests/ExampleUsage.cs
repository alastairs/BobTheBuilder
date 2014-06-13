using System;
using System.Collections.Generic;
using Xunit;

namespace BobTheBuilder.Tests
{
    public class ExampleUsage
    {
        [Fact]
        public void Usage()
        {
            const string stringProperty = "expected value";
            var expected = new SampleType
            {
                StringProperty = stringProperty
            };

            var built = A.BuilderFor<SampleType>()
                            .WithStringProperty(stringProperty)
                            .Build();

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithImplicitCast()
        {
            const string stringProperty = "expected value";
            var expected = new SampleType
            {
                StringProperty = stringProperty
            };

            SampleType built = A.BuilderFor<SampleType>()
                                    .WithStringProperty(stringProperty);

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithComplexType()
        {
            var complexProperty = new Exception();
            var expected = new SampleType
            {
                ComplexProperty = complexProperty
            };

            SampleType built = A.BuilderFor<SampleType>()
                                    .WithComplexProperty(complexProperty);

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithNamedArgument()
        {
            const string expectedStringValue = "expected value";
            const int expectedIntValue = 1;
            var expected = new SampleType
            {
                StringProperty = expectedStringValue,
                IntProperty = expectedIntValue
            };

            SampleType built = A.BuilderFor<SampleType>()
                                    .With(stringProperty: expectedStringValue, 
                                          intProperty: expectedIntValue);

            Assert.Equal(expected, built, new SampleTypeEqualityComparer());
        }

        [Fact]
        public void UsageWithInheritedProperties()
        {
            const string expectedStringValue = "expected value";
            const int expectedIntValue = 1;
            var expected = new ExtendedSampleType
            {
                IntProperty = expectedIntValue,
                NewStringProperty = expectedStringValue
            };

            ExtendedSampleType built = A.BuilderFor<ExtendedSampleType>()
                                             .WithNewStringProperty(expectedStringValue)
                                             .WithIntProperty(expectedIntValue);

            Assert.Equal(expected, built, new ExtendedSampleTypeEqualityComparer(new SampleTypeEqualityComparer()));
        }

        [Fact]
        public void UsageForImmutableTypes()
        {
            ImmutableBlogPost builtPost = A.BuilderFor<ImmutableBlogPost>()
                                            .WithTitle("My first blog post")
                                            .WithAuthor(
                                                A.BuilderFor<ImmutableAuthor>()
                                                    .With(name: "Joe Bloggs", startDate: new DateTime(2013, 05, 23))
                                                    .Build())
                                            .WithContent("This is a blog post")
                                            .WithTimestamp(new DateTime(2014, 09, 12, 23, 55, 00))
                                            .WithTags(new[] {"coding", "csharp"});

            var expectedPost = new ImmutableBlogPost(
                                    "My first blog post",
                                    new ImmutableAuthor(
                                        "Joe Bloggs",
                                        new DateTime(2013, 05, 23)),
                                    "This is a blog",
                                    new DateTime(2014, 09, 12, 23, 55, 00),
                                    new[] { "coding", "csharp" });

            Assert.Equal(expectedPost, builtPost);
        }
    }
}
