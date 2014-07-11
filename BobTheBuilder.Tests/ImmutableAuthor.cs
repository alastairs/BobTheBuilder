using System;

namespace BobTheBuilder.Tests
{
    internal class ImmutableAuthor : IEquatable<ImmutableAuthor>
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

        public override bool Equals(object other)
        {
            return Equals(other as ImmutableAuthor);
        }

        public bool Equals(ImmutableAuthor other)
        {
            return Equals(this, other);
        }

        public bool Equals(ImmutableAuthor x, ImmutableAuthor y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            var namesAreEqual = x.Name == y.Name;
            var startDatesAreEqual = x.StartDate == y.StartDate;

            return namesAreEqual &&
                   startDatesAreEqual;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}