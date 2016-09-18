using System;
using System.Linq;
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
        public void UsageWithNestedBuilderInMethodSyntax()
        {
            var built = A.BuilderFor<ImmutableBlogPost>()
                .WithTitle("anonymousTitle")
                .WithAuthor(A.BuilderFor<ImmutableAuthor>()
                            .WithName("Jo")
                            .WithStartDate(DateTime.Today))
                .WithContent("anonymousContent")
                .WithTimestamp(DateTime.Today)
                .WithTags(Enumerable.Empty<string>());

            var expected = new ImmutableBlogPost(
                "anonymousTitle",
                new ImmutableAuthor("Jo", DateTime.Today),
                "anonymousContent",
                DateTime.Today,
                Enumerable.Empty<string>()
            );
            Assert.Equal(expected, built);
        }

        [Fact]
        public void UsageWithNestedBuilderInNamedArgumentsSyntax()
        {
            var built = A.BuilderFor<ImmutableBlogPost>().With(
                title: "anonymousTitle",
                author: A.BuilderFor<ImmutableAuthor>().With(
                    name: "Jo",
                    startDate: DateTime.Today),
                content: "anonymousContent",
                timestamp: DateTime.Today,
                tags: Enumerable.Empty<string>());

            var expected = new ImmutableBlogPost(
                "anonymousTitle",
                new ImmutableAuthor("Jo", DateTime.Today),
                "anonymousContent",
                DateTime.Today,
                Enumerable.Empty<string>()
            );
            Assert.Equal(expected, built);
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
            const string expectedTitle = "My first blog post";
            const string expectedAuthorName = "Joe Bloggs";
            var expectedStartDate = new DateTime(2013, 05, 23);
            const string expectedContent = "This is a blog post";
            var expectedTimestamp = new DateTime(2014, 09, 12, 23, 55, 00);
            var expectedTags = new[] { "coding", "csharp" };

            ImmutableBlogPost builtPost = A.BuilderFor<ImmutableBlogPost>()
                                            .WithTitle(expectedTitle)
                                            .WithAuthor(
                                                A.BuilderFor<ImmutableAuthor>()
                                                    .With(name: expectedAuthorName, startDate: expectedStartDate)
                                                    .Build())
                                            .WithContent(expectedContent)
                                            .WithTimestamp(expectedTimestamp)
                                            .WithTags(expectedTags);

            var expectedPost = new ImmutableBlogPost(
                                    expectedTitle,
                                    new ImmutableAuthor(
                                        expectedAuthorName,
                                        expectedStartDate),
                                    expectedContent,
                                    expectedTimestamp,
                                    expectedTags);

            Assert.Equal(expectedPost, builtPost);
        }
    }
}
