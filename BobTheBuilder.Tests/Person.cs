using System;

namespace BobTheBuilder.Tests
{
    internal class Person
    {
        public string Name { get; set; }
        public int AgeInYears { get; set; }
        public Address Address { get; set; }
    }

    public record Address
    {
        public string Line1 { get; }
        public string Line2 { get; }
        public string Line3 { get; }
        public string County { get; }
        public string PostCode { get; }
    }

    internal class Employee : Person
    {
        public Employee Manager { get; set; }

        public string JobTitle { get; set; }
    }
}
