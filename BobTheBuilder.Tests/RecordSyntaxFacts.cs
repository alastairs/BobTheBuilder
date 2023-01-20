using AutoFixture.Xunit2;
using Xunit;

namespace BobTheBuilder.Tests;

public class RecordSyntaxFacts
{
    [Theory, AutoData]
    public void SetStringStateByName(string expected)
    {
        var sut = A.BuilderFor<Person>().UsingRecords();

        sut = sut with { Name = expected };
        Person result = sut.Build();

        Assert.Equal(expected, result.Name);
    }
}