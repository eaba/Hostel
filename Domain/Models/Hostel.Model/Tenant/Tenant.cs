
using System;
using System.Collections.Generic;

namespace Hostel.Model.Tenant
{
    public class Tenant
    {
        public Guid PersonId { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public Tenancy Tenancy { get; }
        public Birthday Birthday { get; }
        public List<Issue> Issues { get; }
        public Tenant(string firstName, string lastName, Tenancy tenancy, Birthday birthday, List<Issue> issues, Guid person)
        {
            LastName = lastName;
            FirstName = firstName;
            Tenancy = tenancy;
            Birthday = birthday;
            Issues = issues;
            PersonId = person;
        }
    }
}
