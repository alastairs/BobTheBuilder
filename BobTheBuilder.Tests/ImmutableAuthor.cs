using System;

namespace BobTheBuilder.Tests
{
    internal class ImmutableAuthor
    {
        private readonly string name;
        private readonly DateTime startDate;

        public ImmutableAuthor(string name, DateTime startDate)
        {
            this.name = name;
            this.startDate = startDate;
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
        }
    }
}