using System;
using System.Collections.Generic;

namespace BobTheBuilder.Tests
{
    internal class PersonEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            var namesAreEqual = x.Name == y.Name;
            var agesAreEqual = x.AgeInYears == y.AgeInYears;
            var addressesAreEqual = x.Address == y.Address;

            return namesAreEqual && agesAreEqual && addressesAreEqual;
        }

        public int GetHashCode(Person obj)
        {
            return 1;
        }
    }

    internal class EmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        private readonly IEqualityComparer<Person> wrappedComparer;

        internal EmployeeEqualityComparer(PersonEqualityComparer wrappedComparer)
        {
            this.wrappedComparer = wrappedComparer ?? throw new ArgumentNullException("wrappedComparer");
        }

        public bool Equals(Employee x, Employee y)
        {
            if (!wrappedComparer.Equals(x, y))
            {
                return false;
            }

            var jobTitlesAreEqual = x?.JobTitle == y?.JobTitle;
            return jobTitlesAreEqual;
        }

        public int GetHashCode(Employee obj)
        {
            return 1;
        }
    }
}
