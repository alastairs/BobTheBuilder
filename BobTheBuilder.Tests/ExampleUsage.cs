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
            const string name = "expected value";
            var expected = new Person
            {
                Name = name
            };

            var built = A.BuilderFor<Person>()
                            .WithName(name)
                            .Build();

            Assert.Equal(expected, built, new PersonEqualityComparer());
        }

        [Fact]
        public void UsageWithImplicitCast()
        {
            const string name = "expected value";
            var expected = new Person
            {
                Name = name
            };

            Person built = A.BuilderFor<Person>().WithName(name);

            Assert.Equal(expected, built, new PersonEqualityComparer());
        }

        [Fact]
        public void UsageWithComplexType()
        {
            var address = new Address();
            var expected = new Person
            {
                Address = address
            };

            Person built = A.BuilderFor<Person>()
                                    .WithAddress(address);

            Assert.Equal(expected, built, new PersonEqualityComparer());
        }

        [Fact]
        public void UsageWithNamedArgument()
        {
            const string name = "expected value";
            const int age = 1;
            var expected = new Person
            {
                Name = name,
                AgeInYears = age
            };

            Person built = A.BuilderFor<Person>()
                                .With(name: name,
                                     ageInYears: age);

            Assert.Equal(expected, built, new PersonEqualityComparer());
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
        public void UsageWithObjectSyntax()
        {
            const string Name = "expected value";
            const int AgeInYears = 1;
            var expected = new Person
            {
                Name = Name,
                AgeInYears = AgeInYears
            };

            Person built = A.BuilderFor<Person>().With(new { Name, AgeInYears });

            Assert.Equal(expected, built, new PersonEqualityComparer());
        }

        [Fact]
        public void UsageWithInheritedProperties()
        {
            const string jobTitle = "expected value";
            const int age = 1;
            var expected = new Employee
            {
                AgeInYears = age,
                JobTitle = jobTitle
            };

            Employee built = A.BuilderFor<Employee>()
                             .WithJobTitle(jobTitle)
                             .WithAgeInYears(age);

            Assert.Equal(expected, built, new EmployeeEqualityComparer(new PersonEqualityComparer()));
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
